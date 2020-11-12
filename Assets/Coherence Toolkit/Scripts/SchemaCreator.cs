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

        public static void SaveSyncBehaviour(CoherenceSync coherenceSync)
        {
            var temp = coherenceSync.gameObject.GetComponents(typeof(Component));

            SaveGatheredSchema(temp);
            SaveJson(new CoherenceSync[] { coherenceSync });
        }

        public static void SaveGatheredSchema(Component[] components) {
#if UNITY_EDITOR
            var componentDefinitions = new Dictionary<string, ComponentDefinition>();

            foreach (Component component in components)
            {
                var componentType = component.GetType();
                var componentTypeString = componentType.AssemblyQualifiedName;
                //var componentToggleOn = coherenceSync.GetScriptToggle(componentTypeString) ?? false;

                if(TypeHelpers.SkipThisType(componentType) ||
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
                    //var memberToggleOn = coherenceSync.GetFieldToggle(varString) ?? false;

                    if (!TypeHelpers.IsTypeSupported(fieldType) ||
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


            var schemaFilename = $"Gathered.schema";
            var schemaFullPath = $"{OutDirectory}/{schemaFilename}";

            StreamWriter schemaWriter = new StreamWriter(schemaFullPath);
            var schemaCode = CreateSchema(componentDefinitions.Values);
            schemaWriter.Write(schemaCode);
            schemaWriter.Close();

            Debug.Log($"Saving schema to '{schemaFullPath}'.");
            AssetDatabase.Refresh();
#endif
        }

        private static string CreateSchema(IEnumerable<ComponentDefinition> components)
        {
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

        public static void SaveJson(CoherenceSync[] syncers)
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
            var jsonData = new SyncedBehaviour[] {
                new SyncedBehaviour("CoherenceSyncPlayer", new string[] {"ColorizeBehaviour", "WorldPosition"}),
            };

            var writer = new StringWriter();
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, jsonData);
            return writer.ToString();
        }
    }

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

    // Used for json generation
    public struct SyncedBehaviour
    {
        public string Name;
        public string[] Components;

        public SyncedBehaviour(string name, string[] components) {
            this.Name = name;
            this.Components = components;
        }
    }
}
