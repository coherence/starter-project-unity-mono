namespace Coherence.MonoBridge
{
    using UnityEngine;
    using UnityEditor;

    using System.IO;
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;

    public class SchemaCreator
    {
        static string OutDirectory => $"{Application.dataPath}/Schemas";

        static Dictionary<string, SpecialCase> specialCases = new Dictionary<string, SpecialCase>()
        {
            {"UnityEngine.Transform",
             new SpecialCase(new List<(string, string, string[])> {
                                 ("position", "WorldPosition", new string[] {"Value"}),
                                 ("rotation", "WorldOrientation", new string[] {"Value"}),
                                 ("localScale", "NEED_A_TYPE_HERE", new string[] {"Value"}),
                             })}
        };

        public static void GatherSyncBehavioursAndEmit()
        {
            var coherenceSyncBehaviours = GatherSyncBehaviours();
            SaveSyncBehaviours(coherenceSyncBehaviours);
        }

        private static CoherenceSync[] GatherSyncBehaviours() {
            var gathered = new List<CoherenceSync>();
#if UNITY_EDITOR

            string[] guids = AssetDatabase.FindAssets("t:Object", new[] { "Assets" });
            var coherenceSyncType = typeof(CoherenceSync);

            foreach (string guid in guids)
            {
                string objectPath = AssetDatabase.GUIDToAssetPath(guid);
                Object[] objs = AssetDatabase.LoadAllAssetsAtPath(objectPath);

                foreach (Object o in objs)
                {
                    var synced = o as CoherenceSync;

                    if(synced)
                    {
                        Debug.Log($"Found prefab with CoherenceSync script: {o.name} : {o.GetType()}");
                        gathered.Add(synced);
                    }
                }
            }
#endif

            return gathered.ToArray();
        }

        private static void SaveSyncBehaviours(CoherenceSync[] coherenceSyncBehaviours)
        {
            var setOfComponents = new HashSet<Component>();

            foreach(var sync in coherenceSyncBehaviours)
            {
                setOfComponents.UnionWith(sync.gameObject.GetComponents(typeof(Component)));
            }

            var componentArray = new Component[setOfComponents.Count];
            setOfComponents.CopyTo(componentArray);

            SaveGatheredSchema(componentArray);
            SaveJson(coherenceSyncBehaviours);
        }

        private static void SaveGatheredSchema(Component[] components) {
#if UNITY_EDITOR
            var componentDefinitions = new Dictionary<string, ComponentDefinition>();

            foreach (var component in components)
            {
                var componentType = component.GetType();
                var componentTypeString = componentType.AssemblyQualifiedName;

                // NOTE: If we want to create a unique ECS component for each prefab component, we need this:
                //var componentToggleOn = coherenceSync.GetScriptToggle(componentTypeString) ?? false;

                if(TypeHelpers.SkipThisType(componentType) ||
                   componentType == typeof(Transform))
                {
                    continue;
                }

                var componentName = componentType.ToString();
                var componentDefinition = new ComponentDefinition(componentName.ToString());

                componentDefinitions[componentName] = componentDefinition;

                var mappings = TypeHelpers.Members(componentType);

                foreach (var memberInfo in mappings)
                {
                    var fieldType = TypeHelpers.GetUnderlyingType(memberInfo);
                    var varString = componentTypeString + CoherenceSync.KeyDelimiter + memberInfo.Name;

                    // NOTE: If we want to create a unique ECS component for each prefab component, we need this:
                    //var memberToggleOn = coherenceSync.GetFieldToggle(varString) ?? false;

                    if (!TypeHelpers.IsTypeSupported(fieldType) ||
                        TypeHelpers.IsMonoMember(memberInfo.GetType()))
                    {
                        continue;
                    }

                    var memberName = memberInfo.Name;
                    var memberType = TypeHelpers.ToSchemaType(fieldType);
                    var member = new ComponentMemberDescription(memberName, memberType);
                    componentDefinition.mappings.Add(member);
                }
            }


            var schemaFilename = $"Gathered.schema";
            var schemaFullPath = $"{OutDirectory}/{schemaFilename}";

            StreamWriter schemaWriter = new StreamWriter(schemaFullPath);
            var schemaCode = CreateSchema(componentDefinitions.Values, false);
            schemaWriter.Write(schemaCode);
            schemaWriter.Close();

            Debug.Log($"Saving schema to '{schemaFullPath}'.");
            AssetDatabase.Refresh();
#endif
        }

        private static string CreateSchema(IEnumerable<ComponentDefinition> components, bool writeHeader)
        {
            var header =
@"name Schema

namespace Coherence.Generated.FirstProject



";

            var writer = new StringWriter();

            if(writeHeader) {
                writer.Write(header);
            }

            foreach(var component in components)
            {
                writer.Write($"component {component.name}\n");
                foreach(var member in component.mappings)
                {
                    writer.Write($"  {member.variableName} {member.typeName}\n");
                }
                writer.Write("\n");
            }

            writer.Close();
            return writer.ToString();
        }

        private static void SaveJson(CoherenceSync[] syncers)
        {
            var jsonFilename = $"Gathered.json";
            var jsonFullPath = $"{OutDirectory}/{jsonFilename}";

            StreamWriter jsonWriter = new StreamWriter(jsonFullPath);
            var jsonCode = CreateJson(syncers);
            jsonWriter.Write(jsonCode);
            jsonWriter.Close();

            Debug.Log($"Saving json to '{jsonFullPath}'.");
        }

        private static string CreateJson(CoherenceSync[] syncers)
        {
            // Each GO/Prefab with a CoherenceSync script will result in
            // one CoherenceSyncNAME, where NAME is the name of the GO/Preab itself.
            // These are transmitted to the protocol-code-generator via a json file
            // containing an array of such 'SyncedBehaviour':s

            var jsonData = new List<SyncedBehaviour>();

            foreach(var coherenceSync in syncers)
            {
                var components = coherenceSync.gameObject.GetComponents(typeof(Component));
                var syncTheseComponents = new List<SyncedComponent>();

                foreach (Component component in components)
                {
                    var componentType = component.GetType();
                    var componentTypeString = componentType.AssemblyQualifiedName;
                    var componentToggleOn = coherenceSync.GetScriptToggle(componentTypeString) ?? false;

                    if(TypeHelpers.SkipThisType(componentType) ||
                       !componentToggleOn)
                    {
                        continue;
                    }

                    var componentName = componentType.ToString();

                    // Special cases
                    if(specialCases.TryGetValue(componentName, out SpecialCase specialCase))
                    {
                        syncTheseComponents.AddRange(specialCase.Components(componentTypeString, coherenceSync));
                        continue;
                    }

                    // Normal components
                    var mappings = TypeHelpers.Members(componentType);
                    var syncTheseMembers = new List<string>();

                    foreach (MemberInfo memberInfo in mappings)
                    {
                        var fieldType = TypeHelpers.GetUnderlyingType(memberInfo);
                        var varString = componentTypeString + CoherenceSync.KeyDelimiter + memberInfo.Name;
                        var memberToggleOn = coherenceSync.GetFieldToggle(varString) ?? false;

                        if (!TypeHelpers.IsTypeSupported(fieldType) ||
                            !memberToggleOn ||
                            TypeHelpers.IsMonoMember(memberInfo.GetType()))
                        {
                            continue;
                        }

                        syncTheseMembers.Add(memberInfo.Name);
                    }

                    var syncedComponent = new SyncedComponent(componentName, syncTheseMembers.ToArray());
                    syncTheseComponents.Add(syncedComponent);
                }

                var mangledName = Mangle(coherenceSync.name);
                var className = $"CoherenceSync{mangledName}";
                jsonData.Add(new SyncedBehaviour(className, syncTheseComponents.ToArray()));
            }

            var writer = new StringWriter();
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, jsonData.ToArray());
            return writer.ToString();
        }

        private static string Mangle(string s)
        {
            return
                s.Replace("-", "_")
                 .Replace(" ", "_");
        }
    }

    // Used for schema generation -- corresponds to an coherence ECS component.
    public struct ComponentDefinition
    {
        public string name;
        public List<ComponentMemberDescription> mappings;

        public ComponentDefinition(string name) {
            this.name = name;
            this.mappings = new List<ComponentMemberDescription>();
        }
    }

    // Used for schema generation -- corresponds to an coherence ECS component member.
    public struct ComponentMemberDescription
    {
        public string variableName;
        public string typeName;

        public ComponentMemberDescription(string variableName, string typeName)
        {
            this.variableName = variableName;
            this.typeName = typeName;
        }
    }

    // Used for json generation -- corresponds to one CoherenceSyncNAME being generated.
    // Note: The member names have some stuttering to make the json readable on its own
    public struct SyncedBehaviour
    {
        public string BehaviourName;
        public SyncedComponent[] Components;

        public SyncedBehaviour(string name, SyncedComponent[] components) {
            this.BehaviourName = name;
            this.Components = components;
        }
    }

    // Used for json generation
    // Note: The member names have some stuttering to make the json readable on its own
    public struct SyncedComponent
    {
        public string ComponentName;
        public string[] Members;

        public SyncedComponent(string name, string[] mappings) {
            this.ComponentName = name;
            this.Members = mappings;
        }
    }

    // Structured way of handle special cases in the emitter, for example for Transform (translation/rotation/localScale)
    public struct SpecialCase
    {
        List<(string, string, string[])> mappings; // (<monoBehaviourField>, <ecsComponentName>, <ecsComponentMembers>)

        public SpecialCase(List<(string, string, string[])> mappings)
        {
            this.mappings = mappings;
        }

        public List<SyncedComponent> Components(string componentTypeString, CoherenceSync coherenceSync)
        {
            var syncTheseComponents = new List<SyncedComponent>();

            foreach(var (monoBehaviourField, ecsComponentName, ecsComponentMembers) in mappings)
            {
                var key = componentTypeString + CoherenceSync.KeyDelimiter + monoBehaviourField;
                var toggleOn = coherenceSync.GetFieldToggle(key);

                if(toggleOn == null) {
                    Debug.Log($"No key named {key}");
                }
                else if(toggleOn.Value) {
                    var translationComponent = new SyncedComponent(ecsComponentName, ecsComponentMembers);
                    syncTheseComponents.Add(translationComponent);
                }
            }

            return syncTheseComponents;
        }
    }
}
