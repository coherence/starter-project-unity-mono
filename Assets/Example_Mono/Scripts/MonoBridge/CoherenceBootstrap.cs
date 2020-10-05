using System.Collections;
using System.Collections.Generic;
using Coherence.Generated.FirstProject;
using Coherence.Replication.Client.Unity.Ecs;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
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

            entityQuery = entityManager.CreateEntityQuery(typeof(Player), typeof(Translation), ComponentType.Exclude<CoherenceSimulateComponent>());

            StartCoroutine(CheckForNewEntities());
        }
        
        IEnumerator CheckForNewEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.TempJob);

                for (int i = 0; i < entities.Length; i++)
                {
                    if (entityManager.HasComponent<CoherenceSimulateComponent>(entities[i]))
                    {
                        continue;
                    }
                    
                    bool entityAlreadyExists = false;

                    var gameObjects = GameObject.FindObjectsOfType<CoherenceSync>();

                    foreach (var go in gameObjects)
                    {
                        var entity = go.LinkedEntity;

                        if (entity == entities[i])
                        {
                            entityAlreadyExists = true;
                            break;
                        }
                    }

                    if (!entityAlreadyExists)
                    {
                        Debug.Log("Creating mono representation for remote entity " + entities[i]);

                        var newEntity = GameObject.Instantiate(prefab);
                        newEntity.name = "Network: " + entities[i].ToString();
                        var sync = newEntity.GetComponent<CoherenceSync>();
                        sync.IsSimulated = false;
                        sync.LinkedEntity = entities[i];
                        break;
                    }
                }

                entities.Dispose();
            }
        }
    }
}