


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
	[UpdateInGroup(typeof(PresentationSystemGroup))]
	public class DetectDeletedEntitiesSystem : SystemBase
	{
		private NativeHashMap<Entity, DetectedEntityDeletion> destroyedEntities;
		private bool booted;
		
		private void Boot()
		{
			destroyedEntities = World.GetExistingSystem<NetworkSystem>().DestroyedEntities;
			booted = false;
		}
		
		protected override void OnUpdate()
		{
			if (!booted)
			{
				Boot();
			}
			
			var simulationFrame = World.GetExistingSystem<CoherenceSimulationSystemGroup>().SimulationFrame;
			
			// Ensure all simulated entities have their system state component in order to track entity destruction
			Entities.WithNone<LingerSimulated>().ForEach((Entity entity, int entityInQueryIndex, in Simulated simulate) =>
			{
				EntityManager.AddComponentData(entity, new LingerSimulated());
			}).WithStructuralChanges().WithoutBurst().Run();
			
			// Keep track of locally destroyed entities so that SyncReceiveSystem does not revive them
			var commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
			Entities.WithNone<Simulated>().ForEach((Entity entity, int entityInQueryIndex, in LingerSimulated sync) =>
			{
				destroyedEntities.TryAdd(entity, new DetectedEntityDeletion { Entity = entity, simulationFrame = simulationFrame, serialized = false });
				commandBuffer.RemoveComponent<LingerSimulated>(entity);
			}).WithoutBurst().Run();
			
			// Clear entities that were locally destroyed over 10s ago, to prevent hashmap from overflowing  
			var kv = destroyedEntities.GetKeyValueArrays(Allocator.TempJob);
			for (var i = 0; i < kv.Values.Length; i++)
			{
				if (simulationFrame > kv.Values[i].simulationFrame + 60 * 10)
				{
					destroyedEntities.Remove(kv.Keys[i]);
				}
			}
			kv.Dispose();
		}
	}
}

// ------------------ end of DetectDeletedEntity.cs -----------------
#endregion
