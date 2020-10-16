namespace Coherence.MonoBridge
{
    using Coherence.Generated.FirstProject;
    using Coherence.Replication.Client.Unity.Ecs;
    using System.Collections;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Transforms;
    using UnityEngine;

    public class CoherenceBootstrap : MonoBehaviour
    {
        public string schemaNamespace = "Coherence.Generated.FirstProject.";
        public bool debugMode = true;
        
        private EntityManager entityManager;
        private EntityQuery entityQueryRemote;
        private EntityQuery entityQueryLocal;
        private Hashtable entityMap = new Hashtable();

        private void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityQueryRemote = entityManager.CreateEntityQuery(typeof(GenericPrefabReference), typeof(Translation), ComponentType.Exclude<CoherenceSimulateComponent>());
            entityQueryLocal = entityManager.CreateEntityQuery(typeof(GenericPrefabReference), typeof(Translation), typeof(CoherenceSimulateComponent));

            _ = StartCoroutine(CheckForNewNetworkedEntities());
            _ = StartCoroutine(CheckForNewLocalEntities());
            _ = StartCoroutine(CleanUpDeletedEntities());
        }

        private IEnumerator CleanUpDeletedEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                ArrayList toDelete = new ArrayList();
                foreach (DictionaryEntry entry in entityMap)
                {
                    GameObject go = (GameObject)entry.Value;

                    if (go != null && go.activeInHierarchy) continue;
                    
                    if(debugMode) Debug.Log($"Entity {(Entity)entry.Key} no longer exists. Destroying entity.");
                    
                    entityManager.DestroyEntity((Entity)entry.Key);
                    _ = toDelete.Add((Entity)entry.Key);
                }

                foreach (object td in toDelete)
                {
                    entityMap.Remove((Entity)td);
                }
            }
        }

        private IEnumerator CheckForNewLocalEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                NativeArray<Entity> entities = entityQueryLocal.ToEntityArray(Allocator.TempJob);

                for (int i = 0; i < entities.Length; i++)
                {
                    CoherenceSync[] gameObjects = FindObjectsOfType<CoherenceSync>();

                    foreach (CoherenceSync go in gameObjects)
                    {
                        Entity entity = go.LinkedEntity;
                        if (entity == Entity.Null)
                        {
                            continue;
                        }

                        if (!go.isSimulated)
                        {
                            continue;
                        }

                        if (entity == entities[i])
                        {
                            if (!entityMap.Contains(entities[i]))
                            {
                                if(debugMode) Debug.Log($"Found new entity locally: {entities[i]}->{go}");
                                entityMap[entities[i]] = go.gameObject;
                            }
                        }
                    }
                }

                entities.Dispose();
            }
        }

        private IEnumerator CheckForNewNetworkedEntities()
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

                    CoherenceSync[] gameObjects = FindObjectsOfType<CoherenceSync>();

                    foreach (CoherenceSync go in gameObjects)
                    {
                        Entity entity = go.LinkedEntity;

                        if (entity == entities[i])
                        {
                            entityAlreadyExists = true;

                            break;
                        }
                    }

                    if (!entityAlreadyExists)
                    {
                        GameObject ent = SpawnEntity(entities[i]);
                        entityMap[entities[i]] = ent;
                        break;
                    }
                }

                entities.Dispose();
            }
        }

        private GameObject SpawnEntity(Entity entity)
        {
            if(debugMode) Debug.Log("Creating mono representation for remote entity " + entity);

            FixedString64 prefabName = entityManager.GetComponentData<GenericPrefabReference>(entity).prefab;
            if(debugMode) Debug.Log("Instantiating prefab " + prefabName);
            Object resource = Resources.Load(prefabName.ToString());

            GameObject newEntity = (GameObject)Instantiate(resource);
            CoherenceSync sync = newEntity.GetComponent<CoherenceSync>();
            sync.SpawnFromNetwork(entity, "Network: " + entity);

            return newEntity;
        }
    }
}