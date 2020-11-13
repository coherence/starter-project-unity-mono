


#region SerializeCreateEntity
// -----------------------------------
//  SerializeCreateEntity.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Schema
{
    using Message.Serializer.Serialize;
    using Coherence.Log;
    using Unity.Entities;
    using Unity.Transforms;
    using IOutBitStream = Coherence.Brook.IOutBitStream;
    using global::Coherence.Generated.FirstProject;
    using Coherence.Replication.Unity;

    public class SerializeCreateEntityRequest
    {
        private MessageSerializers messageSerializers;

        public SerializeCreateEntityRequest(UnityMapper mapper)
        {
            messageSerializers = new MessageSerializers(mapper);
        }
        
        public void SerializeComponentsInMessageFormat(EntityManager entityManager,
            Entity entity, uint[] foundComponentTypes, IOutBitStream bitStream, ILog log)
        {
            var protocolOutStream = new FieldStream.Serialize.Streams.OutBitStream(bitStream, log);

            foreach (var coherenceComponentType in foundComponentTypes)
            {
				ComponentTypeIdSerializer.Serialize(coherenceComponentType, bitStream);

				switch (coherenceComponentType)
                {
					
                    case TypeIds.InternalWorldPosition:
					{
						var data = entityManager.GetComponentData<Translation>(entity);
						messageSerializers.WorldPosition(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalWorldOrientation:
					{
						var data = entityManager.GetComponentData<Rotation>(entity);
						messageSerializers.WorldOrientation(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalLocalUser:
					{
						var data = entityManager.GetComponentData<LocalUser>(entity);
						messageSerializers.LocalUser(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalWorldPositionQuery:
					{
						var data = entityManager.GetComponentData<WorldPositionQuery>(entity);
						messageSerializers.WorldPositionQuery(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalSessionBased:
					{
						var data = entityManager.GetComponentData<SessionBased>(entity);
						messageSerializers.SessionBased(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericPrefabReference:
					{
						var data = entityManager.GetComponentData<GenericPrefabReference>(entity);
						messageSerializers.GenericPrefabReference(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericScale:
					{
						var data = entityManager.GetComponentData<GenericScale>(entity);
						messageSerializers.GenericScale(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt0:
					{
						var data = entityManager.GetComponentData<GenericFieldInt0>(entity);
						messageSerializers.GenericFieldInt0(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt1:
					{
						var data = entityManager.GetComponentData<GenericFieldInt1>(entity);
						messageSerializers.GenericFieldInt1(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt2:
					{
						var data = entityManager.GetComponentData<GenericFieldInt2>(entity);
						messageSerializers.GenericFieldInt2(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt3:
					{
						var data = entityManager.GetComponentData<GenericFieldInt3>(entity);
						messageSerializers.GenericFieldInt3(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt4:
					{
						var data = entityManager.GetComponentData<GenericFieldInt4>(entity);
						messageSerializers.GenericFieldInt4(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt5:
					{
						var data = entityManager.GetComponentData<GenericFieldInt5>(entity);
						messageSerializers.GenericFieldInt5(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt6:
					{
						var data = entityManager.GetComponentData<GenericFieldInt6>(entity);
						messageSerializers.GenericFieldInt6(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt7:
					{
						var data = entityManager.GetComponentData<GenericFieldInt7>(entity);
						messageSerializers.GenericFieldInt7(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt8:
					{
						var data = entityManager.GetComponentData<GenericFieldInt8>(entity);
						messageSerializers.GenericFieldInt8(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldInt9:
					{
						var data = entityManager.GetComponentData<GenericFieldInt9>(entity);
						messageSerializers.GenericFieldInt9(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat0:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat0>(entity);
						messageSerializers.GenericFieldFloat0(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat1:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat1>(entity);
						messageSerializers.GenericFieldFloat1(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat2:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat2>(entity);
						messageSerializers.GenericFieldFloat2(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat3:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat3>(entity);
						messageSerializers.GenericFieldFloat3(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat4:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat4>(entity);
						messageSerializers.GenericFieldFloat4(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat5:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat5>(entity);
						messageSerializers.GenericFieldFloat5(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat6:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat6>(entity);
						messageSerializers.GenericFieldFloat6(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat7:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat7>(entity);
						messageSerializers.GenericFieldFloat7(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat8:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat8>(entity);
						messageSerializers.GenericFieldFloat8(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldFloat9:
					{
						var data = entityManager.GetComponentData<GenericFieldFloat9>(entity);
						messageSerializers.GenericFieldFloat9(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldVector0:
					{
						var data = entityManager.GetComponentData<GenericFieldVector0>(entity);
						messageSerializers.GenericFieldVector0(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldVector1:
					{
						var data = entityManager.GetComponentData<GenericFieldVector1>(entity);
						messageSerializers.GenericFieldVector1(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldVector2:
					{
						var data = entityManager.GetComponentData<GenericFieldVector2>(entity);
						messageSerializers.GenericFieldVector2(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldVector3:
					{
						var data = entityManager.GetComponentData<GenericFieldVector3>(entity);
						messageSerializers.GenericFieldVector3(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldString0:
					{
						var data = entityManager.GetComponentData<GenericFieldString0>(entity);
						messageSerializers.GenericFieldString0(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldString1:
					{
						var data = entityManager.GetComponentData<GenericFieldString1>(entity);
						messageSerializers.GenericFieldString1(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldString2:
					{
						var data = entityManager.GetComponentData<GenericFieldString2>(entity);
						messageSerializers.GenericFieldString2(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalGenericFieldString4:
					{
						var data = entityManager.GetComponentData<GenericFieldString4>(entity);
						messageSerializers.GenericFieldString4(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalNPCBehaviour:
					{
						var data = entityManager.GetComponentData<NPCBehaviour>(entity);
						messageSerializers.NPCBehaviour(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalRotationBehaviour:
					{
						var data = entityManager.GetComponentData<RotationBehaviour>(entity);
						messageSerializers.RotationBehaviour(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalShowNameAndState:
					{
						var data = entityManager.GetComponentData<ShowNameAndState>(entity);
						messageSerializers.ShowNameAndState(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalPlayerBehaviour:
					{
						var data = entityManager.GetComponentData<PlayerBehaviour>(entity);
						messageSerializers.PlayerBehaviour(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalBullet:
					{
						var data = entityManager.GetComponentData<Bullet>(entity);
						messageSerializers.Bullet(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalColorizeBehaviour:
					{
						var data = entityManager.GetComponentData<ColorizeBehaviour>(entity);
						messageSerializers.ColorizeBehaviour(protocolOutStream, data);
						break;
					}
					
                    case TypeIds.InternalController:
					{
						var data = entityManager.GetComponentData<Controller>(entity);
						messageSerializers.Controller(protocolOutStream, data);
						break;
					}
					

                    default:
                    {
                        log.Warning($"Unknown component", "component", coherenceComponentType);
                        break;
                    }
                }
            }
        }
    }
}

// ------------------ end of SerializeCreateEntity.cs -----------------
#endregion
