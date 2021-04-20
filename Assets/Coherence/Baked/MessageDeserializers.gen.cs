


#region MessageDeserializers
// -----------------------------------
//  MessageDeserializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using Coherence.Replication.Protocol.Definition;
	using global::Coherence.Generated;
	using Unity.Transforms;	
	using Replication.Unity;

	public class MessageDeserializers
	{
    	private CoherenceToUnityConverters coherenceToUnityConverters;

    	public MessageDeserializers(UnityMapper mapper)
    	{
        	coherenceToUnityConverters = new CoherenceToUnityConverters(mapper);
    	}

		public void Translation(IInBitStream bitstream, ref Translation data)
		{
			var value = bitstream.ReadVector3f(24, 2400);
			data.Value = coherenceToUnityConverters.ToUnityfloat3(value);     
		}

		public void Rotation(IInBitStream bitstream, ref Rotation data)
		{
			var value = bitstream.ReadUnitRotation();
			data.Value = coherenceToUnityConverters.ToUnityquaternion(value);     
		}

		public void LocalUser(IInBitStream bitstream, ref LocalUser data)
		{
			data.localIndex =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void WorldPositionQuery(IInBitStream bitstream, ref WorldPositionQuery data)
		{
			var position = bitstream.ReadVector3f(24, 2400);
			data.position = coherenceToUnityConverters.ToUnityfloat3(position);
			var radius = bitstream.ReadFixedPoint(24, 2400);
			data.radius = coherenceToUnityConverters.ToUnityfloat(radius);     
		}

		public void ArchetypeComponent(IInBitStream bitstream, ref ArchetypeComponent data)
		{
			data.index =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void Persistence(IInBitStream bitstream, ref Persistence data)
		{
			var uuid = bitstream.ReadShortString();
			data.uuid = coherenceToUnityConverters.ToUnityFixedString64(uuid);
			var expiry = bitstream.ReadShortString();
			data.expiry = coherenceToUnityConverters.ToUnityFixedString64(expiry);     
		}

		public void ConnectedEntity(IInBitStream bitstream, ref ConnectedEntity data)
		{
			data.value =  bitstream.ReadEntity();     
		}

		public void GenericPrefabReference(IInBitStream bitstream, ref GenericPrefabReference data)
		{
			var prefab = bitstream.ReadShortString();
			data.prefab = coherenceToUnityConverters.ToUnityFixedString64(prefab);     
		}

		public void GenericScale(IInBitStream bitstream, ref GenericScale data)
		{
			var Value = bitstream.ReadVector3f(24, 2400);
			data.Value = coherenceToUnityConverters.ToUnityfloat3(Value);     
		}

		public void GenericFieldInt0(IInBitStream bitstream, ref GenericFieldInt0 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt1(IInBitStream bitstream, ref GenericFieldInt1 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt2(IInBitStream bitstream, ref GenericFieldInt2 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt3(IInBitStream bitstream, ref GenericFieldInt3 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt4(IInBitStream bitstream, ref GenericFieldInt4 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt5(IInBitStream bitstream, ref GenericFieldInt5 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt6(IInBitStream bitstream, ref GenericFieldInt6 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt7(IInBitStream bitstream, ref GenericFieldInt7 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt8(IInBitStream bitstream, ref GenericFieldInt8 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldInt9(IInBitStream bitstream, ref GenericFieldInt9 data)
		{
			data.number =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericFieldBool0(IInBitStream bitstream, ref GenericFieldBool0 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool1(IInBitStream bitstream, ref GenericFieldBool1 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool2(IInBitStream bitstream, ref GenericFieldBool2 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool3(IInBitStream bitstream, ref GenericFieldBool3 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool4(IInBitStream bitstream, ref GenericFieldBool4 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool5(IInBitStream bitstream, ref GenericFieldBool5 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool6(IInBitStream bitstream, ref GenericFieldBool6 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool7(IInBitStream bitstream, ref GenericFieldBool7 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool8(IInBitStream bitstream, ref GenericFieldBool8 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldBool9(IInBitStream bitstream, ref GenericFieldBool9 data)
		{
			data.number =  bitstream.ReadBool();     
		}

		public void GenericFieldFloat0(IInBitStream bitstream, ref GenericFieldFloat0 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat1(IInBitStream bitstream, ref GenericFieldFloat1 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat2(IInBitStream bitstream, ref GenericFieldFloat2 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat3(IInBitStream bitstream, ref GenericFieldFloat3 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat4(IInBitStream bitstream, ref GenericFieldFloat4 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat5(IInBitStream bitstream, ref GenericFieldFloat5 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat6(IInBitStream bitstream, ref GenericFieldFloat6 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat7(IInBitStream bitstream, ref GenericFieldFloat7 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat8(IInBitStream bitstream, ref GenericFieldFloat8 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldFloat9(IInBitStream bitstream, ref GenericFieldFloat9 data)
		{
			var number = bitstream.ReadFixedPoint(24, 2400);
			data.number = coherenceToUnityConverters.ToUnityfloat(number);     
		}

		public void GenericFieldVector0(IInBitStream bitstream, ref GenericFieldVector0 data)
		{
			var Value = bitstream.ReadVector3f(24, 2400);
			data.Value = coherenceToUnityConverters.ToUnityfloat3(Value);     
		}

		public void GenericFieldVector1(IInBitStream bitstream, ref GenericFieldVector1 data)
		{
			var Value = bitstream.ReadVector3f(24, 2400);
			data.Value = coherenceToUnityConverters.ToUnityfloat3(Value);     
		}

		public void GenericFieldVector2(IInBitStream bitstream, ref GenericFieldVector2 data)
		{
			var Value = bitstream.ReadVector3f(24, 2400);
			data.Value = coherenceToUnityConverters.ToUnityfloat3(Value);     
		}

		public void GenericFieldVector3(IInBitStream bitstream, ref GenericFieldVector3 data)
		{
			var Value = bitstream.ReadVector3f(24, 2400);
			data.Value = coherenceToUnityConverters.ToUnityfloat3(Value);     
		}

		public void GenericFieldString0(IInBitStream bitstream, ref GenericFieldString0 data)
		{
			var name = bitstream.ReadShortString();
			data.name = coherenceToUnityConverters.ToUnityFixedString64(name);     
		}

		public void GenericFieldString1(IInBitStream bitstream, ref GenericFieldString1 data)
		{
			var name = bitstream.ReadShortString();
			data.name = coherenceToUnityConverters.ToUnityFixedString64(name);     
		}

		public void GenericFieldString2(IInBitStream bitstream, ref GenericFieldString2 data)
		{
			var name = bitstream.ReadShortString();
			data.name = coherenceToUnityConverters.ToUnityFixedString64(name);     
		}

		public void GenericFieldString3(IInBitStream bitstream, ref GenericFieldString3 data)
		{
			var name = bitstream.ReadShortString();
			data.name = coherenceToUnityConverters.ToUnityFixedString64(name);     
		}

		public void GenericFieldString4(IInBitStream bitstream, ref GenericFieldString4 data)
		{
			var name = bitstream.ReadShortString();
			data.name = coherenceToUnityConverters.ToUnityFixedString64(name);     
		}

		public void GenericFieldQuaternion0(IInBitStream bitstream, ref GenericFieldQuaternion0 data)
		{
			var Value = bitstream.ReadUnitRotation();
			data.Value = coherenceToUnityConverters.ToUnityquaternion(Value);     
		}

		public void GenericFieldEntity0(IInBitStream bitstream, ref GenericFieldEntity0 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity1(IInBitStream bitstream, ref GenericFieldEntity1 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity2(IInBitStream bitstream, ref GenericFieldEntity2 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity3(IInBitStream bitstream, ref GenericFieldEntity3 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity4(IInBitStream bitstream, ref GenericFieldEntity4 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity5(IInBitStream bitstream, ref GenericFieldEntity5 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity6(IInBitStream bitstream, ref GenericFieldEntity6 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity7(IInBitStream bitstream, ref GenericFieldEntity7 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity8(IInBitStream bitstream, ref GenericFieldEntity8 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void GenericFieldEntity9(IInBitStream bitstream, ref GenericFieldEntity9 data)
		{
			data.Value =  bitstream.ReadEntity();     
		}

		public void Cube_Cube(IInBitStream bitstream, ref Cube_Cube data)
		{
			data.friend =  bitstream.ReadEntity();     
		}

		public void Player_Controller(IInBitStream bitstream, ref Player_Controller data)
		{
			data.otherPlayer =  bitstream.ReadEntity();
			data.otherPlayerTransform =  bitstream.ReadEntity();
			data.otherPlayerSync =  bitstream.ReadEntity();     
		}

		public void AuthorityTransfer(IInBitStream bitstream, ref AuthorityTransfer data)
		{
			data.participant =  bitstream.ReadIntegerRange(15, -9999);     
		}

		public void GenericCommand(IInBitStream bitstream, ref GenericCommand data)
		{
			var name = bitstream.ReadShortString();
			data.name = coherenceToUnityConverters.ToUnityFixedString64(name);
			data.paramInt1 =  bitstream.ReadIntegerRange(15, -9999);
			data.paramInt2 =  bitstream.ReadIntegerRange(15, -9999);
			data.paramInt3 =  bitstream.ReadIntegerRange(15, -9999);
			data.paramInt4 =  bitstream.ReadIntegerRange(15, -9999);
			var paramFloat1 = bitstream.ReadFixedPoint(24, 2400);
			data.paramFloat1 = coherenceToUnityConverters.ToUnityfloat(paramFloat1);
			var paramFloat2 = bitstream.ReadFixedPoint(24, 2400);
			data.paramFloat2 = coherenceToUnityConverters.ToUnityfloat(paramFloat2);
			var paramFloat3 = bitstream.ReadFixedPoint(24, 2400);
			data.paramFloat3 = coherenceToUnityConverters.ToUnityfloat(paramFloat3);
			var paramFloat4 = bitstream.ReadFixedPoint(24, 2400);
			data.paramFloat4 = coherenceToUnityConverters.ToUnityfloat(paramFloat4);
			data.paramBool1 =  bitstream.ReadBool();
			data.paramBool2 =  bitstream.ReadBool();
			data.paramBool3 =  bitstream.ReadBool();
			data.paramBool4 =  bitstream.ReadBool();
			data.paramEntity1 =  bitstream.ReadEntity();
			data.paramEntity2 =  bitstream.ReadEntity();
			data.paramEntity3 =  bitstream.ReadEntity();
			data.paramEntity4 =  bitstream.ReadEntity();
			var paramString = bitstream.ReadShortString();
			data.paramString = coherenceToUnityConverters.ToUnityFixedString64(paramString);     
		}

		public void Player_Controller_Foo(IInBitStream bitstream, ref Player_Controller_Foo data)
		{
			data.g =  bitstream.ReadEntity();     
		}

		public void Player_Controller_Boo(IInBitStream bitstream, ref Player_Controller_Boo data)
		{     
		}

		public void TransferAction(IInBitStream bitstream, ref TransferAction data)
		{
			data.participant =  bitstream.ReadIntegerRange(15, -9999);
			data.accepted =  bitstream.ReadBool();     
		}
	}
}

// ------------------ end of MessageDeserializers.cs -----------------
#endregion
