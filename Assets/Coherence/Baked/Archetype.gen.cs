


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
	using System.Collections.Generic;

	public struct LastObservedLod : IComponentData
	{
		public int Level;
	}

	

	public static class Archetype
	{
	

		public static Dictionary<string, int> IndexForName = new Dictionary<string, int>() {
		
		};

	
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
