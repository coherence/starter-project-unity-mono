


#region MessageSerializers
// -----------------------------------
//  MessageSerializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using Coherence.Replication.Protocol.Definition;
	using global::Coherence.Generated;
	using Unity.Transforms;
	using Replication.Unity;


public class MessageSerializers
{
    private CoherenceToUnityConverters coherenceToUnityConverters;

    public MessageSerializers(UnityMapper mapper)
    {
        coherenceToUnityConverters = new CoherenceToUnityConverters(mapper);
    }


	public void WorldPosition(IOutBitStream bitstream, Translation data)
	{

			
				var converted_value = coherenceToUnityConverters.FromUnityfloat3(data.Value);
				bitstream.WriteVector3f(converted_value, 24, 2400);
			

	}

	public void WorldOrientation(IOutBitStream bitstream, Rotation data)
	{

			
				var converted_value = coherenceToUnityConverters.FromUnityquaternion(data.Value);
				bitstream.WriteUnitRotation(converted_value);
			

	}

	public void LocalUser(IOutBitStream bitstream, LocalUser data)
	{

			
				bitstream.WriteIntegerRange(data.localIndex, 15, -9999);
			

	}

	public void WorldPositionQuery(IOutBitStream bitstream, WorldPositionQuery data)
	{

			
				var converted_position = coherenceToUnityConverters.FromUnityfloat3(data.position);
				bitstream.WriteVector3f(converted_position, 24, 2400);
			

			
				var converted_radius = coherenceToUnityConverters.FromUnityfloat(data.radius);
				bitstream.WriteFixedPoint(converted_radius, 24, 40000);
			

	}

	public void SessionBased(IOutBitStream bitstream, SessionBased data)
	{

	}

	public void Transferable(IOutBitStream bitstream, Transferable data)
	{

			
				bitstream.WriteIntegerRange(data.participant, 15, -9999);
			

	}

	public void GenericPrefabReference(IOutBitStream bitstream, GenericPrefabReference data)
	{

			
				var converted_prefab = coherenceToUnityConverters.FromUnityFixedString64(data.prefab);
				bitstream.WriteShortString(converted_prefab);
			

	}

	public void GenericScale(IOutBitStream bitstream, GenericScale data)
	{

			
				var converted_Value = coherenceToUnityConverters.FromUnityfloat3(data.Value);
				bitstream.WriteVector3f(converted_Value, 24, 2400);
			

	}

