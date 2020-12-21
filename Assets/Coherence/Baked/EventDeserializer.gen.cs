


#region EventDeserializer
// -----------------------------------
//  EventDeserializer.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Toolkit
{
	using global::Coherence.Generated.FirstProject;
	using Coherence.Log;
	using Unity.Entities;
	using Replication.Client.Unity.Ecs;
	using Coherence.Replication.Unity;

	public class PerformEvents : IPerformEvent
	{
        private MessageDeserializers messageDeserializers;

        public PerformEvents(UnityMapper mapper)
        {
            messageDeserializers = new MessageDeserializers(mapper);
        }

		public void PerformEvent(EntityManager mgr, Entity entity, uint eventTypeID,
                                 Coherence.Replication.Protocol.Definition.IInBitStream bitStream)
		{

		}
	}
}


// ------------------ end of EventDeserializer.cs -----------------
#endregion
