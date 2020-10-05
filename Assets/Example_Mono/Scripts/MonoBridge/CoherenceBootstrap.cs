using Coherence.Generated.FirstProject;
using Coherence.Replication.Client.Unity.Ecs;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Coherence.MonoBridge
{
    public class CoherenceBootstrap : MonoBehaviour
    {
        private EntityManager entityManager;
        public Transform prefab;
        private EntityQuery entityQuery;
        
        void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityQuery = entityManager.CreateEntityQuery(typeof(Player), ComponentType.Exclude<CoherenceSimulateComponent>());
        }
        
        void Update()
        {
            NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.TempJob);

            for (int i = 0; i < entities.Length; i++)
            {
                bool entityAlreadyExists = false;
                
                var gameObjects = GameObject.FindObjectsOfType<CoherenceSync>();

                foreach (var go in gameObjects)
                {
                    var entity = go.entity;

                    if (entity == entities[i])
                    {
                        entityAlreadyExists = true;
                        break;
                    }
                }

                if (!entityAlreadyExists)
                {
                    var newEntity = GameObject.Instantiate(prefab);
                    var sync = prefab.GetComponent<CoherenceSync>();
                    sync.entity = entities[i];
                }
            }

            entities.Dispose();
        }
    }
}