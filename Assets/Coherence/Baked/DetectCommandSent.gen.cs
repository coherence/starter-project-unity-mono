


#region DetectCommandSent
// -----------------------------------
//  DetectCommandSent.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
    using UnityEngine;
    using Unity.Entities;
    using global::Coherence.Generated;

    using Message;
    using Message.Serializer.Serialize;
    using Coherence.Brisk.Connect;
    using Coherence.Brook;
    using Coherence.Log;
    using Replication.Client.Unity.Ecs;
    using Replication.Unity;

   // ReSharper disable once ClassNeverInstantiated.Global
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [AlwaysUpdateSystem]
    public class DetectCommandsSentSystem : SystemBase
    {
        private bool isBooted;
	    private Sender cachedSender;
		private MessageSerializers messageSerializers;

        void BootUp()
        {
            var netSys = World.GetOrCreateSystem<NetworkSystem>();
			messageSerializers = new MessageSerializers(netSys.Mapper);
        }

	    protected override void OnUpdate()
	    {
	        if (!isBooted)
	        {
		        BootUp();
		        isBooted = true;
	        }

		    if (cachedSender == null)
		    {
			    cachedSender = World.GetExistingSystem<SyncSendSystem>().Sender;
			    if (cachedSender == null)
			    {
				    return;
			    }
            }

	        var burstSender = cachedSender;
	        var mapper = cachedSender.Mapper;



#region AuthorityTransferRequest
			// ------------  AuthorityTransferRequest --------------
            Entities
                .ForEach((Entity entity, DynamicBuffer<AuthorityTransferRequest> buffer) =>
                    {
                        if (buffer.Length == 0)
                        {
                            return;
                        }
                        
                        var foundEntity = mapper.ToCoherenceEntityId(entity, out var coherenceEntityId);
                        if (!foundEntity)
                        {
	                        Debug.LogError($"send command request. Can not find entity {entity}");
	                        return;
                        }
                        
                        var rawArray = buffer.Reinterpret<AuthorityTransferRequest>();

                        for (var i=0; i<rawArray.Length; i++)
                        {
	                        var item = rawArray[i];
	                        var octetStream = new OctetWriter(512);
	                        var bitStream = new OutBitStream(octetStream);
	                        EntityIdSerializer.Serialize(coherenceEntityId, bitStream);
	                        var protocol = new Coherence.FieldStream.Serialize.Streams.OutBitStream(bitStream);

	                        // --------- Type Specific Part ---------------
	                        ComponentTypeIdSerializer.Serialize(TypeIds.InternalAuthorityTransfer, bitStream);
	                        messageSerializers.AuthorityTransferRequest(protocol, item);
	                        // --------------------------------------------

	                        bitStream.Flush();
	                        var payload = new BitSerializedMessage(octetStream.Octets, bitStream.Tell);
	                        burstSender.MessageChannels.PushEntityCommand(payload);
                        }

                        buffer.Clear();
                    }).WithoutBurst().Run();
#endregion


#region GenericCommandRequest
			// ------------  GenericCommandRequest --------------
            Entities
                .ForEach((Entity entity, DynamicBuffer<GenericCommandRequest> buffer) =>
                    {
                        if (buffer.Length == 0)
                        {
                            return;
                        }
                        
                        var foundEntity = mapper.ToCoherenceEntityId(entity, out var coherenceEntityId);
                        if (!foundEntity)
                        {
	                        Debug.LogError($"send command request. Can not find entity {entity}");
	                        return;
                        }
                        
                        var rawArray = buffer.Reinterpret<GenericCommandRequest>();

                        for (var i=0; i<rawArray.Length; i++)
                        {
	                        var item = rawArray[i];
	                        var octetStream = new OctetWriter(512);
	                        var bitStream = new OutBitStream(octetStream);
	                        EntityIdSerializer.Serialize(coherenceEntityId, bitStream);
	                        var protocol = new Coherence.FieldStream.Serialize.Streams.OutBitStream(bitStream);

	                        // --------- Type Specific Part ---------------
	                        ComponentTypeIdSerializer.Serialize(TypeIds.InternalGenericCommand, bitStream);
	                        messageSerializers.GenericCommandRequest(protocol, item);
	                        // --------------------------------------------

	                        bitStream.Flush();
	                        var payload = new BitSerializedMessage(octetStream.Octets, bitStream.Tell);
	                        burstSender.MessageChannels.PushEntityCommand(payload);
                        }

                        buffer.Clear();
                    }).WithoutBurst().Run();
#endregion


        }
    }

}


// ------------------ end of DetectCommandSent.cs -----------------
#endregion
