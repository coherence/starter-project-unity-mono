


#region Archetype
// -----------------------------------
//  Archetype.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;

	

	public static class Archetype
	{
	

	
	}

	public class ArchetypeTagSystem : SystemBase
	{
		EndSimulationEntityCommandBufferSystem commandBufferSystem;

		protected override void OnCreate()
		{
			commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			
		}
	}
}


// ------------------ end of Archetype.cs -----------------
#endregion
