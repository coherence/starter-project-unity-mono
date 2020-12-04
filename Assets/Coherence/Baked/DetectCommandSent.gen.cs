


#region DetectCommandSent
// -----------------------------------
//  DetectCommandSent.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
{
    using UnityEngine;
    using Unity.Entities;
    using global::Coherence.Generated.FirstProject;

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



        }
    }

}


// ------------------ end of DetectCommandSent.cs -----------------
#endregion
