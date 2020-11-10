


#region DeleteEvent
// -----------------------------------
//  DeleteEvent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.Schema
{
	using global::Coherence.Generated.FirstProject;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;

	[AlwaysUpdateSystem]
	[UpdateInGroup(typeof(CoherenceSimulationSystemGroup))]
	[UpdateAfter(typeof(SyncSendSystem))]
	public class DeleteEventSystem : SystemBase
	{
		protected override void OnUpdate()
		{

			Dependency.Complete();
		}
	}
}
// ------------------ end of DeleteEvent.cs -----------------
#endregion
