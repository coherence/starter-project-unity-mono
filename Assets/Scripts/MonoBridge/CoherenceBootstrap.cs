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
        private EntityQuery entityQuery;
        
        void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityQuery = entityManager.CreateEntityQuery(typeof(Player), typeof(Translation), ComponentType.Exclude<CoherenceSimulateComponent>());

            StartCoroutine(CheckForNewEntities());
            StartCoroutine(CleanUpDeletedEntities());
        }

        IEnumerator CleanUpDeletedEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                // TODO: add functionality
            }
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
                        SpawnEntity(entities, i);
                        break;
                    }
                }

                entities.Dispose();
            }
        }

        private void SpawnEntity(NativeArray<Entity> entities, int i)
        {
            Debug.Log("Creating mono representation for remote entity " + entities[i]);

            var prefabName = entityManager.GetComponentData<Player>(entities[i]).prefab;
            Debug.Log("Instatiating prefab " + prefabName);
            var resource = Resources.Load(prefabName.ToString());

            var newEntity = (GameObject) GameObject.Instantiate(resource);
            var sync = newEntity.GetComponent<CoherenceSync>();
            sync.SpawnFromNetwork(entities[i], "Network: " + entities[i]);
        }
    }
}