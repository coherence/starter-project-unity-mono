


#region DetectDeletedEntity
// -----------------------------------
//  DetectDeletedEntity.cs
// -----------------------------------
			
namespace Coherence.Replication.Client.Unity.Ecs
{
	using Coherence.Replication.Unity;
	using global::Unity.Collections;
	using global::Unity.Entities;
	
	[AlwaysUpdateSystem]
	[UpdateInGroup(typeof(GatherChangesGroup))]
	public class DetectDeletedEntitiesSystem : SystemBase
	{
		private NetworkSystem networkSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			networkSystem = World.GetExistingSystem<NetworkSystem>();
		}

		protected override void OnUpdate()
		{
			// Ensure all simulated entities have their system state component in order to track entity destruction
			Entities.WithNone<LingerSimulated>().ForEach((Entity entity, int entityInQueryIndex, in Simulated simulate) =>
			{
				EntityManager.AddComponentData(entity, new LingerSimulated());
			}).WithStructuralChanges().WithoutBurst().Run();
			
			// Keep track of locally destroyed entities so that SyncReceiveSystem does not revive them
			var commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
			Entities.WithNone<Simulated>().ForEach((Entity entity, int entityInQueryIndex, in LingerSimulated sync) =>
			{
				networkSystem.changeBuffer.DestroyEntity(entity);
				commandBuffer.RemoveComponent<LingerSimulated>(entity);
			}).WithoutBurst().Run();
		}
	}
}

// ------------------ end of DetectDeletedEntity.cs -----------------
#endregion
