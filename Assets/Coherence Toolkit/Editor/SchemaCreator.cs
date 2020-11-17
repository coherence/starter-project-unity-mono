namespace Coherence.MonoBridge
{
    using UnityEngine;
    using UnityEditor;

    using System.IO;
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;
    using UnityEditor.Animations;

    public class SchemaCreator
    {
        static string OutDirectory => $"{Application.dataPath}/Schemas";

        static Dictionary<string, IWorkaround> specialCases = new Dictionary<string, IWorkaround>()
        {
            {"UnityEngine.Transform",
             new BasicWorkaround(new List<(string, string, string[])> {
                     ("position", "WorldPosition", new string[] {"Value"}),
                     ("rotation", "WorldOrientation", new string[] {"Value"}),
                     ("localScale", "GenericScale", new string[] {"Value"}),
                 })},

            {"UnityEngine.Animator",
             new AnimatorWorkaround()}
        };

        public static void GatherSyncBehavioursAndEmit()
        {
            var coherenceSyncBehaviours = GatherSyncBehaviours();
            SaveGatheredSchema(coherenceSyncBehaviours);
            SaveJson(coherenceSyncBehaviours);
        }

        private static CoherenceSync[] GatherSyncBehaviours() {
            var gathered = new List<CoherenceSync>();
#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });
            var coherenceSyncType = typeof(CoherenceSync);

            foreach (var guid in guids)
            {
                var objectPath = AssetDatabase.GUIDToAssetPath(guid);
                var objs = AssetDatabase.LoadAllAssetsAtPath(objectPath);

                foreach (var o in objs)
                {
                    var synced = o as CoherenceSync;

                    if(synced)
                    {
                        //Debug.Log($"Found prefab with CoherenceSync script: {o.name} : {o.GetType()}");
                        gathered.Add(synced);
                    }
                }
            }
#endif
            return gathered.ToArray();
        }

        private static void SaveGatheredSchema(CoherenceSync[] coherenceSyncBehaviours) {
#if UNITY_EDITOR
            var componentDefinitions = new Dictionary<string, ComponentDefinition>();

            foreach(var coherenceSync in coherenceSyncBehaviours)
            {
                var components = coherenceSync.gameObject.GetComponents<Component>();

                foreach (var component in components)
                {
                    var componentType = component.GetType();
                    var componentTypeString = componentType.AssemblyQualifiedName;
                    var componentName = componentType.ToString();
                    var componentToggleOn = coherenceSync.GetScriptToggle(componentTypeString) ?? false;

                    if(TypeHelpers.SkipThisType(componentType) || !componentToggleOn)
                    {
                        continue;
                    }

                    if(specialCases.TryGetValue(componentName, out IWorkaround workaround))
                    {
                        var defs = workaround.ComponentDefinitions(componentTypeString, coherenceSync);
                        foreach(var def in defs)
                        {
                            componentDefinitions[def.name] = def;
                        }
                        continue;
                    }

                    var schemaComponentName = SchemaComponentName(coherenceSync, componentName);
                    var componentDefinition = new ComponentDefinition(schemaComponentName);
                    componentDefinitions[schemaComponentName] = componentDefinition;

                    var members = TypeHelpers.Members(componentType);

                    foreach (var memberInfo in members)
                    {
                        var fieldType = TypeHelpers.GetUnderlyingType(memberInfo);
                        var varString = componentTypeString + CoherenceSync.KeyDelimiter + memberInfo.Name;
                        var memberToggleOn = coherenceSync.GetFieldToggle(varString) ?? false;

                        if (!TypeHelpers.IsTypeSupported(fieldType) ||
                            TypeHelpers.IsMonoMember(memberInfo.GetType()) ||
                            !memberToggleOn)
                        {
                            continue;
                        }

                        var memberName = memberInfo.Name;
                        var memberType = TypeHelpers.ToSchemaType(fieldType);
                        var member = new ComponentMemberDescription(memberName, memberType);
                        componentDefinition.members.Add(member);
                    }
                }
            }

            var schemaFilename = $"Gathered.schema";
            var schemaFullPath = $"{OutDirectory}/{schemaFilename}";

            var schemaWriter = new StreamWriter(schemaFullPath);
            var schemaCode = CreateSchema(componentDefinitions.Values, false);
            schemaWriter.Write(schemaCode);
            schemaWriter.Close();

            Debug.Log($"Saving schema to '{schemaFullPath}'.");
            AssetDatabase.Refresh();
#endif
        }

        public static string SchemaComponentName(CoherenceSync coherenceSync, string componentName)
        {
            return Mangle(coherenceSync.name + "_" + componentName);
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
                foreach(var member in component.members)
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

            var jsonWriter = new StreamWriter(jsonFullPath);
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
                    if(specialCases.TryGetValue(componentName, out IWorkaround specialCase))
                    {
                        syncTheseComponents.AddRange(specialCase.SyncedComponents(componentTypeString, coherenceSync));
                        continue;
                    }

                    // Normal components
                    var members = TypeHelpers.Members(componentType);
                    var syncTheseMembers = new List<string>();

                    foreach (MemberInfo memberInfo in members)
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

        public static string Mangle(string s)
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
        public List<ComponentMemberDescription> members;

        public ComponentDefinition(string name) {
            this.name = name;
            this.members = new List<ComponentMemberDescription>();
        }

        public ComponentDefinition(string name, List<ComponentMemberDescription> members) {
            this.name = name;
            this.members = members;
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

        public SyncedComponent(string name, string[] members) {
            this.ComponentName = name;
            this.Members = members;
        }
    }

    public interface IWorkaround
    {
        List<SyncedComponent> SyncedComponents(string componentTypeString, CoherenceSync coherenceSync);
        List<ComponentDefinition> ComponentDefinitions(string componentTypeString, CoherenceSync coherenceSync);
    }

    // Structured way of handle special cases in the emitter, for example for Transform (translation/rotation/localScale)
    public class BasicWorkaround : IWorkaround
    {
        List<(string, string, string[])> mappings; // (<monoBehaviourField>, <ecsComponentName>, <ecsComponentMembers>)

        public BasicWorkaround(List<(string, string, string[])> mappings)
        {
            this.mappings = mappings;
        }

        public List<SyncedComponent> SyncedComponents(string componentTypeString, CoherenceSync coherenceSync)
        {
            var syncTheseComponents = new List<SyncedComponent>();

            foreach(var (monoBehaviourField, ecsComponentName, ecsComponentMembers) in mappings)
            {
                var key = componentTypeString + CoherenceSync.KeyDelimiter + monoBehaviourField;
                var toggleOn = coherenceSync.GetFieldToggle(key);

                if(toggleOn == null) {
                    Debug.Log($"Can't find toggle key '{key}'");
                }
                else if(toggleOn.Value) {
                    var component = new SyncedComponent(ecsComponentName, ecsComponentMembers);
                    syncTheseComponents.Add(component);
                }
            }

            return syncTheseComponents;
        }

        public List<ComponentDefinition> ComponentDefinitions(string componentTypeString, CoherenceSync coherenceSync)
        {
            return new List<ComponentDefinition>(); // no schema components defined by default
        }
    }

    public class AnimatorWorkaround : IWorkaround
    {
        public List<SyncedComponent> SyncedComponents(string componentTypeString, CoherenceSync coherenceSync)
        {
            var syncTheseComponents = new List<SyncedComponent>();
            var animator = coherenceSync.gameObject.GetComponent<Animator>();
            var controller = animator.runtimeAnimatorController as AnimatorController;

            if(controller == null)
            {
                return syncTheseComponents;
            }

            var componentMembers = new List<string>();

            foreach (var parameter in controller.parameters)
            {
                var key = componentTypeString + CoherenceSync.KeyDelimiter + parameter.name;
                var toggleOn = coherenceSync.GetFieldToggle(key);

                if(toggleOn == null)
                {
                    Debug.LogWarning($"Can't find toggle key '{key}'");
                }
                else if(toggleOn.Value)
                {
                    componentMembers.Add(parameter.name);
                }
            }

            var componentName = SchemaCreator.SchemaComponentName(coherenceSync, "Animator");
            var animatorComponent = new SyncedComponent(componentName, componentMembers.ToArray());
            syncTheseComponents.Add(animatorComponent);

            return syncTheseComponents;
        }

        public List<ComponentDefinition> ComponentDefinitions(string componentTypeString, CoherenceSync coherenceSync)
        {
            var definitions = new List<ComponentDefinition>();
            var animator = coherenceSync.gameObject.GetComponent<Animator>();
            var controller = animator.runtimeAnimatorController as AnimatorController;

            if(controller == null)
            {
                return definitions;
            }

            var members = new List<ComponentMemberDescription>();

            foreach (var parameter in controller.parameters)
            {
                var key = componentTypeString + CoherenceSync.KeyDelimiter + parameter.name;
                var toggleOn = coherenceSync.GetFieldToggle(key);

                if(toggleOn == null)
                {
                    Debug.LogWarning($"Can't find toggle key '{key}'");
                }
                else if(toggleOn.Value)
                {
                    string parameterTypeName = null;

                    switch(parameter.type) {
                        case AnimatorControllerParameterType.Bool:
                            parameterTypeName = "Bool";
                            break;
                        case AnimatorControllerParameterType.Int:
                            parameterTypeName = "Int";
                            break;
                        case AnimatorControllerParameterType.Float:
                            parameterTypeName = "Float";
                            break;
                    }

                    if(parameterTypeName != null)
                    {
                        var description = new ComponentMemberDescription(parameter.name, parameterTypeName);
                        members.Add(description);
                    }
                }
            }

            var componentName = SchemaCreator.SchemaComponentName(coherenceSync, "Animator");
            var animatorComponent = new ComponentDefinition(componentName, members);
            definitions.Add(animatorComponent);

            return definitions;
        }
    }
}
