namespace Coherence.MonoBridge
{
    using System.IO;
    using UnityEngine;
    using UnityEditor;

    using System.Collections.Generic;
    using System.Reflection;
    using System;
    using System.Linq;

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

    public struct ComponentDefinition
    {
        public string name;
        public List<ComponentMemberDescription> members;

        public ComponentDefinition(string name) {
            this.name = name;
            this.members = new List<ComponentMemberDescription>();
        }
    }

    public class SchemaCreator
    {
        public static void SaveSyncBehaviour(CoherenceSync coherenceSync)
        {
#if UNITY_EDITOR

            // foreach(var element in coherenceSync.FieldLinks) {
            //     Debug.Log($"LINK {element.Key} => {element.Value}");
            // }

            // foreach(var element in coherenceSync.FieldToggles) {
            //     Debug.Log($"TOGGLE {element.Key} => {element.Value}");
            // }

            // foreach(var element in coherenceSync.FieldTypes) {
            //     Debug.Log($"TYPE {element.Key} => {element.Value}");
            // }

            var componentDefinitions = new Dictionary<string, ComponentDefinition>();

            // TODO: Share with CoherenceSyncEditor code:
            Type monoBehaviourType = typeof(MonoBehaviour);
            const BindingFlags monoBindingFlags = BindingFlags.Public | BindingFlags.Instance;
            MemberInfo[] monoMembers = monoBehaviourType.GetFields(monoBindingFlags).Cast<MemberInfo>()
                .Concat(monoBehaviourType.GetProperties(monoBindingFlags)).ToArray();

            Component[] components = coherenceSync.gameObject.GetComponents(typeof(Component));

            foreach (Component component in components)
            {
                var componentType = component.GetType();
                var componentTypeString = componentType.AssemblyQualifiedName;
                var toggleOn = coherenceSync.GetScriptToggle(componentTypeString) ?? false;

                if(SkipThisType(componentType) || !toggleOn || componentType == typeof(Transform))
                {
                    continue;
                }

                var componentName = componentType.ToString();
                var componentDefinition = new ComponentDefinition(componentName.ToString());

                componentDefinitions[componentName] = componentDefinition;

                // TODO: Share
                const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                MemberInfo[] members =
                    componentType.GetFields(bindingFlags).Cast<MemberInfo>()
                    .Concat(componentType.GetProperties(bindingFlags)).ToArray();

                foreach (MemberInfo variable in members)
                {
                    Type fieldType = GetUnderlyingType(variable);

                    if (!IsTypeSupported(fieldType))
                    {
                        continue;
                    }

                    bool doProceed = true;

                    // Remove those inherited from MonoBehaviour (de-clu tter)
                    foreach (MemberInfo monoVariable in monoMembers)
                    {
                        if (variable.Name == monoVariable.Name)
                        {
                            doProceed = false;
                            break;
                        }
                    }

                    if (!doProceed)
                    {
                        continue;
                    }

                    var memberName = variable.Name;
                    var memberType = ToSchemaType(fieldType);
                    var member = new ComponentMemberDescription(memberName, memberType);
                    componentDefinition.members.Add(member);
                }

            }

            // foreach(var x in coherenceSync.FieldLinks.Keys)
            // {
            //     var componentName = coherenceSync.GetComponentFromKey(x).ToString();

            //     if(!componentDefinitions.ContainsKey(componentName)) {
            //         componentDefinitions[componentName] = new ComponentDefinition(componentName.ToString());
            //     }

            //     var component = componentDefinitions[componentName];
            //     component.members.Add(new ComponentMemberDescription());
            // }


            // new string[]{
            //     "Translation",
            //     "Rotation",
            //     "SessionBased",
            //     "Simulated"
            // };


            var className = $"CoherenceSync{coherenceSync.name}";

            var filename = $"Baked.gen.schema"; // TODO: Rename to Gathered
            var outDirectory = $"{Application.dataPath}/Schemas";
            var outFilePath = $"{outDirectory}/{filename}";

            StreamWriter writer = new StreamWriter(outFilePath);
            var schemaCode = CreateBakedSchema(componentDefinitions.Values);
            writer.Write(schemaCode);
            writer.Close();

            Debug.Log($"Saving baked schema to '{outFilePath}', generated code: {schemaCode}");

            AssetDatabase.Refresh();
#endif
        }

        private static string CreateBakedSchema(IEnumerable<ComponentDefinition> components) {

            var header =
@"name Schema

namespace Coherence.Generated.FirstProject



";

            var writer = new StringWriter();
            writer.Write(header);

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

        public static bool SkipThisType(Type compType)
        {
            return
                compType.IsSubclassOf(typeof(Renderer)) || compType == typeof(Renderer) ||
                compType.IsSubclassOf(typeof(MeshFilter)) || compType == typeof(MeshFilter) ||
                compType.IsSubclassOf(typeof(CoherenceSync)) || compType == typeof(CoherenceSync) ||
                compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider) ||
                compType.IsSubclassOf(typeof(Rigidbody)) || compType == typeof(Rigidbody) ||
                compType.IsSubclassOf(typeof(Rigidbody2D)) || compType == typeof(Rigidbody2D) ||
                compType.IsSubclassOf(typeof(Collider)) || compType == typeof(Collider) ||
                compType.IsSubclassOf(typeof(Collider2D)) || compType == typeof(Collider2D);
        }

        public static Type GetUnderlyingType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.All:
                case MemberTypes.Constructor:
                case MemberTypes.Custom:
                case MemberTypes.NestedType:
                case MemberTypes.TypeInfo:
                default:
                    throw new ArgumentException
                    (
                        "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        private readonly static Type[] supportedTypes = {
            typeof(Vector3),
            typeof(Quaternion),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(string),
            typeof(bool),
            typeof(Boolean)
        };

        public static bool IsTypeSupported(Type type)
        {
            foreach (Type ct in supportedTypes)
            {
                if (type.IsSubclassOf(ct) || type == ct)
                {
                    return true;
                }
            }

            return false;
        }

        public static string ToSchemaType(Type type)
        {
            if(type == typeof(int)) { return "Int"; }
            else if(type == typeof(uint)) { return "Int"; }
            else if(type == typeof(float)) { return "Float"; }
            else if(type == typeof(bool)) { return "Bool"; }
            else if(type == typeof(Boolean)) { return "Bool"; }
            else if(type == typeof(string)) { return "String64"; }
            else if(type == typeof(Vector3)) { return "Vector3"; }
            else if(type == typeof(Quaternion)) { return "Quaternion"; }
            else {
                throw new Exception($"Can't convert type {type} to schema member type.");
            }
        }
    }
}
