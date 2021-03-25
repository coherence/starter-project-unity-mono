


#region DeserializeAndSkipComponentUpdate
// -----------------------------------
//  DeserializeAndSkipComponentUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
    using Coherence.Log;
    using Unity.Transforms;
    using global::Coherence.Generated;
    using Replication.Client.Unity.Ecs;
    using Coherence.Replication.Unity;

    public class DeserializeComponentUpdateSkipGenerated
    {
        private UnityReaders unityReaders;

        public DeserializeComponentUpdateSkipGenerated(UnityMapper mapper)
        {
            unityReaders = new UnityReaders(mapper);
        }

		
		private void DeserializeWorldPosition(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new Translation();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeWorldOrientation(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new Rotation();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeLocalUser(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new LocalUser();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeWorldPositionQuery(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new WorldPositionQuery();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeArchetypeComponent(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new ArchetypeComponent();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializePersistence(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new Persistence();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeInputClient(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new InputClient();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericPrefabReference(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericPrefabReference();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericScale(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericScale();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt0(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt0();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt1(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt1();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt2(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt2();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt3(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt3();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt4(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt4();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt5(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt5();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt6(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt6();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt7(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt7();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt8(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt8();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldInt9(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldInt9();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat0(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat0();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat1(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat1();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat2(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat2();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat3(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat3();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat4(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat4();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat5(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat5();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat6(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat6();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat7(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat7();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat8(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat8();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldFloat9(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldFloat9();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldVector0(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldVector0();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldVector1(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldVector1();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldVector2(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldVector2();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldVector3(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldVector3();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldString0(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldString0();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldString1(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldString1();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldString2(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldString2();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldString4(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldString4();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
		private void DeserializeGenericFieldQuaternion0(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new GenericFieldQuaternion0();
            unityReaders.Read(ref ignored, protocolStream);
		}
		
 
		public void SkipComponentDataUpdate(uint componentType, Coherence.Replication.Protocol.Definition.IInBitStream inProtocolStream)
        {
			switch (componentType)
            {

                case TypeIds.InternalWorldPosition:
					DeserializeWorldPosition(inProtocolStream);
                    break;

                case TypeIds.InternalWorldOrientation:
					DeserializeWorldOrientation(inProtocolStream);
                    break;

                case TypeIds.InternalLocalUser:
					DeserializeLocalUser(inProtocolStream);
                    break;

                case TypeIds.InternalWorldPositionQuery:
					DeserializeWorldPositionQuery(inProtocolStream);
                    break;

                case TypeIds.InternalArchetypeComponent:
					DeserializeArchetypeComponent(inProtocolStream);
                    break;

                case TypeIds.InternalPersistence:
					DeserializePersistence(inProtocolStream);
                    break;

                case TypeIds.InternalInputClient:
					DeserializeInputClient(inProtocolStream);
                    break;

                case TypeIds.InternalGenericPrefabReference:
					DeserializeGenericPrefabReference(inProtocolStream);
                    break;

                case TypeIds.InternalGenericScale:
					DeserializeGenericScale(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt0:
					DeserializeGenericFieldInt0(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt1:
					DeserializeGenericFieldInt1(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt2:
					DeserializeGenericFieldInt2(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt3:
					DeserializeGenericFieldInt3(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt4:
					DeserializeGenericFieldInt4(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt5:
					DeserializeGenericFieldInt5(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt6:
					DeserializeGenericFieldInt6(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt7:
					DeserializeGenericFieldInt7(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt8:
					DeserializeGenericFieldInt8(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldInt9:
					DeserializeGenericFieldInt9(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat0:
					DeserializeGenericFieldFloat0(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat1:
					DeserializeGenericFieldFloat1(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat2:
					DeserializeGenericFieldFloat2(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat3:
					DeserializeGenericFieldFloat3(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat4:
					DeserializeGenericFieldFloat4(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat5:
					DeserializeGenericFieldFloat5(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat6:
					DeserializeGenericFieldFloat6(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat7:
					DeserializeGenericFieldFloat7(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat8:
					DeserializeGenericFieldFloat8(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldFloat9:
					DeserializeGenericFieldFloat9(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldVector0:
					DeserializeGenericFieldVector0(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldVector1:
					DeserializeGenericFieldVector1(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldVector2:
					DeserializeGenericFieldVector2(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldVector3:
					DeserializeGenericFieldVector3(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldString0:
					DeserializeGenericFieldString0(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldString1:
					DeserializeGenericFieldString1(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldString2:
					DeserializeGenericFieldString2(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldString4:
					DeserializeGenericFieldString4(inProtocolStream);
                    break;

                case TypeIds.InternalGenericFieldQuaternion0:
					DeserializeGenericFieldQuaternion0(inProtocolStream);
                    break;

			}
		}
    }
    
    public class DeserializeComponentsAndSkipWrapper : ISchemaSpecificComponentDeserializerAndSkip
    {
        DeserializeComponentUpdateSkipGenerated deserializeComponentUpdateSkipGenerated;

        public DeserializeComponentsAndSkipWrapper(UnityMapper mapper)
        {
            deserializeComponentUpdateSkipGenerated = new DeserializeComponentUpdateSkipGenerated(mapper);
        }

        public void DeserializeAndSkipComponent(uint componentTypeId, Coherence.Replication.Protocol.Definition.IInBitStream protocolOutStream)
        {
            deserializeComponentUpdateSkipGenerated.SkipComponentDataUpdate(componentTypeId, protocolOutStream);
        }
    }    
}

// ------------------ end of DeserializeAndSkipComponentUpdate.cs -----------------
#endregion
