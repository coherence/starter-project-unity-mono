using System;
using Coherence.Generated.FirstProject;
using Coherence.Replication.Client.Unity.Ecs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Coherence.MonoBridge
{
    public class CoherenceSyncPlayer : MonoBehaviour
    {
        /*
         *  This is sample generated sync code that should be used as a template when modifying
         *  the protocol code generator.
         *
         *  Please note that here Generic components are used for convenience.
         *
         *  In the actual baked file, please use the concrete component and command names and values.
         *
         */

        public class CollisionCommandArgs : EventArgs
        {
            public string Name { get; set; }
            public int ParamInt1 { get; set; }

            public override string ToString()
            {
                {
                    return "{Name} {ParamInt1}";
                }
            }
        }

        public delegate void CollisionCommandHandler(object sender, CoherenceSync.GenericNetworkCommandArgs e);
        public event CollisionCommandHandler CollisionCommandReceived;

        private CoherenceSync coherenceSync;
        private ShowNameAndState showNameAndState;
        private EntityManager entityManager;

        private bool componentsInitialized = false;

        protected void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            showNameAndState = GetComponent<ShowNameAndState>();
            coherenceSync = GetComponent<CoherenceSync>();
            coherenceSync.usingReflection = false;
        }

        private void InitializeComponents()
        {
            if (!coherenceSync.isSimulated) return;

            var entity = coherenceSync.LinkedEntity;

            entityManager.AddComponent<Translation>(entity);
            entityManager.AddComponent<Rotation>(entity);
            entityManager.AddComponent<SessionBased>(entity);
            entityManager.AddComponent<Simulated>(entity);
            entityManager.AddComponent<GenericCommand>(entity);
            entityManager.AddComponent<GenericFieldInt0>(entity);
            entityManager.AddComponent<GenericFieldString0>(entity);

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

            ReceiveCollisionCommand();
            SyncEcsBaked();
        }

        private void ReceiveCollisionCommand()
        {
            var entity = coherenceSync.LinkedEntity;
            var buffer = entityManager.GetBuffer<GenericCommand>(entity);

            foreach (var cmd in buffer)
            {
                ProcessCollisionCommand(cmd);
            }

            buffer.Clear();
        }

        private void ProcessCollisionCommand(GenericCommand cmd)
        {
            var args = new CoherenceSync.GenericNetworkCommandArgs
            {
                Name = cmd.name.ToString(),
                ParamInt1 = cmd.paramInt1,
            };

            CollisionCommandReceived?.Invoke(this, args);
        }

        public void SendCollisionCommand(CoherenceSync sender)
        {
            var entity = coherenceSync.LinkedEntity;
            if (entity == Entity.Null) return;

            var cmd = new GenericCommandRequest
            {
                name = null,
                paramInt1 = 1,
            };

            if (entityManager.HasComponent<GenericCommandRequest>(entity))
            {
                var cmdRequestBuffer = entityManager.GetBuffer<GenericCommandRequest>(entity);
                _ = cmdRequestBuffer.Add(cmd);
            }
            else
            {
                this.ProcessCollisionCommand(new GenericCommand()
                {
                    name = cmd.name,
                    paramInt1 = cmd.paramInt1
                });
            }
        }

        private void SyncEcsBaked()
        {
            var entity = coherenceSync.LinkedEntity;

            if (coherenceSync.isSimulated)
            {
                var position = transform.position;
                entityManager.SetComponentData(entity, new Translation()
                {
                    Value = new float3(position.x, position.y, position.z)
                });

                var rotation = transform.rotation;
                entityManager.SetComponentData(entity, new Rotation()
                {
                    Value = new quaternion(rotation.x, rotation.y, rotation.z, rotation.w)
                });

                entityManager.SetComponentData(entity, new GenericFieldString0()
                {
                    name = CoherenceSync.TrimString64(showNameAndState.PlayerName)
                });

                entityManager.SetComponentData(entity, new GenericFieldInt0()
                {
                    number = showNameAndState.State
                });
            }
            else
            {
                var translation = entityManager.GetComponentData<Translation>(entity);
                var rotation = entityManager.GetComponentData<Rotation>(entity);
                var genericFieldString0 = entityManager.GetComponentData<GenericFieldString0>(entity);
                var genericFieldInt0 = entityManager.GetComponentData<GenericFieldInt0>(entity);

                var tr = transform;

                tr.position = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                tr.rotation = new Quaternion(rotation.Value.value.x, rotation.Value.value.y,rotation.Value.value.z, rotation.Value.value.w);
                showNameAndState.name = genericFieldString0.name.ToString();
                showNameAndState.State = genericFieldInt0.number;
            }
        }
    }
}
