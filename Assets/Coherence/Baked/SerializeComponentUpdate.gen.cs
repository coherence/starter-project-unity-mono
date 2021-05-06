


#region SerializeComponentUpdate
// -----------------------------------
//  SerializeComponentUpdate.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using System;
	using Unity.Entities;
	using Unity.Transforms;
	using global::Coherence.Generated;
	using Coherence.Replication.Protocol.Definition;
	using Replication.Client.Unity.Ecs;
    using Coherence.Replication.Unity;

    public class SerializeComponentUpdatesGenerated
    {
         private UnityWriters unityWriters;

         public SerializeComponentUpdatesGenerated(UnityMapper mapper)
         {
             unityWriters = new UnityWriters(mapper);
         }


        private void SerializeWorldPosition(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<Translation>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeWorldOrientation(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<Rotation>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeLocalUser(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<LocalUser>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeWorldPositionQuery(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<WorldPositionQuery>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeArchetypeComponent(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<ArchetypeComponent>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializePersistence(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<Persistence>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeConnectedEntity(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<ConnectedEntity>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericPrefabReference(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericPrefabReference>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericScale(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericScale>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldInt9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldBool9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldFloat9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldVector0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldVector1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldVector2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldVector3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldString0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldString1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldString2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldString3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldString4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldQuaternion0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldQuaternion0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

        private void SerializeGenericFieldEntity9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {
            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldEntity9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);
        }
        

    
        public void SerializeComponent(EntityManager entityManager, Entity unityEntity, uint componentType, uint fieldMask, IOutBitStream protocolOutStream)
        {
            switch (componentType)
            {

                case TypeIds.InternalWorldPosition:
                    SerializeWorldPosition(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalWorldOrientation:
                    SerializeWorldOrientation(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalLocalUser:
                    SerializeLocalUser(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalWorldPositionQuery:
                    SerializeWorldPositionQuery(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalArchetypeComponent:
                    SerializeArchetypeComponent(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalPersistence:
                    SerializePersistence(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalConnectedEntity:
                    SerializeConnectedEntity(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericPrefabReference:
                    SerializeGenericPrefabReference(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericScale:
                    SerializeGenericScale(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt0:
                    SerializeGenericFieldInt0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt1:
                    SerializeGenericFieldInt1(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt2:
                    SerializeGenericFieldInt2(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt3:
                    SerializeGenericFieldInt3(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt4:
                    SerializeGenericFieldInt4(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt5:
                    SerializeGenericFieldInt5(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt6:
                    SerializeGenericFieldInt6(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt7:
                    SerializeGenericFieldInt7(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt8:
                    SerializeGenericFieldInt8(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldInt9:
                    SerializeGenericFieldInt9(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool0:
                    SerializeGenericFieldBool0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool1:
                    SerializeGenericFieldBool1(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool2:
                    SerializeGenericFieldBool2(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool3:
                    SerializeGenericFieldBool3(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool4:
                    SerializeGenericFieldBool4(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool5:
                    SerializeGenericFieldBool5(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool6:
                    SerializeGenericFieldBool6(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool7:
                    SerializeGenericFieldBool7(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool8:
                    SerializeGenericFieldBool8(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldBool9:
                    SerializeGenericFieldBool9(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat0:
                    SerializeGenericFieldFloat0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat1:
                    SerializeGenericFieldFloat1(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat2:
                    SerializeGenericFieldFloat2(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat3:
                    SerializeGenericFieldFloat3(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat4:
                    SerializeGenericFieldFloat4(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat5:
                    SerializeGenericFieldFloat5(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat6:
                    SerializeGenericFieldFloat6(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat7:
                    SerializeGenericFieldFloat7(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat8:
                    SerializeGenericFieldFloat8(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldFloat9:
                    SerializeGenericFieldFloat9(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldVector0:
                    SerializeGenericFieldVector0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldVector1:
                    SerializeGenericFieldVector1(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldVector2:
                    SerializeGenericFieldVector2(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldVector3:
                    SerializeGenericFieldVector3(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldString0:
                    SerializeGenericFieldString0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldString1:
                    SerializeGenericFieldString1(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldString2:
                    SerializeGenericFieldString2(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldString3:
                    SerializeGenericFieldString3(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldString4:
                    SerializeGenericFieldString4(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldQuaternion0:
                    SerializeGenericFieldQuaternion0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity0:
                    SerializeGenericFieldEntity0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity1:
                    SerializeGenericFieldEntity1(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity2:
                    SerializeGenericFieldEntity2(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity3:
                    SerializeGenericFieldEntity3(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity4:
                    SerializeGenericFieldEntity4(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity5:
                    SerializeGenericFieldEntity5(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity6:
                    SerializeGenericFieldEntity6(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity7:
                    SerializeGenericFieldEntity7(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity8:
                    SerializeGenericFieldEntity8(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldEntity9:
                    SerializeGenericFieldEntity9(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                default:
                    throw new Exception($"unknown componentType {componentType}");
            }
        }
    }
    
    class SerializeComponentUpdatesWrapper : ISchemaSpecificComponentSerializer
    {
        private SerializeComponentUpdatesGenerated serializeComponentUpdatesGenerated;

        public SerializeComponentUpdatesWrapper(UnityMapper mapper)
        {
            serializeComponentUpdatesGenerated = new SerializeComponentUpdatesGenerated(mapper);
        }

    	public void SerializeComponent(EntityManager entityManager, Entity unityEntity, uint ComponentTypeId, uint fieldMask, IOutBitStream protocolOutStream)
    	{
    		serializeComponentUpdatesGenerated.SerializeComponent(entityManager, unityEntity, ComponentTypeId, fieldMask, protocolOutStream);
    	}
    }

}


// ------------------ end of SerializeComponentUpdate.cs -----------------
#endregion