	public void GenericFieldInt0(IOutBitStream bitstream, GenericFieldInt0 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt1(IOutBitStream bitstream, GenericFieldInt1 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt2(IOutBitStream bitstream, GenericFieldInt2 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt3(IOutBitStream bitstream, GenericFieldInt3 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt4(IOutBitStream bitstream, GenericFieldInt4 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt5(IOutBitStream bitstream, GenericFieldInt5 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt6(IOutBitStream bitstream, GenericFieldInt6 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt7(IOutBitStream bitstream, GenericFieldInt7 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt8(IOutBitStream bitstream, GenericFieldInt8 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldInt9(IOutBitStream bitstream, GenericFieldInt9 data)
	{

			
				bitstream.WriteIntegerRange(data.number, 15, -9999);
			

	}

	public void GenericFieldFloat0(IOutBitStream bitstream, GenericFieldFloat0 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat1(IOutBitStream bitstream, GenericFieldFloat1 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat2(IOutBitStream bitstream, GenericFieldFloat2 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat3(IOutBitStream bitstream, GenericFieldFloat3 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat4(IOutBitStream bitstream, GenericFieldFloat4 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat5(IOutBitStream bitstream, GenericFieldFloat5 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat6(IOutBitStream bitstream, GenericFieldFloat6 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat7(IOutBitStream bitstream, GenericFieldFloat7 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat8(IOutBitStream bitstream, GenericFieldFloat8 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldFloat9(IOutBitStream bitstream, GenericFieldFloat9 data)
	{

			
				var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
				bitstream.WriteFixedPoint(converted_number, 24, 40000);
			

	}

	public void GenericFieldVector0(IOutBitStream bitstream, GenericFieldVector0 data)
	{

			
				var converted_Value = coherenceToUnityConverters.FromUnityfloat3(data.Value);
				bitstream.WriteVector3f(converted_Value, 24, 2400);
			

	}

	public void GenericFieldVector1(IOutBitStream bitstream, GenericFieldVector1 data)
	{

			
				var converted_Value = coherenceToUnityConverters.FromUnityfloat3(data.Value);
				bitstream.WriteVector3f(converted_Value, 24, 2400);
			

	}

	public void GenericFieldVector2(IOutBitStream bitstream, GenericFieldVector2 data)
	{

			
				var converted_Value = coherenceToUnityConverters.FromUnityfloat3(data.Value);
				bitstream.WriteVector3f(converted_Value, 24, 2400);
			

	}

	public void GenericFieldVector3(IOutBitStream bitstream, GenericFieldVector3 data)
	{

			
				var converted_Value = coherenceToUnityConverters.FromUnityfloat3(data.Value);
				bitstream.WriteVector3f(converted_Value, 24, 2400);
			

	}

	public void GenericFieldString0(IOutBitStream bitstream, GenericFieldString0 data)
	{

			
				var converted_name = coherenceToUnityConverters.FromUnityFixedString64(data.name);
				bitstream.WriteShortString(converted_name);
			

	}

	public void GenericFieldString1(IOutBitStream bitstream, GenericFieldString1 data)
	{

			
				var converted_name = coherenceToUnityConverters.FromUnityFixedString64(data.name);
				bitstream.WriteShortString(converted_name);
			

	}

	public void GenericFieldString2(IOutBitStream bitstream, GenericFieldString2 data)
	{

			
				var converted_name = coherenceToUnityConverters.FromUnityFixedString64(data.name);
				bitstream.WriteShortString(converted_name);
			

	}

	public void GenericFieldString4(IOutBitStream bitstream, GenericFieldString4 data)
	{

			
				var converted_name = coherenceToUnityConverters.FromUnityFixedString64(data.name);
				bitstream.WriteShortString(converted_name);
			

	}

	public void GenericFieldQuaternion0(IOutBitStream bitstream, GenericFieldQuaternion0 data)
	{

			
				var converted_Value = coherenceToUnityConverters.FromUnityquaternion(data.Value);
				bitstream.WriteUnitRotation(converted_Value);
			

	}

	public void AuthorityTransfer(IOutBitStream bitstream, AuthorityTransfer data)
	{

			
				bitstream.WriteIntegerRange(data.participant, 15, -9999);
			

	}

	public void GenericCommand(IOutBitStream bitstream, GenericCommand data)
	{

			
				var converted_name = coherenceToUnityConverters.FromUnityFixedString64(data.name);
				bitstream.WriteShortString(converted_name);
			

			
				bitstream.WriteIntegerRange(data.paramInt1, 15, -9999);
			

			
				bitstream.WriteIntegerRange(data.paramInt2, 15, -9999);
			

			
				bitstream.WriteIntegerRange(data.paramInt3, 15, -9999);
			

			
				bitstream.WriteIntegerRange(data.paramInt4, 15, -9999);
			

			
				var converted_paramFloat1 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat1);
				bitstream.WriteFixedPoint(converted_paramFloat1, 24, 40000);
			

			
				var converted_paramFloat2 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat2);
				bitstream.WriteFixedPoint(converted_paramFloat2, 24, 40000);
			

			
				var converted_paramFloat3 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat3);
				bitstream.WriteFixedPoint(converted_paramFloat3, 24, 40000);
			

			
				var converted_paramFloat4 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat4);
				bitstream.WriteFixedPoint(converted_paramFloat4, 24, 40000);
			

			
				var converted_paramString = coherenceToUnityConverters.FromUnityFixedString64(data.paramString);
				bitstream.WriteShortString(converted_paramString);
			

	}


/// ------------------------ Requests --------------------------

	public void AuthorityTransferRequest(IOutBitStream bitstream, AuthorityTransferRequest data)
	{

			
				bitstream.WriteIntegerRange(data.participant, 15, -9999);
			

	}

	public void GenericCommandRequest(IOutBitStream bitstream, GenericCommandRequest data)
	{

			
				var converted_name = coherenceToUnityConverters.FromUnityFixedString64(data.name);
				bitstream.WriteShortString(converted_name);
			

			
				bitstream.WriteIntegerRange(data.paramInt1, 15, -9999);
			

			
				bitstream.WriteIntegerRange(data.paramInt2, 15, -9999);
			

			
				bitstream.WriteIntegerRange(data.paramInt3, 15, -9999);
			

			
				bitstream.WriteIntegerRange(data.paramInt4, 15, -9999);
			

			
				var converted_paramFloat1 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat1);
				bitstream.WriteFixedPoint(converted_paramFloat1, 24, 40000);
			

			
				var converted_paramFloat2 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat2);
				bitstream.WriteFixedPoint(converted_paramFloat2, 24, 40000);
			

			
				var converted_paramFloat3 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat3);
				bitstream.WriteFixedPoint(converted_paramFloat3, 24, 40000);
			

			
				var converted_paramFloat4 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat4);
				bitstream.WriteFixedPoint(converted_paramFloat4, 24, 40000);
			

			
				var converted_paramString = coherenceToUnityConverters.FromUnityFixedString64(data.paramString);
				bitstream.WriteShortString(converted_paramString);
			

	}



}

}

// ------------------ end of MessageSerializers.cs -----------------
#endregion
