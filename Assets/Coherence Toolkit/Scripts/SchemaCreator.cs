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

        // TODO: We need to pass in a LIST of sync behaviours that we've gathered here!
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
            // Each 'Syncedbehaviour' here will result in a separate 'CoherenceSync???.cs'
            // script generated by the protocol-code-generator.
            var jsonData = new List<SyncedBehaviour>();

            // Each GO/Prefab with a CoherenceSync script will result in one CoherenceSyncNAME
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
                       !componentToggleOn ||
                       componentType == typeof(Transform))
                    {
                        continue;
                    }

                    var members = TypeHelpers.Members(componentType);
                    var syncTheseMembers = new List<string>();

                    foreach (MemberInfo variable in members)
                    {
                        var fieldType = TypeHelpers.GetUnderlyingType(variable);
                        var varString = componentTypeString + CoherenceSync.KeyDelimiter + variable.Name;
                        var memberToggleOn = coherenceSync.GetFieldToggle(varString) ?? false;

                        if (!TypeHelpers.IsTypeSupported(fieldType) ||
                            !memberToggleOn ||
                            TypeHelpers.IsMonoMember(variable.GetType()))
                        {
                            continue;
                        }

                        syncTheseMembers.Add(variable.Name);
                    }

                    var componentName = componentType.ToString();
                    var syncedComponent = new SyncedComponent(componentName, syncTheseMembers.ToArray());
                    syncTheseComponents.Add(syncedComponent);
                }

                var className = $"CoherenceSync{coherenceSync.name}";
                jsonData.Add(new SyncedBehaviour(className, syncTheseComponents.ToArray()));
            }

            var writer = new StringWriter();
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(writer, jsonData.ToArray());
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
}
