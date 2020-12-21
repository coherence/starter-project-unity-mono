


#region SendEvent
// -----------------------------------
//  SendEvent.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Toolkit
{
    using UnityEngine;
    using Unity.Entities;
    using global::Coherence.Generated.FirstProject;

    // TODO: Remove some of these imports
    using Message;
    using Message.Serializer.Serialize;
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



        }
    }

}


// ------------------ end of SendEvent.cs -----------------
#endregion
