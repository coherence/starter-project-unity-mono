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

            Component[] components = coherenceSync.gameObject.GetComponents(typeof(Component));

            foreach (Component component in components)
            {
                var componentType = component.GetType();
                var componentTypeString = componentType.AssemblyQualifiedName;
                var componentToggleOn = coherenceSync.GetScriptToggle(componentTypeString) ?? false;

                if(TypeHelpers.SkipThisType(componentType) ||
                   !componentToggleOn ||
                   componentType == typeof(Transform))
                {
                    continue;
                }

                var componentName = componentType.ToString();
                var componentDefinition = new ComponentDefinition(componentName.ToString());

                componentDefinitions[componentName] = componentDefinition;

                var members = TypeHelpers.Members(componentType);

                foreach (MemberInfo variable in members)
                {
                    var fieldType = TypeHelpers.GetUnderlyingType(variable);
                    var varString = componentTypeString + CoherenceSync.KeyDelimiter + variable.Name;
                    var memberToggleOn = coherenceSync.GetFieldToggle(varString) ?? false;

                    if (!memberToggleOn ||
                        !TypeHelpers.IsTypeSupported(fieldType) ||
                        TypeHelpers.IsMonoMember(variable.GetType()))
                    {
                        continue;
                    }

                    var memberName = variable.Name;
                    var memberType = TypeHelpers.ToSchemaType(fieldType);
                    var member = new ComponentMemberDescription(memberName, memberType);
                    componentDefinition.members.Add(member);
                }
            }

            var className = $"CoherenceSync{coherenceSync.name}";

            var filename = $"Gathered.schema";
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
            //writer.Write(header); Not needed if we concat the schemas together

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


    }
}
