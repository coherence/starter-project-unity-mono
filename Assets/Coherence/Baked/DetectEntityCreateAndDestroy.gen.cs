


#region DetectEntityCreateAndDestroy
// -----------------------------------
//  DetectEntityCreateAndDestroy.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
    using Replication.Client.Unity.Ecs;
	using Unity.Collections;
    using Unity.Entities;
    using Replication.Unity;
    using Unity.Transforms;
    using UnityEngine;    
    
    [UpdateInGroup(typeof(GatherChangesGroup))]
    public class DetectEntityCreateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (World.GetExistingSystem<SyncSendSystem>().Sender == null)
            {
                return;
            }

            var mapper = World.GetExistingSystem<SyncSendSystem>().Sender.Mapper;
            
            Entities.WithNone<Mapped>().ForEach((Entity entity, int entityInQueryIndex, in Simulated simulate) =>
            {
                var id = mapper.NextEntityId;
                mapper.Add(id, entity);
                EntityManager.AddComponent<Mapped>(entity);
            }).WithStructuralChanges().WithoutBurst().Run();

            Dependency.Complete();
       }
    }
} // namespace
        


// ------------------ end of DetectEntityCreateAndDestroy.cs -----------------
#endregion
