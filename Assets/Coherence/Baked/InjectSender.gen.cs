


#region InjectSender
// -----------------------------------
//  InjectSender.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.Toolkit
{
	using global::Coherence.Generated.FirstProject;
    using Coherence.Replication.Client.Unity.Ecs;
    using Unity.Entities;

    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateBefore(typeof(DetectDeletedEntitiesSystem))]
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
            var sender = new Sender(World, netSys.Connector, netSys.Mapper, wrapper, netSys.SentPacketsCache);
            World.GetExistingSystem<SyncSendSystem>().Sender = sender;
        }
    }
}
// ------------------ end of InjectSender.cs -----------------
#endregion
