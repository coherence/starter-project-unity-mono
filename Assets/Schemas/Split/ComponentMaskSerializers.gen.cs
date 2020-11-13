


#region ComponentMaskSerializers
// -----------------------------------
//  ComponentMaskSerializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Schema
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

		
		
		public void Write(in GenericPrefabReference data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.prefab);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericScale data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.Value);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt0 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt1 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt2 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt3 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt4 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt5 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt6 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt7 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt8 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldInt9 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.number, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat0 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat1 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat2 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat3 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat4 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat5 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat6 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat7 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat8 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldFloat9 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.number);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldVector0 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.Value);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldVector1 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.Value);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldVector2 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.Value);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldVector3 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.Value);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldString0 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.name);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldString1 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.name);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldString2 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.name);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in GenericFieldString4 data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.name);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in NPCBehaviour data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.movementSpeed);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in RotationBehaviour data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.rotationSpeed);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in ShowNameAndState data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.PlayerName);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.TestInt, 15, -9999);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in PlayerBehaviour data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.movementSpeed);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in Bullet data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.speed);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.lifetime);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in ColorizeBehaviour data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteIntegerRange(data.iii, 15, -9999);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.fff);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteBool(data.bbb);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat3(data.target);
					bitstream.WriteVector3f(v, 24, 2400);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityquaternion(data.whatever_works);
					bitstream.WriteUnitRotation(v);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.name2);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
	     }

		
		
		public void Write(in Controller data, uint propertyMask, Coherence.Replication.Protocol.Definition.IOutBitStream bitstream)
		{
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.xAxis);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityFixedString64(data.yAxis);
					bitstream.WriteShortString(v);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteBool(data.useTankControls);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.moveSpeed);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.rotationSpeed);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteBool(data.canJump);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.airborneSpeedModifier);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					var v = coherenceToUnityConverters.FromUnityfloat(data.jumpHeight);
					bitstream.WriteFixedPoint(v, 24, 40000);
				
			}
			propertyMask >>= 1;
	
			if (bitstream.WriteMask((propertyMask & 0x01) != 0))
			{
				
					bitstream.WriteBool(data.useGun);
				
			}
			propertyMask >>= 1;
	
	     }

		
	}
}

// ------------------ end of ComponentMaskSerializers.cs -----------------
#endregion
