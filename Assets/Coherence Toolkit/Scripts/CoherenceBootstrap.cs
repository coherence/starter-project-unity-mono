namespace Coherence.MonoBridge
{
    using Coherence.Generated.FirstProject;
    using Coherence.Replication.Client.Unity.Ecs;
    using System.Collections.Generic;
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

        private Dictionary<Entity, CoherenceSync> mapper = new Dictionary<Entity, CoherenceSync>();
        private List<Entity> toDestroy = new List<Entity>();

        private void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            entityQueryRemote = entityManager.CreateEntityQuery(
                typeof(GenericPrefabReference),
                typeof(Translation),
                ComponentType.Exclude<Simulated>());

            entityQueryLocal = entityManager.CreateEntityQuery(
                typeof(GenericPrefabReference),
                typeof(Translation),
                typeof(Simulated));
        }

        private void LateUpdate()
        {
            CleanUpDeletedEntities();
            CheckForNewLocalEntities();
            CheckForNewNetworkedEntities();
        }

        private void Log(object message, Object context = null)
        {
            if (!debugMode)
            {
                return;
            }

            Debug.Log($"<color=brown>[Coherence Bootstrap]</color> {message}", context);
        }

        private void LogError(object message, Object context = null)
        {
            if (!debugMode)
            {
                return;
            }

            Debug.LogError($"<color=brown>[Coherence Bootstrap]</color> {message}", context);
        }

        private void CleanUpDeletedEntities()
        {
            toDestroy.Clear();

            foreach (KeyValuePair<Entity, CoherenceSync> pair in mapper)
            {
                Entity entity = pair.Key;
                CoherenceSync sync = pair.Value;

                if (sync && sync.enabled)
                {
                    continue;
                }

                if (!sync)
                {
                    Log($"Destroying {entity}: linked CoherenceSync was destroyed.");
                }

                if (sync && !sync.enabled)
                {
                    Log($"Destroying {entity}: linked CoherenceSync was disabled.", sync);
                }

                entityManager.DestroyEntity(entity);
                toDestroy.Add(entity);
            }

            foreach (Entity e in toDestroy)
            {
                _ = mapper.Remove(e);
            }
        }

        private void CheckForNewLocalEntities()
        {
            NativeArray<Entity> entities = entityQueryLocal.ToEntityArray(Allocator.TempJob);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];

                foreach (CoherenceSync sync in CoherenceSync.instances)
                {
                    if (!sync.isSimulated)
                    {
                        continue;
                    }

                    if (sync.LinkedEntity == Entity.Null)
                    {
                        continue;
                    }

                    if (sync.LinkedEntity == entity)
                    {
                        if (!mapper.ContainsKey(entity))
                        {
                            Log($"Linking: {entity} -> {sync}", sync);
                            mapper[entities[i]] = sync;
                        }
                    }
                }
            }

            entities.Dispose();
        }

        private void CheckForNewNetworkedEntities()
        {
            NativeArray<Entity> entities = entityQueryRemote.ToEntityArray(Allocator.TempJob);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];

                if (entityManager.HasComponent<Simulated>(entities[i]))
                {
                    continue;
                }

                bool entityAlreadyExists = false;
                foreach (CoherenceSync sync in CoherenceSync.instances)
                {
                    if (sync.LinkedEntity == entity)
                    {
                        entityAlreadyExists = true;
                        break;
                    }
                }

                if (!entityAlreadyExists)
                {
                    mapper[entity] = SpawnEntity(entity);
                    break;
                }
            }

            entities.Dispose();
        }

        public CoherenceSync SpawnEntity(Entity entity)
        {
            FixedString64 prefabName = entityManager.GetComponentData<GenericPrefabReference>(entity).prefab;

            CoherenceSync syncPrefab = Resources.Load<CoherenceSync>(prefabName.ToString());
            if (!syncPrefab)
            {
                LogError($"Could not find Resource '{prefabName}' for entity " + entity);
            }
            CoherenceSync sync = Instantiate(syncPrefab);

            Log($"Instantiated Resource '{prefabName}' for entity " + entity, sync);

            sync.SpawnFromNetwork(entity, "Network: " + entity);

            return sync;
        }
    }
}
