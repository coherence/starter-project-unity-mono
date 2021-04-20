


#region CoherenceSyncPlayer
// -----------------------------------
//  CoherenceSyncPlayer.cs
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
    using Coherence.Ecs;

    public class CoherenceSyncPlayer : CoherenceSyncBaked
    {
        private CoherenceSync coherenceSync;
        private EntityManager entityManager;
        private bool componentsInitialized = false;

        // Cached references to MonoBehaviours on this GameObject
        private UnityEngine.Transform _unityengine_transform;
        private Controller _controller;

        protected void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            coherenceSync = GetComponent<CoherenceSync>();
            coherenceSync.usingReflection = false;
            _unityengine_transform = GetComponent<UnityEngine.Transform>();
            _controller = GetComponent<Controller>();
            
            coherenceSync.AddCommandRequestDelegate("Controller.Foo", SendCommand_Player_Controller_Foo);

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
            entityManager.AddComponent<Player_Controller>(entity);

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
            ReceiveCommand_Player_Controller_Foo();

            SyncEcsBaked();
        }
        void SendCommand_Player_Controller_Foo(object[] args)
        {
            var entity = coherenceSync.LinkedEntity;

            if(!entityManager.HasComponent<Player_Controller_FooRequest>(entity))
            {
                Debug.LogError($"Can't send command to {name}, it hasn't got a 'Player_Controller_FooRequest' component.");
            }

            var specificCommandRequest = new Player_Controller_FooRequest();
            int i = 0;
            specificCommandRequest.g = (SerializeEntityID)(args[i++]);

            var buffer = entityManager.GetBuffer<Player_Controller_FooRequest>(entity);
            buffer.Add(specificCommandRequest);
        }

        void ReceiveCommand_Player_Controller_Foo()
        {
            var entity = coherenceSync.LinkedEntity;

            if(!entityManager.HasComponent<Player_Controller_Foo>(entity))
            {
                return;
            }

            var buffer = entityManager.GetBuffer<Player_Controller_Foo>(entity);
            var target = GetComponent<Controller>();

            foreach (var cmd in buffer)
            {
                target.Foo(CoherenceMonoBridge.EntityIdToGameObject(cmd.g));
            }

            buffer.Clear();
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

        private void SyncEcsBaked()
        {
            var entity = coherenceSync.LinkedEntity;

            if (coherenceSync.isSimulated)
            {
                entityManager.SetComponentData(entity, new Translation() 
                {
                    Value = (_unityengine_transform.position),
                });
                entityManager.SetComponentData(entity, new Player_Controller() 
                {
                    otherPlayer = CoherenceMonoBridge.UnityObjectToEntityId(_controller.otherPlayer),
                    otherPlayerTransform = CoherenceMonoBridge.UnityObjectToEntityId(_controller.otherPlayerTransform),
                    otherPlayerSync = CoherenceMonoBridge.UnityObjectToEntityId(_controller.otherPlayerSync),
                });
            }
            else
            {
                if (entityManager.HasComponent<Translation>(entity)) 
                {
                    var data = entityManager.GetComponentData<Translation>(entity);
                    _unityengine_transform.position = (data.Value);
                }
                if (entityManager.HasComponent<Player_Controller>(entity)) 
                {
                    var data = entityManager.GetComponentData<Player_Controller>(entity);
                    _controller.otherPlayer = (GameObject)CoherenceMonoBridge.EntityIdToGameObject(data.otherPlayer);
                    _controller.otherPlayerTransform = (Transform)CoherenceMonoBridge.EntityIdToTransform(data.otherPlayerTransform);
                    _controller.otherPlayerSync = (CoherenceSync)CoherenceMonoBridge.EntityIdToCoherenceSync(data.otherPlayerSync);
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

// ------------------ end of CoherenceSyncPlayer.cs -----------------
#endregion
