


#region ComponentMaskSerializers
// -----------------------------------
//  ComponentMaskSerializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
{
	using global::Coherence.Generated.FirstProject;
	using Replication.Unity;
	using Unity.Transforms;


	public class UnityWriters
	{
        private CoherenceToUnityConverters coherenceToUnityConverters;

        public UnityWriters(UnityMapper mapper)
        {
            coherenceToUnityConverters = new CoherenceToUnityConverters(mapper);
        }

		
		
		public void Write(in Translation data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.Value);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in Rotation data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityquaternion(data.Value);
					bitstream.WriteUnitRotation(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in LocalUser data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.localIndex, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in WorldPositionQuery data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.position);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.radius);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in SessionBased data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
	     }

		
	}
}

// ------------------ end of ComponentMaskSerializers.cs -----------------
#endregion
