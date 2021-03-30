


#region ComponentData
// -----------------------------------
//  ComponentData.cs
// -----------------------------------
			
namespace Coherence.Generated
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
	
	// EcsComponentData: InternalArchetypeComponentData
	public struct ArchetypeComponent : IComponentData
	{
		public int index;
	}
	
	// EcsComponentData: InternalPersistenceData
	public struct Persistence : IComponentData
	{
		public FixedString64 uuid;
		public FixedString64 expiry;
	}
	
	// EcsComponentData: InternalGenericPrefabReferenceData
	public struct GenericPrefabReference : IComponentData
	{
		public FixedString64 prefab;
	}
	
	// EcsComponentData: InternalGenericScaleData
	public struct GenericScale : IComponentData
	{
		public float3 Value;
	}
	
	// EcsComponentData: InternalGenericFieldInt0Data
	public struct GenericFieldInt0 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt1Data
	public struct GenericFieldInt1 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt2Data
	public struct GenericFieldInt2 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt3Data
	public struct GenericFieldInt3 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt4Data
	public struct GenericFieldInt4 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt5Data
	public struct GenericFieldInt5 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt6Data
	public struct GenericFieldInt6 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt7Data
	public struct GenericFieldInt7 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt8Data
	public struct GenericFieldInt8 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldInt9Data
	public struct GenericFieldInt9 : IComponentData
	{
		public int number;
	}
	
	// EcsComponentData: InternalGenericFieldBool0Data
	public struct GenericFieldBool0 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool1Data
	public struct GenericFieldBool1 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool2Data
	public struct GenericFieldBool2 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool3Data
	public struct GenericFieldBool3 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool4Data
	public struct GenericFieldBool4 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool5Data
	public struct GenericFieldBool5 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool6Data
	public struct GenericFieldBool6 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool7Data
	public struct GenericFieldBool7 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool8Data
	public struct GenericFieldBool8 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldBool9Data
	public struct GenericFieldBool9 : IComponentData
	{
		public bool number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat0Data
	public struct GenericFieldFloat0 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat1Data
	public struct GenericFieldFloat1 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat2Data
	public struct GenericFieldFloat2 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat3Data
	public struct GenericFieldFloat3 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat4Data
	public struct GenericFieldFloat4 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat5Data
	public struct GenericFieldFloat5 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat6Data
	public struct GenericFieldFloat6 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat7Data
	public struct GenericFieldFloat7 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat8Data
	public struct GenericFieldFloat8 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldFloat9Data
	public struct GenericFieldFloat9 : IComponentData
	{
		public float number;
	}
	
	// EcsComponentData: InternalGenericFieldVector0Data
	public struct GenericFieldVector0 : IComponentData
	{
		public float3 Value;
	}
	
	// EcsComponentData: InternalGenericFieldVector1Data
	public struct GenericFieldVector1 : IComponentData
	{
		public float3 Value;
	}
	
	// EcsComponentData: InternalGenericFieldVector2Data
	public struct GenericFieldVector2 : IComponentData
	{
		public float3 Value;
	}
	
	// EcsComponentData: InternalGenericFieldVector3Data
	public struct GenericFieldVector3 : IComponentData
	{
		public float3 Value;
	}
	
	// EcsComponentData: InternalGenericFieldString0Data
	public struct GenericFieldString0 : IComponentData
	{
		public FixedString64 name;
	}
	
	// EcsComponentData: InternalGenericFieldString1Data
	public struct GenericFieldString1 : IComponentData
	{
		public FixedString64 name;
	}
	
	// EcsComponentData: InternalGenericFieldString2Data
	public struct GenericFieldString2 : IComponentData
	{
		public FixedString64 name;
	}
	
	// EcsComponentData: InternalGenericFieldString4Data
	public struct GenericFieldString4 : IComponentData
	{
		public FixedString64 name;
	}
	
	// EcsComponentData: InternalGenericFieldQuaternion0Data
	public struct GenericFieldQuaternion0 : IComponentData
	{
		public quaternion Value;
	}
	
	// EcsComponentData: InternalGenericFieldEntity0Data
	public struct GenericFieldEntity0 : IComponentData
	{
		public Entity Value;
	}
	
	// EcsComponentData: InternalGenericFieldEntity1Data
	public struct GenericFieldEntity1 : IComponentData
	{
		public Entity Value;
	}
	
	// EcsComponentData: InternalGenericFieldEntity2Data
	public struct GenericFieldEntity2 : IComponentData
	{
		public Entity Value;
	}
	
	// EcsComponentData: InternalGenericFieldEntity3Data
	public struct GenericFieldEntity3 : IComponentData
	{
		public Entity Value;
	}
	
	// EcsComponentData: InternalCube_CubeData
	public struct Cube_Cube : IComponentData
	{
		public Entity friend;
	}
}
// ------------------ end of ComponentData.cs -----------------
#endregion
