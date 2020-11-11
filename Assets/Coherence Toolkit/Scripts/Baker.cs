namespace Coherence.MonoBridge
{
    using System.IO;
    using UnityEngine;
    using UnityEditor;

    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class Baker
    {
        public static void SaveSyncBehaviour(string gameObjectName)
        {
#if UNITY_EDITOR
            var componentNames = new string[]{
                "Translation",
                "Rotation",
                "SessionBased",
                "Simulated"
            };

            var className = $"CoherenceSync{gameObjectName}";

            var filename = $"Baked.gen.schema";
            var outDirectory = $"{Application.dataPath}/Schemas";
            var outFilePath = $"{outDirectory}/{filename}";

            StreamWriter writer = new StreamWriter(outFilePath);
            var schemaCode = CreateBakedSchema(componentNames);
            writer.Write(schemaCode);
            writer.Close();

            Debug.Log($"Saving baked schema to '{outFilePath}', generated code: {schemaCode}");

            AssetDatabase.Refresh();
#endif
        }

        private static string CreateBakedSchema(string[] componentNames) {

            var header =
@"name Schema

namespace Coherence.Generated.FirstProject";

            foreach(var name in componentNames)
            {

            }

            return header;
        }
    }
}
