


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
	
	
	
	// EcsComponentData: InternalNPCBehaviourData
	public struct NPCBehaviour : IComponentData
	{
		public float movementSpeed;
	}
	
	
	
	// EcsComponentData: InternalRotationBehaviourData
	public struct RotationBehaviour : IComponentData
	{
		public float rotationSpeed;
	}
	
	
	
	// EcsComponentData: InternalShowNameAndStateData
	public struct ShowNameAndState : IComponentData
	{
		public FixedString64 PlayerName;
		public int TestInt;
	}
	
	
	
	// EcsComponentData: InternalPlayerBehaviourData
	public struct PlayerBehaviour : IComponentData
	{
		public float movementSpeed;
	}
	
	
	
	// EcsComponentData: InternalBulletData
	public struct Bullet : IComponentData
	{
		public float speed;
		public float lifetime;
	}
	
	
	
	// EcsComponentData: InternalColorizeBehaviourData
	public struct ColorizeBehaviour : IComponentData
	{
	}
	
	
	
	// EcsComponentData: InternalControllerData
	public struct Controller : IComponentData
	{
		public FixedString64 xAxis;
		public FixedString64 yAxis;
		public bool useTankControls;
		public float moveSpeed;
		public float rotationSpeed;
		public bool canJump;
		public float airborneSpeedModifier;
		public float jumpHeight;
		public bool useGun;
	}
	
	

}


// ------------------ end of ComponentData.cs -----------------
#endregion
