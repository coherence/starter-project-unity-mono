using System.Collections;
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
        private EntityQuery entityQueryRemote;
        private EntityQuery entityQueryLocal;
        
        private Hashtable entityMap = new Hashtable();
        
        void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityQueryRemote = entityManager.CreateEntityQuery(typeof(Player), typeof(Translation), ComponentType.Exclude<CoherenceSimulateComponent>());
            entityQueryLocal = entityManager.CreateEntityQuery(typeof(Player), typeof(Translation), typeof(CoherenceSimulateComponent));

            StartCoroutine(CheckForNewNetworkedEntities());
            StartCoroutine(CheckForNewLocalEntities());
            StartCoroutine(CleanUpDeletedEntities());
        }

        IEnumerator CleanUpDeletedEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                ArrayList toDelete = new ArrayList();
                foreach (DictionaryEntry entry in entityMap)
                {
                    var go = (GameObject)entry.Value;

                    if (go == null || !go.activeInHierarchy)
                    {
                        Debug.Log($"Entity {(Entity)entry.Key} no longer exists. Destroying entity.");
                        entityManager.DestroyEntity((Entity)entry.Key);
                        toDelete.Add((Entity) entry.Key);
                    }
                }

                foreach (var td in toDelete)
                {
                    entityMap.Remove((Entity)td);
                }
            }
        }
        
        IEnumerator CheckForNewLocalEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                NativeArray<Entity> entities = entityQueryLocal.ToEntityArray(Allocator.TempJob);

                for (int i = 0; i < entities.Length; i++)
                {
                    var gameObjects = GameObject.FindObjectsOfType<CoherenceSync>();

                    foreach (var go in gameObjects)
                    {
                        var entity = go.LinkedEntity;
                        if (entity == Entity.Null) continue;
                        if (!go.isSimulated) continue;
                        
                        if (entity == entities[i])
                        {
                            if (!entityMap.Contains(entities[i]))
                            {
                                Debug.Log($"Found new entity locally: {entities[i]}->{go}");
                                entityMap[entities[i]] = go.gameObject;
                            }
                        }
                    }
                }

                entities.Dispose();
            }
        }
        
        IEnumerator CheckForNewNetworkedEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                NativeArray<Entity> entities = entityQueryRemote.ToEntityArray(Allocator.TempJob);

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
                        var ent = SpawnEntity(entities[i]);
                        entityMap[entities[i]] = ent;
                        break;
                    }
                }

                entities.Dispose();
            }
        }

        private GameObject SpawnEntity(Entity entity)
        {
            Debug.Log("Creating mono representation for remote entity " + entity);

            var prefabName = entityManager.GetComponentData<Player>(entity).prefab;
            Debug.Log("Instantiating prefab " + prefabName);
            var resource = Resources.Load(prefabName.ToString());

            var newEntity = (GameObject) GameObject.Instantiate(resource);
            var sync = newEntity.GetComponent<CoherenceSync>();
            sync.SpawnFromNetwork(entity, "Network: " + entity);

            return newEntity;
        }
    }
}