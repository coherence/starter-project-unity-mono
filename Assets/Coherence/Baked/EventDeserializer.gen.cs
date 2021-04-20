


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
					var eventData = new TransferAction();
					messageDeserializers.TransferAction(bitStream, ref eventData);

					// Only add events on Entities we don't own
					if(!mgr.HasComponent<Simulated>(entity))
					{
						if (!mgr.HasComponent<TransferAction>(entity))
						{
							mgr.AddBuffer<TransferAction>(entity);
						}
						var buffer = mgr.GetBuffer<TransferAction>(entity);
						buffer.Add(eventData);
					}
					break;
				}
			}
		}
	}
}


// ------------------ end of EventDeserializer.cs -----------------
#endregion
