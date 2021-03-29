


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

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<WorldPosition_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeWorldOrientation(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<Rotation>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<WorldOrientation_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeLocalUser(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<LocalUser>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<LocalUser_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeWorldPositionQuery(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<WorldPositionQuery>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<WorldPositionQuery_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeArchetypeComponent(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<ArchetypeComponent>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<ArchetypeComponent_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializePersistence(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<Persistence>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<Persistence_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericPrefabReference(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericPrefabReference>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericPrefabReference_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericScale(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericScale>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericScale_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt0_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt1_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt2_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt3_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt4_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt5_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt6_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt7_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt8_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldInt9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldInt9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldInt9_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool0_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool1_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool2_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool3_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool4_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool5_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool6_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool7_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool8_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldBool9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldBool9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldBool9_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat0_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat1_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat2_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat3_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat4_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat5(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat5>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat5_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat6(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat6>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat6_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat7(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat7>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat7_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat8(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat8>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat8_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldFloat9(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldFloat9>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldFloat9_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldVector0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldVector0_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldVector1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldVector1_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldVector2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldVector2_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldVector3(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldVector3>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldVector3_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldString0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldString0_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldString1(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString1>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldString1_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldString2(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString2>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldString2_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldString4(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldString4>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldString4_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
        }
        

        private void SerializeGenericFieldQuaternion0(EntityManager EntityManager, Entity entity, uint mask, IOutBitStream protocolOutStream)
        {

            // Write component changes to output stream
            var componentData = EntityManager.GetComponentData<GenericFieldQuaternion0>(entity);
            unityWriters.Write(componentData, mask, protocolOutStream);

            // Reset accumulated priority so the same component is not sent again next frame
            var syncData = EntityManager.GetComponentData<GenericFieldQuaternion0_Sync>(entity);

            syncData.accumulatedPriority = 0;

            syncData.lastSentData = componentData;

            syncData.hasBeenSerialized = true;
            syncData.resendMask &= ~mask;	// Clear serialized fields from resend mask
            EntityManager.SetComponentData(entity, syncData);
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

                case TypeIds.InternalGenericFieldString4:
                    SerializeGenericFieldString4(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                case TypeIds.InternalGenericFieldQuaternion0:
                    SerializeGenericFieldQuaternion0(entityManager, unityEntity, fieldMask, protocolOutStream);
                    break;

                default:
                    throw new Exception($"unknown componentType {componentType}");
            }
        }
    
        
        public void UpdateDestructState(EntityManager entityManager, Entity unityEntity, uint componentTypeId)
        {
            switch (componentTypeId)
            {

                case TypeIds.InternalWorldPosition:
                {
                    var syncData = entityManager.GetComponentData<WorldPosition_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalWorldOrientation:
                {
                    var syncData = entityManager.GetComponentData<WorldOrientation_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalLocalUser:
                {
                    var syncData = entityManager.GetComponentData<LocalUser_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalWorldPositionQuery:
                {
                    var syncData = entityManager.GetComponentData<WorldPositionQuery_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalArchetypeComponent:
                {
                    var syncData = entityManager.GetComponentData<ArchetypeComponent_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalPersistence:
                {
                    var syncData = entityManager.GetComponentData<Persistence_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericPrefabReference:
                {
                    var syncData = entityManager.GetComponentData<GenericPrefabReference_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericScale:
                {
                    var syncData = entityManager.GetComponentData<GenericScale_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt0:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt0_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt1:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt1_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt2:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt2_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt3:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt3_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt4:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt4_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt5:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt5_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt6:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt6_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt7:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt7_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt8:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt8_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldInt9:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldInt9_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool0:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool0_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool1:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool1_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool2:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool2_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool3:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool3_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool4:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool4_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool5:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool5_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool6:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool6_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool7:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool7_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool8:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool8_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldBool9:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldBool9_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat0:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat0_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat1:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat1_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat2:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat2_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat3:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat3_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat4:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat4_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat5:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat5_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat6:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat6_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat7:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat7_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat8:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat8_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldFloat9:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldFloat9_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldVector0:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldVector0_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldVector1:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldVector1_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldVector2:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldVector2_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldVector3:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldVector3_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldString0:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldString0_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldString1:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldString1_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldString2:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldString2_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldString4:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldString4_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }

                case TypeIds.InternalGenericFieldQuaternion0:
                {
                    var syncData = entityManager.GetComponentData<GenericFieldQuaternion0_Sync>(unityEntity);
                    syncData.deleteHasBeenSerialized = true;
                    entityManager.SetComponentData(unityEntity, syncData);
                    break;
                }


                default:
                    throw new Exception($"unknown componentType {componentTypeId}");
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
    	
    	public void UpdateDestructState(EntityManager entityManager, Entity unityEntity, uint componentTypeId)
    	{
            serializeComponentUpdatesGenerated.UpdateDestructState(entityManager, unityEntity, componentTypeId);
        }
    }

}


// ------------------ end of SerializeComponentUpdate.cs -----------------
#endregion
