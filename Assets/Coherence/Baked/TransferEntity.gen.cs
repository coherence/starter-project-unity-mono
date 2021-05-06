


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
	[UpdateInGroup(typeof(CleanupChangesGroup))]
	public class TransferEntitySystem : SystemBase
	{
		protected override void OnUpdate()
		{
			Entities.WithAll<Simulated>().ForEach((Entity entity, DynamicBuffer<TransferAction> buffer) =>
			{
				if (buffer.Length == 0)
				{
					return;
				}

				var transferAction = buffer[0];

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

				buffer.Clear();
			}).WithStructuralChanges().Run();

			Dependency.Complete();
		}
	}
}
// ------------------ end of TransferEntity.cs -----------------
#endregion
