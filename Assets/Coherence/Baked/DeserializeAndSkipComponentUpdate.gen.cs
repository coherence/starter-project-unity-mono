


#region DeserializeAndSkipComponentUpdate
// -----------------------------------
//  DeserializeAndSkipComponentUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
{
    using Coherence.Log;
    using Unity.Transforms;
    using global::Coherence.Generated.FirstProject;
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
		
		private void DeserializeSessionBased(Coherence.Replication.Protocol.Definition.IInBitStream protocolStream)
		{
            var ignored = new SessionBased();
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

                case TypeIds.InternalSessionBased:
					DeserializeSessionBased(inProtocolStream);
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
