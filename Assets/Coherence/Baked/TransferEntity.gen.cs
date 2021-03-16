


#region TransferEntity
// -----------------------------------
//  TransferEntity.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;

	[AlwaysUpdateSystem]
	[UpdateInGroup(typeof(CoherenceSimulationSystemGroup))]
	[UpdateAfter(typeof(SendEventSystem))]
	public class TransferEntitySystem : SystemBase
	{
		protected override void OnUpdate() 
		{ 
 
			Entities.WithAll<Simulated>().ForEach((Entity entity, in TransferAction transferAction) => 
				{ 
					if (transferAction.accepted) 
					{ 
						EntityManager.RemoveComponent<Simulated>(entity); 
						EntityManager.RemoveComponent<LingerSimulated>(entity); 
						ReceiveUpdate.RemoveSyncComponents(EntityManager, entity); 
						ReceiveUpdate.AddCommandBuffers(EntityManager, entity); 
					} 
					if (transferAction.accepted && transferAction.participant == 0) 
					{ 
						EntityManager.AddComponent<Orphan>(entity); 
					} 
				}).WithStructuralChanges().Run(); 
 
			Dependency.Complete(); 
		} 
	} 
}
// ------------------ end of TransferEntity.cs -----------------
#endregion
