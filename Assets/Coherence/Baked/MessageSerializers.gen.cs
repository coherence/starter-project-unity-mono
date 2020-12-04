


#region MessageSerializers
// -----------------------------------
//  MessageSerializers.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
{
	using Coherence.Replication.Protocol.Definition;
	using global::Coherence.Generated.FirstProject;
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


/// ------------------------ Requests --------------------------



}

}

// ------------------ end of MessageSerializers.cs -----------------
#endregion
