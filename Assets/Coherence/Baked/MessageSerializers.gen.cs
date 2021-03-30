


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
			bitstream.WriteFixedPoint(converted_radius, 24, 2400);
		}
	
		public void ArchetypeComponent(IOutBitStream bitstream, ArchetypeComponent data)
		{
			bitstream.WriteIntegerRange(data.index, 15, -9999);
		}
	
		public void Persistence(IOutBitStream bitstream, Persistence data)
		{
			var converted_uuid = coherenceToUnityConverters.FromUnityFixedString64(data.uuid);
			bitstream.WriteShortString(converted_uuid);
			var converted_expiry = coherenceToUnityConverters.FromUnityFixedString64(data.expiry);
			bitstream.WriteShortString(converted_expiry);
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
	
		public void GenericFieldBool0(IOutBitStream bitstream, GenericFieldBool0 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool1(IOutBitStream bitstream, GenericFieldBool1 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool2(IOutBitStream bitstream, GenericFieldBool2 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool3(IOutBitStream bitstream, GenericFieldBool3 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool4(IOutBitStream bitstream, GenericFieldBool4 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool5(IOutBitStream bitstream, GenericFieldBool5 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool6(IOutBitStream bitstream, GenericFieldBool6 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool7(IOutBitStream bitstream, GenericFieldBool7 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool8(IOutBitStream bitstream, GenericFieldBool8 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldBool9(IOutBitStream bitstream, GenericFieldBool9 data)
		{
			bitstream.WriteBool(data.number);
		}
	
		public void GenericFieldFloat0(IOutBitStream bitstream, GenericFieldFloat0 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat1(IOutBitStream bitstream, GenericFieldFloat1 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat2(IOutBitStream bitstream, GenericFieldFloat2 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat3(IOutBitStream bitstream, GenericFieldFloat3 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat4(IOutBitStream bitstream, GenericFieldFloat4 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat5(IOutBitStream bitstream, GenericFieldFloat5 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat6(IOutBitStream bitstream, GenericFieldFloat6 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat7(IOutBitStream bitstream, GenericFieldFloat7 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat8(IOutBitStream bitstream, GenericFieldFloat8 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
		}
	
		public void GenericFieldFloat9(IOutBitStream bitstream, GenericFieldFloat9 data)
		{
			var converted_number = coherenceToUnityConverters.FromUnityfloat(data.number);
			bitstream.WriteFixedPoint(converted_number, 24, 2400);
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
	
		public void GenericFieldEntity0(IOutBitStream bitstream, GenericFieldEntity0 data)
		{
			var converted_Value = coherenceToUnityConverters.FromUnityEntity(data.Value);
			bitstream.WriteEntity(converted_Value);
		}
	
		public void GenericFieldEntity1(IOutBitStream bitstream, GenericFieldEntity1 data)
		{
			var converted_Value = coherenceToUnityConverters.FromUnityEntity(data.Value);
			bitstream.WriteEntity(converted_Value);
		}
	
		public void GenericFieldEntity2(IOutBitStream bitstream, GenericFieldEntity2 data)
		{
			var converted_Value = coherenceToUnityConverters.FromUnityEntity(data.Value);
			bitstream.WriteEntity(converted_Value);
		}
	
		public void GenericFieldEntity3(IOutBitStream bitstream, GenericFieldEntity3 data)
		{
			var converted_Value = coherenceToUnityConverters.FromUnityEntity(data.Value);
			bitstream.WriteEntity(converted_Value);
		}
	
		public void Cube_Cube(IOutBitStream bitstream, Cube_Cube data)
		{
			var converted_friend = coherenceToUnityConverters.FromUnityEntity(data.friend);
			bitstream.WriteEntity(converted_friend);
			var converted_s = coherenceToUnityConverters.FromUnityFixedString64(data.s);
			bitstream.WriteShortString(converted_s);
			bitstream.WriteIntegerRange(data.i, 15, -9999);
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
			bitstream.WriteFixedPoint(converted_paramFloat1, 24, 2400);
			var converted_paramFloat2 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat2);
			bitstream.WriteFixedPoint(converted_paramFloat2, 24, 2400);
			var converted_paramFloat3 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat3);
			bitstream.WriteFixedPoint(converted_paramFloat3, 24, 2400);
			var converted_paramFloat4 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat4);
			bitstream.WriteFixedPoint(converted_paramFloat4, 24, 2400);
			var converted_paramString = coherenceToUnityConverters.FromUnityFixedString64(data.paramString);
			bitstream.WriteShortString(converted_paramString);
		}
	
		public void TransferAction(IOutBitStream bitstream, TransferAction data)
		{
			bitstream.WriteIntegerRange(data.participant, 15, -9999);
			bitstream.WriteBool(data.accepted);
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
			bitstream.WriteFixedPoint(converted_paramFloat1, 24, 2400);
			var converted_paramFloat2 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat2);
			bitstream.WriteFixedPoint(converted_paramFloat2, 24, 2400);
			var converted_paramFloat3 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat3);
			bitstream.WriteFixedPoint(converted_paramFloat3, 24, 2400);
			var converted_paramFloat4 = coherenceToUnityConverters.FromUnityfloat(data.paramFloat4);
			bitstream.WriteFixedPoint(converted_paramFloat4, 24, 2400);
			var converted_paramString = coherenceToUnityConverters.FromUnityFixedString64(data.paramString);
			bitstream.WriteShortString(converted_paramString);
		}
	}
}

// ------------------ end of MessageSerializers.cs -----------------
#endregion
