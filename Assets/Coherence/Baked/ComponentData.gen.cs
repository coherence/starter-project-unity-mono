


#region ComponentData
// -----------------------------------
//  ComponentData.cs
// -----------------------------------
			
namespace Coherence.Generated.FirstProject
{
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;

	
	
 
	
	
 
	
	
	// EcsComponentData: InternalLocalUserData
	public struct LocalUser : IComponentData
	{
		public int localIndex;
	}
	
	
	
	// EcsComponentData: InternalWorldPositionQueryData
	public struct WorldPositionQuery : IComponentData
	{
		public float3 position;
		public float radius;
	}
	
	
	
	// EcsComponentData: InternalSessionBasedData
	public struct SessionBased : IComponentData
	{
	}
	
	

}


// ------------------ end of ComponentData.cs -----------------
#endregion
