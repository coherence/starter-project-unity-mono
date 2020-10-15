using Coherence.Generated.FirstProject;
using Coherence.Replication.Client.Unity.Ecs;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Coherence.MonoBridge
{
    public class CoherenceSync_Player : CoherenceSync
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
        
        public delegate void CollisionCommandHandler(object sender, GenericNetworkCommandArgs e);
        public event CollisionCommandHandler CollisionCommandReceived;

        private ShowNameAndState showNameAndState;

        protected override void InitialiseEntity()
        {
            entity = entityManager.CreateEntity(typeof(Translation), typeof(Rotation),
                typeof(CoherenceSessionComponent), typeof(CoherenceSimulateComponent), typeof(GenericPrefabReference),
                typeof(GenericCommand));

            entityManager.AddComponent<GenericFieldInt0>(entity);
            entityManager.AddComponent<GenericFieldString0>(entity);
        }

        private void ReceiveCollisionCommand()
        {
            var buffer = entityManager.GetBuffer<GenericCommand>(entity);

            foreach (var cmd in buffer)
            {
                ProcessCollisionCommand(cmd);
            }

            buffer.Clear();
        }

        private void ProcessCollisionCommand(GenericCommand cmd)
        {
            var args = new GenericNetworkCommandArgs
            {
                Name = cmd.name.ToString(),
                ParamInt1 = cmd.paramInt1,
            };

            CollisionCommandReceived?.Invoke(this, args);
        }

        public void SendCollisionCommand(CoherenceSync sender)
        {
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

        protected override void ReceiveNetworkCommands()
        {
            ReceiveCollisionCommand();
        }
        
        protected override void SyncEcsBaked()
        {
            if (isSimulated)
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
                    name = TrimString64(showNameAndState.PlayerName)
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
        
        protected new void Awake()
        {
            base.Awake();
            showNameAndState = GetComponent<ShowNameAndState>();
        }

        protected new void Update()
        {
            base.Update();
        }
    }
}