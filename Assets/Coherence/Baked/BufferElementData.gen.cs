


#region BufferElementData
// -----------------------------------
//  BufferElementData.cs
// -----------------------------------
			
namespace Coherence.Generated
{
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
		public FixedString64 paramString;
	}
}

// ------------------ end of BufferElementData.cs -----------------
#endregion
