


#region BufferElementData
// -----------------------------------
//  BufferElementData.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using Coherence.Ecs;
	using UnityEngine.Scripting;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	
	// EcsComponentData: InternalAuthorityTransferData
	public struct AuthorityTransfer : IBufferElementData
	{
		public int participant;
	}

	public struct AuthorityTransferRequest : IBufferElementData
	{
		public int participant;
	}
	
	// EcsComponentData: InternalGenericCommandData
	public struct GenericCommand : IBufferElementData
	{
		public FixedString64 name;
		public int paramInt1;
		public int paramInt2;
		public int paramInt3;
		public int paramInt4;
		public float paramFloat1;
		public float paramFloat2;
		public float paramFloat3;
		public float paramFloat4;
		public bool paramBool1;
		public bool paramBool2;
		public bool paramBool3;
		public bool paramBool4;
		public SerializeEntityID paramEntity1;
		public SerializeEntityID paramEntity2;
		public SerializeEntityID paramEntity3;
		public SerializeEntityID paramEntity4;
		public FixedString64 paramString;
	}

	public struct GenericCommandRequest : IBufferElementData
	{
		public FixedString64 name;
		public int paramInt1;
		public int paramInt2;
		public int paramInt3;
		public int paramInt4;
		public float paramFloat1;
		public float paramFloat2;
		public float paramFloat3;
		public float paramFloat4;
		public bool paramBool1;
		public bool paramBool2;
		public bool paramBool3;
		public bool paramBool4;
		public SerializeEntityID paramEntity1;
		public SerializeEntityID paramEntity2;
		public SerializeEntityID paramEntity3;
		public SerializeEntityID paramEntity4;
		public FixedString64 paramString;
	}
	
	// EcsComponentData: InternalPlayer_Controller_FooData
	public struct Player_Controller_Foo : IBufferElementData
	{
		public SerializeEntityID g;
	}

	public struct Player_Controller_FooRequest : IBufferElementData
	{
		public SerializeEntityID g;
	}
	
	// EcsComponentData: InternalPlayer_Controller_BooData
	public struct Player_Controller_Boo : IBufferElementData
	{
	}

	public struct Player_Controller_BooRequest : IBufferElementData
	{
	}
}

// ------------------ end of BufferElementData.cs -----------------
#endregion
