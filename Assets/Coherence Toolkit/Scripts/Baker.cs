namespace Coherence.MonoBridge
{
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEditor;

    public class Baker
    {
        public static void SaveSyncBehaviour(string gameObjectName)
        {
            var className = $"CoherenceSync{gameObjectName}";
            var generatedMonoBehaviourCode = InstantiateTemplate(className);
            var fileName = $"{className}.cs";
            var outDirectory = $"{Application.dataPath}/Schemas";
            var outFilePath = $"{outDirectory}/{fileName}";

            StreamWriter writer = new StreamWriter(outFilePath);
            writer.Write(generatedMonoBehaviourCode);
            writer.Close();

            Debug.Log($"Saving baked file to '{outFilePath}', generated code: {generatedMonoBehaviourCode}");

            AssetDatabase.Refresh();
        }

        public static string InstantiateTemplate(string className)
        {
            return $@"
using Coherence.Generated.FirstProject;
using Coherence.Replication.Client.Unity.Ecs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Coherence.MonoBridge
{{
    public class {className} : MonoBehaviour
    {{
        private CoherenceSync coherenceSync;
        private EntityManager entityManager;
        private bool componentsInitialized = false;

        protected void Awake()
        {{
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            coherenceSync = GetComponent<CoherenceSync>();
            coherenceSync.usingReflection = false;
        }}

        private void InitializeComponents()
        {{
            if (!coherenceSync.isSimulated) return;

            var entity = coherenceSync.LinkedEntity;

            entityManager.AddComponent<Translation>(entity);
            entityManager.AddComponent<Rotation>(entity);
            entityManager.AddComponent<SessionBased>(entity);
            entityManager.AddComponent<Simulated>(entity);

            componentsInitialized = true;
        }}

        void Update()
        {{
            if (!coherenceSync.EcsEntityExists())
            {{
                return;
            }}

            if (!componentsInitialized)
            {{
                InitializeComponents();
            }}

            SyncEcsBaked();
        }}

        private void SyncEcsBaked()
        {{
            var entity = coherenceSync.LinkedEntity;

            if (coherenceSync.isSimulated)
            {{
                var position = transform.position;
                entityManager.SetComponentData(entity, new Translation()
                {{
                    Value = new float3(position.x, position.y, position.z)
                }});

                var rotation = transform.rotation;
                entityManager.SetComponentData(entity, new Rotation()
                {{
                    Value = new quaternion(rotation.x, rotation.y, rotation.z, rotation.w)
                }});
            }}
            else
            {{
                var translation = entityManager.GetComponentData<Translation>(entity);
                var rotation = entityManager.GetComponentData<Rotation>(entity);

                var tr = transform;

                tr.position = new Vector3(translation.Value.x,
                                          translation.Value.y,
                                          translation.Value.z);

                tr.rotation = new Quaternion(rotation.Value.value.x,
                                             rotation.Value.value.y,
                                             rotation.Value.value.z,
                                             rotation.Value.value.w);
            }}
        }}
    }}
}}
";
        }
    }
}