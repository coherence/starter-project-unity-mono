


#region EventDeserializer
// -----------------------------------
//  EventDeserializer.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
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

			switch (eventTypeID)
			{

				case TypeIds.InternalTransferAction:
				{
                    // Field 'TransferAction'
					var eventData = new TransferAction();
					messageDeserializers.TransferAction(bitStream, ref eventData);                    

                    if(!mgr.HasComponent<Simulated>(entity))
                    {
                        // Only add events on Entities we don't own
                        // UnityEngine.Debug.Log($"Adding an event on {entity}: {eventData}");
                        mgr.AddComponentData(entity, eventData);
                    }
					break;
				}

			}

		}
	}
}


// ------------------ end of EventDeserializer.cs -----------------
#endregion
