


#region SendEvent
// -----------------------------------
//  SendEvent.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using UnityEngine;
	using Unity.Entities;
	using global::Coherence.Generated;

	using Coherence.Brook;
	using Message;
	using Message.Serializer.Serialize;
	using MessageSync.Serialize;
	using Replication.Client.Unity.Ecs;
	using Replication.Unity;

	[UpdateInGroup(typeof(GatherChangesGroup))]
	[AlwaysUpdateSystem]
	public class SendEventSystem : SystemBase
	{
		private bool isBooted;

		private MessageSerializers messageSerializers;
		private UnityMapper mapper;
		private OutgoingCombinedChannel messageChannels;

		private const int MAX_EVENT_OCTETS = 512;

		void BootUp()
		{
			var netSys = World.GetOrCreateSystem<NetworkSystem>();
			messageSerializers = new MessageSerializers(netSys.Mapper);
			mapper = netSys.Mapper;
			messageChannels = netSys.MessageChannels;
		}

		protected override void OnUpdate()
		{
			if (!isBooted)
			{
				BootUp();
				isBooted = true;
			}

			var octetStream = new OctetWriter(MAX_EVENT_OCTETS);
			var bitStream = new OutBitStream(octetStream);

			// TransferAction
			Entities.WithAll<Simulated>().ForEach((Entity entity, DynamicBuffer<TransferAction> buffer) =>
			{
				if (buffer.Length == 0)
				{
					return;
				}

				var foundEntity = mapper.ToCoherenceEntityId(entity, out var coherenceEntityId);
				if (!foundEntity)
				{
					Debug.LogError($"sending event, but can't find entity {entity} in coherence mapper");
					return;
				}

				for (var i = 0; i < buffer.Length; i++)
				{
					bitStream.Rewind(0);
					EntityIdSerializer.Serialize(coherenceEntityId, bitStream);

					var protocol = new Coherence.FieldStream.Serialize.Streams.OutBitStream(bitStream);

					var eventComponent = buffer[i];

					// --------- Type Specific Part ---------------
					ComponentTypeIdSerializer.Serialize(TypeIds.InternalTransferAction, bitStream);
					messageSerializers.TransferAction(protocol, eventComponent);
					// --------------------------------------------

					bitStream.Flush();
					var payload = new BitSerializedMessage(octetStream.Octets, bitStream.Tell);
					messageChannels.PushEntityEvent(payload);
				}

				buffer.Clear();
			}).WithoutBurst().Run();
		}
	}
}

// ------------------ end of SendEvent.cs -----------------
#endregion
