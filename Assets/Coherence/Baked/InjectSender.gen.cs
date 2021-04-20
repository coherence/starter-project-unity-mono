


#region InjectSender
// -----------------------------------
//  InjectSender.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
    using Coherence.Replication.Client.Unity.Ecs;
    using Unity.Entities;

    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(GatherChangesGroup))]
    public class InjectSenderSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (World.GetExistingSystem<SyncSendSystem>().Sender != null)
            {
                return;
            }
            
            var netSys = World.GetOrCreateSystem<NetworkSystem>();
            var wrapper = new SerializeComponentUpdatesWrapper(netSys.Mapper);
            var sender = new Sender(World, netSys.Mapper, wrapper, netSys.MessageChannels);
            World.GetExistingSystem<SyncSendSystem>().Sender = sender;
        }
    }
}
// ------------------ end of InjectSender.cs -----------------
#endregion
