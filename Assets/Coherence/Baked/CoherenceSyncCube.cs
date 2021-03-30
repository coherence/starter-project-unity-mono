


#region CoherenceSyncCube
// -----------------------------------
//  CoherenceSyncCube.cs
// -----------------------------------
			
namespace Coherence.Generated
{
    using UnityEngine;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using System;
    using System.Reflection;
    using Coherence.Toolkit;
    using Coherence.Replication.Client.Unity.Ecs;
    using static Coherence.Toolkit.CoherenceSync;
    using global::Coherence.Generated.Internal;
    using System.Linq;

    public class CoherenceSyncCube : CoherenceSyncBaked
    {
        private CoherenceSync coherenceSync;
        private EntityManager entityManager;
        private bool componentsInitialized = false;

        // Cached references to MonoBehaviours on this GameObject
        private UnityEngine.Transform _unityengine_transform;
        private Cube _cube;

        protected void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            coherenceSync = GetComponent<CoherenceSync>();
            coherenceSync.usingReflection = false;
            _unityengine_transform = GetComponent<UnityEngine.Transform>();
            _cube = GetComponent<Cube>();

            coherenceSync.OnSpawnFromNetwork += OnSpawnFromNetwork;
        }

        private void OnSpawnFromNetwork()
        {
            InitializeComponents();
            SyncEcsBaked();
        }

        public override void InitializeComponents()
        {
            var entity = coherenceSync.LinkedEntity;

            if (coherenceSync.HasArchetype) 
            {
                entityManager.AddComponent<LastObservedLod>(entity);
            }            

            if (!coherenceSync.isSimulated) 
            {
                return;
            }

            entityManager.AddComponent<Translation>(entity);
            entityManager.AddComponent<Rotation>(entity);
            entityManager.AddComponent<Cube_Cube>(entity);

            if (coherenceSync.HasArchetype)
            {
                int archetypeIndex = Archetype.IndexForName[coherenceSync.Archetype.ArchetypeName];
                entityManager.AddComponentData(entity, new ArchetypeComponent { index = archetypeIndex });
            }

            if (coherenceSync.lifetimeType == CoherenceSync.LifetimeType.Persistent)
            {
                entityManager.AddComponentData(entity, new Persistence()
                {
                    uuid = coherenceSync.persistenceUUID,
                    expiry = coherenceSync.GetPersistenceExpiryString()
                });
            }

            if (coherenceSync.authorityTransferType != CoherenceSync.AuthorityTransferType.NotTransferable)
            {
                entityManager.AddComponent<AuthorityTransfer>(entity);
            }

            entityManager.AddComponent<Simulated>(entity);

            componentsInitialized = true;
        }

        void Update()
        {
            if (!coherenceSync.EcsEntityExists())
            {
                return;
            }

            if (!componentsInitialized)
            {
                InitializeComponents();
            }

            SyncEcsBaked();
        }

        static FixedString64 ObjectToFixedString64(object o)
        {
            return new FixedString64((string)o);
        }

        static float3 Vector3ToFloat(object o)
        {
            Vector3 v = (Vector3)o;
            return new float3(v.x, v.y, v.z);
        }

        static Entity ObjectToEntity(GameObject from)
        {
            var fromSync = from.GetComponent<CoherenceSync>();

            if(fromSync == null)
            {
                return default;
            }

            return fromSync.entity;
        }

        static GameObject EntityToObject(Entity from)
        {
            var toSync = CoherenceMonoBridge.Instance?.GetCoherenceSyncForEntity(from);
            return toSync?.gameObject;
        }

        private void SyncEcsBaked()
        {
            var entity = coherenceSync.LinkedEntity;

            if (coherenceSync.isSimulated)
            {
                entityManager.SetComponentData(entity, new Translation() 
                {
                    Value = _unityengine_transform.position,
                });
                entityManager.SetComponentData(entity, new Rotation() 
                {
                    Value = _unityengine_transform.rotation,
                });
                entityManager.SetComponentData(entity, new Cube_Cube() 
                {
                    friend = ObjectToEntity(_cube.friend),
                    s = _cube.s?? "",
                    i = _cube.i,
                });
            }
            else
            {
                if (entityManager.HasComponent<Translation>(entity)) 
                {
                    var data = entityManager.GetComponentData<Translation>(entity);
                    _unityengine_transform.position = data.Value; // float3
                }
                if (entityManager.HasComponent<Rotation>(entity)) 
                {
                    var data = entityManager.GetComponentData<Rotation>(entity);
                    _unityengine_transform.rotation = data.Value; // quaternion
                }
                if (entityManager.HasComponent<Cube_Cube>(entity)) 
                {
                    var data = entityManager.GetComponentData<Cube_Cube>(entity);
                    _cube.friend = EntityToObject(data.friend); // Entity
                    _cube.s = data.s.ToString(); // FixedString64
                    _cube.i = data.i; // int
                }

                if (coherenceSync.HasArchetype) 
                {
                    int level = entityManager.GetComponentData<LastObservedLod>(entity).Level;
                    coherenceSync.Archetype.SetObservedLodLevel(level);
                }
            }
        }
    }
}

// ------------------ end of CoherenceSyncCube.cs -----------------
#endregion
