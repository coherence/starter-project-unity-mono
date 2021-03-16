


#region DeleteEvent
// -----------------------------------
//  DeleteEvent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;

	[AlwaysUpdateSystem]
	[UpdateInGroup(typeof(CoherenceSimulationSystemGroup))]
	[UpdateAfter(typeof(TransferEntitySystem))]
	public class DeleteEventSystem : SystemBase
	{
		protected override void OnUpdate()
		{

			Entities
			   .WithAll<global::Coherence.Generated.TransferAction>()
			   .ForEach((Entity entity) =>
			{

				EntityManager.RemoveComponent<TransferAction>(entity);
			}).WithStructuralChanges().Run();

			Dependency.Complete();
		}
	}
}
// ------------------ end of DeleteEvent.cs -----------------
#endregion
