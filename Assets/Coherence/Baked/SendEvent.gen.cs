


#region SendEvent
// -----------------------------------
//  SendEvent.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
    using UnityEngine;
    using Unity.Entities;
    using global::Coherence.Generated;

    // TODO: Remove some of these imports
    using Message;
    using Message.Serializer.Serialize;
    using MessageSync.Serialize;
    using Coherence.Brisk.Connect;
    using Coherence.Brook;
    using Coherence.Log;
    using Replication.Client.Unity.Ecs;
    using Replication.Unity;

    [UpdateInGroup(typeof(CoherenceSimulationSystemGroup))]
    [AlwaysUpdateSystem]
    public class SendEventSystem : SystemBase
    {
        private bool isBooted;

        private MessageSerializers messageSerializers;
        private UnityMapper mapper;
        private OutgoingCombinedChannel messageChannels;

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


            // TransferAction
            Entities
                .WithAll<Simulated>()
                .ForEach((Entity entity, in TransferAction eventComponent) =>
                    {
                        var foundEntity = mapper.ToCoherenceEntityId(entity, out var coherenceEntityId);
                        if (!foundEntity)
                        {
                            Debug.LogError($"sending event, but can't find entity {entity} in coherence mapper");
                            return;
                        }

                        var octetStream = new OctetWriter(512); // THIS MAGIC NUMBER IS TAKEN FROM COMMANDS CODE, HMM?!!
                        var bitStream = new OutBitStream(octetStream);

                        EntityIdSerializer.Serialize(coherenceEntityId, bitStream);

                        var protocol = new Coherence.FieldStream.Serialize.Streams.OutBitStream(bitStream);

                        // --------- Type Specific Part ---------------
                        ComponentTypeIdSerializer.Serialize(TypeIds.InternalTransferAction, bitStream);
                        messageSerializers.TransferAction(protocol, eventComponent);
                        // --------------------------------------------

                        bitStream.Flush();
                        var payload = new BitSerializedMessage(octetStream.Octets, bitStream.Tell);
                        messageChannels.PushEntityEvent(payload);
					}).WithoutBurst().Run();


        }
    }

}


// ------------------ end of SendEvent.cs -----------------
#endregion
