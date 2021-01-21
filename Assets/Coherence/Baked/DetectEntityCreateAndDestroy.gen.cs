


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
    
    // ReSharper disable once ClassNeverInstantiated.Global
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateBefore(typeof(SyncSendSystem))]
    public class DetectEntityCreateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (World.GetExistingSystem<SyncSendSystem>().Sender == null)
            {
                Debug.LogWarning("No sender");
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
