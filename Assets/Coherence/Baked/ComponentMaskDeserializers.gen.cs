


#region ComponentMaskDeserializers
// -----------------------------------
//  ComponentMaskDeserializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using Unity.Transforms;
	using Coherence.Replication.Unity;
	using Coherence.Replication.Protocol.Definition;
	using global::Coherence.Generated;


public class UnityReaders
{
    private CoherenceToUnityConverters coherenceToUnityConverters;

    public UnityReaders(UnityMapper mapper)
    {
        coherenceToUnityConverters = new CoherenceToUnityConverters(mapper);
    }
	
	public uint Read(ref Translation data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.Value = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref Rotation data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadUnitRotation();
			     data.Value = coherenceToUnityConverters.ToUnityquaternion(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref LocalUser data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.localIndex = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref WorldPositionQuery data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.position = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}

		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.radius = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000010;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref ArchetypeComponent data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.index = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref Persistence data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.uuid = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}

		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.expiry = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000010;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref ConnectedEntity data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericPrefabReference data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.prefab = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericScale data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.Value = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt1 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt2 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt3 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt4 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt5 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt6 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt7 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt8 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldInt9 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadIntegerRange(15, -9999);
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool1 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool2 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool3 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool4 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool5 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool6 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool7 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool8 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldBool9 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadBool();
			       data.number = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat1 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat2 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat3 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat4 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat5 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat6 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat7 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat8 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldFloat9 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadFixedPoint(24, 2400);
			     data.number = coherenceToUnityConverters.ToUnityfloat(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldVector0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.Value = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldVector1 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.Value = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldVector2 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.Value = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldVector3 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadVector3f(24, 2400);
			     data.Value = coherenceToUnityConverters.ToUnityfloat3(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldString0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.name = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldString1 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.name = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldString2 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.name = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldString3 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.name = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldString4 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadShortString();
			     data.name = coherenceToUnityConverters.ToUnityFixedString64(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldQuaternion0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadUnitRotation();
			     data.Value = coherenceToUnityConverters.ToUnityquaternion(coherenceField);
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity0 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity1 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity2 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity3 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity4 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity5 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity6 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity7 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity8 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref GenericFieldEntity9 data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.Value = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref Cube_Cube data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.friend = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}
       
		return propertyMask;
	}

	
	public uint Read(ref Player_Controller data, IInBitStream bitstream)
	{
		var propertyMask = (uint)0;


		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.otherPlayer = coherenceField;
			propertyMask |= 0b00000000000000000000000000000001;
		}

		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.otherPlayerTransform = coherenceField;
			propertyMask |= 0b00000000000000000000000000000010;
		}

		if (bitstream.ReadMask()) 
		{
			var coherenceField = bitstream.ReadEntity();
			       data.otherPlayerSync = coherenceField;
			propertyMask |= 0b00000000000000000000000000000100;
		}
       
		return propertyMask;
	}

	
}

}

// ------------------ end of ComponentMaskDeserializers.cs -----------------
#endregion
