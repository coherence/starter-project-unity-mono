


#region DeleteEvent
// -----------------------------------
//  DeleteEvent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Unity.Entities;

	[AlwaysUpdateSystem]
	[UpdateInGroup(typeof(CleanupChangesGroup))]
	public class DeleteEventSystem : SystemBase
	{
		protected override void OnUpdate()
		{
			Entities.ForEach((Entity entity, DynamicBuffer<TransferAction> buffer) =>
			{
				buffer.Clear();
			}).ScheduleParallel();
		}
	}
}
// ------------------ end of DeleteEvent.cs -----------------
#endregion
