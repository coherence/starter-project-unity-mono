


#region EventData
// -----------------------------------
//  EventData.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using Coherence.Ecs;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;

	// Event: InternalTransferAction
	public struct TransferAction : IBufferElementData
	{
		public int participant;
		public bool accepted;
	}
}


// ------------------ end of EventData.cs -----------------
#endregion
