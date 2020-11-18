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
             new BasicWorkaround("transform", "Transform", new List<(string, string, string[], string[])> {
                     ("position", "WorldPosition", new string[] {"Value"}, new string[] {"position"}),
                     ("rotation", "WorldOrientation", new string[] {"Value"}, new string[] {"rotation"}),
                     ("localScale", "GenericScale", new string[] {"Value"}, new string[] {"localScale"}),
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

                    var schemaComponentName = SchemaComponentName(coherenceSync, componentName);
                    var syncedComponent = new SyncedComponent(schemaComponentName,
                                                              syncTheseMembers.ToArray(),
                                                              true,
                                                              $"_{componentName.ToLower()}",
                                                              componentName,
                                                              syncTheseMembers.ToArray(), // same names for getters
                                                              syncTheseMembers.ToArray() // same names for setters
                                                              );
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
        public string ComponentName; // Name of the ECS component that the sync script will sync the MonoBehaviour:s data with
        public string[] Members; // Names of the members of the ECS component
        public bool NeedCachedProperty; // Will generate a _componentName reference in the sync script
        public string Property; // Name of the property to access the MonoBehaviour via, e.g. 'transform'
        public string PropertyType; // Type of the property to access the MonoBehaviour via, e.g. 'transform'
        public string[] PropertyGetters; // How to get data from the property, e.g. '.position' or 'GetFloat'
        public string[] PropertySetters; // How to get data from the property, e.g. '.position' or 'SetFloat()'

        public SyncedComponent(string name, string[] members, bool needsInitializer,
                               string property, string propertyType, string[] getters, string[] setters) {
            this.ComponentName = name;
            this.Members = members;
            this.NeedCachedProperty = needsInitializer;
            this.Property = property;
            this.PropertyType = propertyType;
            this.PropertyGetters = getters;
            this.PropertySetters = setters;
        }

        // public SyncedComponent(string name, string[] members) {
        //     this.ComponentName = name;
        //     this.Members = members;
        //     this.NeedCachedProperty = false;
        //     this.MonoBehaviour = null;
        //     this.MonoBehaviourGetters = null;
        // }
    }

    public interface IWorkaround
    {
        List<SyncedComponent> SyncedComponents(string componentTypeString, CoherenceSync coherenceSync);
        List<ComponentDefinition> ComponentDefinitions(string componentTypeString, CoherenceSync coherenceSync);
    }

    // Structured way of handle special cases in the emitter, for example for Transform (translation/rotation/localScale)
    public class BasicWorkaround : IWorkaround
    {
        string monoBehaviourProperty;
        string monoBehaviourPropertyType;

        // mappings contains
        List<(string, string, string[], string[])> mappings;

        public BasicWorkaround(string monoBehaviourProperty, string monoBehaviourPropertyType,
                               List<(string, string, string[], string[])> mappings)
        {
            this.monoBehaviourProperty = monoBehaviourProperty;
            this.monoBehaviourPropertyType = monoBehaviourPropertyType;
            this.mappings = mappings;
        }

        public List<SyncedComponent> SyncedComponents(string componentTypeString, CoherenceSync coherenceSync)
        {
            var syncTheseComponents = new List<SyncedComponent>();

            foreach(var (monoBehaviourField, ecsComponentName, ecsComponentMembers, innerFields) in mappings)
            {
                var key = componentTypeString + CoherenceSync.KeyDelimiter + monoBehaviourField;
                var toggleOn = coherenceSync.GetFieldToggle(key);

                if(toggleOn == null) {
                    Debug.Log($"Can't find toggle key '{key}'");
                }
                else if(toggleOn.Value) {
                    var component = new SyncedComponent(ecsComponentName,
                                                        ecsComponentMembers,
                                                        false,
                                                        monoBehaviourProperty,
                                                        monoBehaviourPropertyType,
                                                        innerFields,
                                                        innerFields);
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
            var animatorGetters = new List<string>();
            var animatorSetters = new List<string>();

            foreach (var parameter in controller.parameters)
            {
                var key = componentTypeString + CoherenceSync.KeyDelimiter + parameter.name;
                var toggleOn = coherenceSync.GetFieldToggle(key);
                var parameterTypeName = ParameterTypeName(parameter.type);

                if(toggleOn == null)
                {
                    Debug.LogWarning($"Can't find toggle key '{key}'");
                }
                else if(toggleOn.Value && parameterTypeName != null)
                {
                    componentMembers.Add(parameter.name);
                    animatorGetters.Add($"Get{parameterTypeName}(\"{parameter.name}\")");
                    animatorSetters.Add($"Set{parameterTypeName}(\"{parameter.name}\", @)");
                }
            }

            var componentName = SchemaCreator.SchemaComponentName(coherenceSync, "Animator");
            var animatorComponent = new SyncedComponent(componentName,
                                                        componentMembers.ToArray(),
                                                        true,
                                                        "_animator",
                                                        "Animator",
                                                        animatorGetters.ToArray(),
                                                        animatorSetters.ToArray()
                                                        );
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
                    var parameterTypeName = ParameterTypeName(parameter.type);

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

        static string ParameterTypeName(AnimatorControllerParameterType type)
        {
            string parameterTypeName = null;

            switch(type) {
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

            return parameterTypeName;
        }
    }
}
