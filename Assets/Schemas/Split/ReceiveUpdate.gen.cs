


#region ReceiveUpdate
// -----------------------------------
//  ReceiveUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Schema
{
	using Coherence.Ecs;
	using Coherence.DeltaEcs;
	using Coherence.Replication.Unity;
	using Coherence.Replication.Client.Unity.Ecs;
	using global::Unity.Transforms;
	using global::Unity.Collections;
	using global::Unity.Entities;
	using Coherence.Brook;
	using Coherence.Log;
	using Coherence.SimulationFrame;
	using global::Coherence.Generated.FirstProject;

	public class ReceiveUpdate : IReceiveUpdate
	{
		private readonly ISchemaSpecificComponentDeserialize componentDeserialize;
		private readonly ILog log;
		private UnityMapper mapper;
		private readonly ISchemaSpecificComponentDeserializerAndSkip componentSkip;
		private NativeHashMap<Entity, DetectedEntityDeletion> destroyedEntities;

		public ReceiveUpdate(ISchemaSpecificComponentDeserialize componentDeserialize,  ISchemaSpecificComponentDeserializerAndSkip componentSkip, UnityMapper mapper, NativeHashMap<Entity, DetectedEntityDeletion> destroyedEntities, ILog log)
		{
			this.componentDeserialize = componentDeserialize;
			this.componentSkip = componentSkip;
			this.mapper = mapper;
			this.log = log;
			this.destroyedEntities = destroyedEntities;
		}

		private void DestroyComponentData(EntityManager entityManager, Entity entity, uint componentType)
		{
			switch (componentType)
			{

				case TypeIds.InternalWorldPosition:
				{
					var hasComponentData = entityManager.HasComponent<Translation>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<Translation>(entity);
					}
					break;
				}

				case TypeIds.InternalWorldOrientation:
				{
					var hasComponentData = entityManager.HasComponent<Rotation>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<Rotation>(entity);
					}
					break;
				}

				case TypeIds.InternalLocalUser:
				{
					var hasComponentData = entityManager.HasComponent<LocalUser>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<LocalUser>(entity);
					}
					break;
				}

				case TypeIds.InternalWorldPositionQuery:
				{
					var hasComponentData = entityManager.HasComponent<WorldPositionQuery>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<WorldPositionQuery>(entity);
					}
					break;
				}

				case TypeIds.InternalSessionBased:
				{
					var hasComponentData = entityManager.HasComponent<SessionBased>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<SessionBased>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericPrefabReference:
				{
					var hasComponentData = entityManager.HasComponent<GenericPrefabReference>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericPrefabReference>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericScale:
				{
					var hasComponentData = entityManager.HasComponent<GenericScale>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericScale>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt0>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt0>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt1>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt1>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt2>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt2>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt3>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt3>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt4>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt4>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt5:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt5>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt5>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt6:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt6>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt6>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt7:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt7>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt7>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt8:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt8>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt8>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt9:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt9>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldInt9>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat0>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat0>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat1>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat1>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat2>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat2>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat3>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat3>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat4>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat4>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat5:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat5>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat5>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat6:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat6>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat6>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat7:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat7>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat7>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat8:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat8>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat8>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat9:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat9>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldFloat9>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector0>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldVector0>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector1>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldVector1>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector2>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldVector2>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector3>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldVector3>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldString0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString0>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldString0>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldString1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString1>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldString1>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldString2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString2>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldString2>(entity);
					}
					break;
				}

				case TypeIds.InternalGenericFieldString4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString4>(entity);
					if (hasComponentData)
					{
						entityManager.RemoveComponent<GenericFieldString4>(entity);
					}
					break;
				}

				default:
				{
					log.Warning($"Unknown component", "component", componentType);
					break;
				}
			}
		}

		private void UpdateComponents(EntityManager entityManager, Entity entity, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
			var componentCount = Deserializator.ReadComponentCount(bitStream, log);
			for (var i = 0; i < componentCount; i++)
			{
				var componentState = Deserializator.ReadComponentState(bitStream, log);
				var componentId = Deserializator.ReadComponentId(bitStream, log);
				switch (componentState)
				{
					case ComponentState.Construct:
						{
							var componentTypeId = Deserializator.ReadComponentTypeId(bitStream, log);

							componentDeserialize.CreateIfNeededAndReadComponentDataUpdate(entityManager,
								entity, componentTypeId, simulationFrame, bitStream, log);
						}
						break;
					case ComponentState.Update:
						{
							// TODO: lookup component ID from state.
							var updateComponentTypeId = componentId;
							componentDeserialize.ReadComponentDataUpdate(entityManager, entity,
								updateComponentTypeId, simulationFrame, bitStream, log);
						}
						break;
					case ComponentState.Destruct:
						{
							var destroyComponentTypeId = componentId;
							DestroyComponentData(entityManager, entity, destroyComponentTypeId);
						}
						break;
				}
			}
		}

		public void PerformUpdate(EntityManager entityManager, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
			var deserializeEntity = new Deserializator();

			while (deserializeEntity.ReadEntity(bitStream, out var entityWithMeta, log))
			{
				var entity = mapper.ToUnityEntity(entityWithMeta.EntityId, false);

				// Skip locally destroyed entities
				if (destroyedEntities.ContainsKey(entity))
				{
					if (!entityWithMeta.IsDeleted)
					{
						DeserializeComponentSkip.SkipComponents(componentSkip, bitStream, log);
					}
					continue;
				}

				// Meta information concerns entity creation, destruction and ownership
				if (entityWithMeta.HasMeta)
				{
					entity = PerformEntityMetaUpdate(entityManager, entityWithMeta, entity);
				}

				// Deserialize and apply component updates
				if (entity != default)
				{
					if (entityManager.HasComponent<Simulated>(entity))
					{
						DeserializeComponentSkip.SkipComponents(componentSkip, bitStream, log);
						log.Warning($"Trying to update owned entity {entityWithMeta.EntityId}");
					}
					else
					{
						UpdateComponents(entityManager, entity, simulationFrame, bitStream);
					}
				} else if (!entityWithMeta.IsDeleted)
				{
					// An error has occurred if the entity is null unless it's because it was just deleted
					log.Warning($"Entity is missing {entityWithMeta.EntityId}");
				}
			}
		}

		private Entity PerformEntityMetaUpdate(EntityManager entityManager, Deserializator.EntityWithMeta entityWithMeta, Entity entity)
		{
			// Entities are CREATED implicitly if they do not exist
			if (entity == default || !entityManager.Exists(entity))
			{
				if (entity != default)
				{
					UnityEngine.Debug.LogWarning("entity might still be mapped: " + entity + " CID: " + entityWithMeta.EntityId);
				}
				if (mapper.IsEntityIdInUse(entityWithMeta.EntityId))
				{
					UnityEngine.Debug.LogWarning("entity index already in use: " + entityWithMeta.EntityId);
					return default;
				}
				entity = entityManager.CreateEntity();
				mapper.Add(entityWithMeta.EntityId, entity);
				entityManager.AddComponent<Mapped>(entity);
			}

			// Entities OWNERSHIP determines iff they should have Simulated
			var hasComponentData = entityManager.HasComponent<Simulated>(entity);
			if (hasComponentData && !entityWithMeta.Ownership)
			{
				entityManager.RemoveComponent<Simulated>(entity);
				entityManager.RemoveComponent<LingerSimulated>(entity);
				RemoveSyncComponents(entityManager, entity);
			}
			else if (!hasComponentData && entityWithMeta.Ownership)
			{
				entityManager.AddComponentData(entity, new Simulated());
				RemoveInterpolationComponents(entityManager, entity);
			}

			// Entities are DELETED explicitly by the IsDeleted flag
			if (entityWithMeta.IsDeleted)
			{
				if (!entityWithMeta.Ownership)
				{
					log.Debug($"Deleting entity {entityWithMeta.Ownership} {entityWithMeta.EntityId}");
					if (entity != default)
					{
						if (entityManager.Exists(entity))
						{
							mapper.Remove(entityWithMeta.EntityId); // This internally requires entity to exist...
							entityManager.RemoveComponent<LingerSimulated>(entity);
							entityManager.DestroyEntity(entity);    // ...so this must be executed afterwards ...
						}
						else
						{
							log.Warning($"Entity has already been deleted: {entityWithMeta.EntityId} : {entity}");
						}
					}
					else
					{
						log.Warning($"Attempted to delete missing entity: {entityWithMeta.EntityId}");
					}
				}
				else
				{
					log.Warning($"Attempted to delete owned entity: {entityWithMeta.EntityId}");
				}

				return default;
			}

			return entity;
		}

		public void UpdateResendMask(EntityManager entityManager, Coherence.Ecs.SerializeEntityID entityId, uint componentTypeId, uint fieldMask)
		{
			var entity = mapper.ToUnityEntity(entityId);

			switch (componentTypeId)
			{

				case TypeIds.InternalWorldPosition:
				{
					var hasComponentData = entityManager.HasComponent<WorldPosition_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<WorldPosition_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalWorldOrientation:
				{
					var hasComponentData = entityManager.HasComponent<WorldOrientation_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<WorldOrientation_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalLocalUser:
				{
					var hasComponentData = entityManager.HasComponent<LocalUser_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<LocalUser_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalWorldPositionQuery:
				{
					var hasComponentData = entityManager.HasComponent<WorldPositionQuery_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<WorldPositionQuery_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalSessionBased:
				{
					var hasComponentData = entityManager.HasComponent<SessionBased_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<SessionBased_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericPrefabReference:
				{
					var hasComponentData = entityManager.HasComponent<GenericPrefabReference_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericPrefabReference_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericScale:
				{
					var hasComponentData = entityManager.HasComponent<GenericScale_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericScale_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt0_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt1_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt2_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt3_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt3_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt4_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt4_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt5:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt5_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt5_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt6:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt6_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt6_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt7:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt7_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt7_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt8:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt8_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt8_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt9:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt9_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt9_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat0_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat1_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat2_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat3_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat3_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat4_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat4_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat5:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat5_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat5_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat6:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat6_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat6_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat7:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat7_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat7_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat8:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat8_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat8_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat9:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat9_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat9_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector0_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector1_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector2_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector3_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector3_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString0_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString1_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString2_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString4_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString4_Sync>(entity);

						syncData.resendMask |= fieldMask;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				default:
				{
					log.Warning($"Unknown component", "component", componentTypeId);
					break;
				}
			}
		}

		public void UpdateHasReceivedConstructor(EntityManager entityManager, Coherence.Ecs.SerializeEntityID entityId, uint componentTypeId)
		{
			var entity = mapper.ToUnityEntity(entityId, false);

			// The entity has been deleted since the packet was sent
			if (destroyedEntities.ContainsKey(entity))
			{
				return;
			}

			if (!entityManager.Exists(entity))
			{
				log.Warning($"Entity does not exist: {entity} ComponentTypeId: {componentTypeId}");
				return;
			}

			if (!entityManager.HasComponent<Simulated>(entity))
			{
				// Ownership may have been lost since the packet was sent
				log.Trace($"Entity is missing Simulated: {entity} ComponentTypeId: {componentTypeId}");
				return;
			}

			var sim = entityManager.GetComponentData<Simulated>(entity);
			sim.hasReceivedConstructor = true;

			switch (componentTypeId)
			{

				case TypeIds.InternalWorldPosition:
				{
					var hasComponentData = entityManager.HasComponent<WorldPosition_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<WorldPosition_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} WorldPosition_Sync");
					}
					break;
				}

				case TypeIds.InternalWorldOrientation:
				{
					var hasComponentData = entityManager.HasComponent<WorldOrientation_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<WorldOrientation_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} WorldOrientation_Sync");
					}
					break;
				}

				case TypeIds.InternalLocalUser:
				{
					var hasComponentData = entityManager.HasComponent<LocalUser_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<LocalUser_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} LocalUser_Sync");
					}
					break;
				}

				case TypeIds.InternalWorldPositionQuery:
				{
					var hasComponentData = entityManager.HasComponent<WorldPositionQuery_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<WorldPositionQuery_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} WorldPositionQuery_Sync");
					}
					break;
				}

				case TypeIds.InternalSessionBased:
				{
					var hasComponentData = entityManager.HasComponent<SessionBased_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<SessionBased_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} SessionBased_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericPrefabReference:
				{
					var hasComponentData = entityManager.HasComponent<GenericPrefabReference_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericPrefabReference_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericPrefabReference_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericScale:
				{
					var hasComponentData = entityManager.HasComponent<GenericScale_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericScale_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericScale_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt0_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt0_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt1_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt1_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt2_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt2_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt3_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt3_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt3_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt4_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt4_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt4_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt5:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt5_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt5_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt5_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt6:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt6_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt6_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt6_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt7:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt7_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt7_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt7_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt8:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt8_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt8_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt8_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldInt9:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldInt9_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldInt9_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldInt9_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat0_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat0_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat1_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat1_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat2_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat2_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat3_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat3_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat3_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat4_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat4_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat4_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat5:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat5_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat5_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat5_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat6:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat6_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat6_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat6_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat7:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat7_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat7_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat7_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat8:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat8_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat8_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat8_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldFloat9:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat9_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldFloat9_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldFloat9_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector0_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldVector0_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector1_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldVector1_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector2_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldVector2_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldVector3:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldVector3_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldVector3_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldVector3_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString0:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString0_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString0_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldString0_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString1:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString1_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString1_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldString1_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString2:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString2_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString2_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldString2_Sync");
					}
					break;
				}

				case TypeIds.InternalGenericFieldString4:
				{
					var hasComponentData = entityManager.HasComponent<GenericFieldString4_Sync>(entity);
					if (hasComponentData)
					{
						var syncData = entityManager.GetComponentData<GenericFieldString4_Sync>(entity);
						syncData.hasReceivedConstructor = true;
						entityManager.SetComponentData(entity, syncData);
					} else
					{
						// Ownership may have been lost since the packet was sent
						log.Trace($"Sync component has been destroyed: {entity} GenericFieldString4_Sync");
					}
					break;
				}

				default:
				{
					log.Warning($"Unknown component", "component", componentTypeId);
					break;
				}
			}
		}

		public void UpdateResendDestroyed(EntityManager entityManager, Coherence.Ecs.SerializeEntityID entityId, AbsoluteSimulationFrame simulationFrame)
		{
			var entity = mapper.ToUnityEntity(entityId, false);
			if (entity == default)
			{
				log.Warning($"Destroyed entity {entityId} missing from mapper");
				return;
			}

			// Flag this entity destruction to be resent
			destroyedEntities[entity] = new DetectedEntityDeletion { Entity = entity, simulationFrame = (ulong)simulationFrame.Frame, serialized = false };
		}

		private void RemoveSyncComponents(EntityManager entityManager, Entity entity)
		{

			if (entityManager.HasComponent<WorldPosition_Sync>(entity))
			{
				entityManager.RemoveComponent<WorldPosition_Sync>(entity);
			}

			if (entityManager.HasComponent<WorldOrientation_Sync>(entity))
			{
				entityManager.RemoveComponent<WorldOrientation_Sync>(entity);
			}

			if (entityManager.HasComponent<LocalUser_Sync>(entity))
			{
				entityManager.RemoveComponent<LocalUser_Sync>(entity);
			}

			if (entityManager.HasComponent<WorldPositionQuery_Sync>(entity))
			{
				entityManager.RemoveComponent<WorldPositionQuery_Sync>(entity);
			}

			if (entityManager.HasComponent<SessionBased_Sync>(entity))
			{
				entityManager.RemoveComponent<SessionBased_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericPrefabReference_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericPrefabReference_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericScale_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericScale_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt0_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt0_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt1_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt1_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt2_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt2_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt3_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt3_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt4_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt4_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt5_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt5_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt6_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt6_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt7_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt7_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt8_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt8_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldInt9_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldInt9_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat0_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat0_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat1_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat1_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat2_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat2_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat3_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat3_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat4_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat4_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat5_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat5_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat6_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat6_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat7_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat7_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat8_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat8_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldFloat9_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldFloat9_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldVector0_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldVector0_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldVector1_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldVector1_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldVector2_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldVector2_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldVector3_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldVector3_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldString0_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldString0_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldString1_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldString1_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldString2_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldString2_Sync>(entity);
			}

			if (entityManager.HasComponent<GenericFieldString4_Sync>(entity))
			{
				entityManager.RemoveComponent<GenericFieldString4_Sync>(entity);
			}

		}

		private void RemoveInterpolationComponents(EntityManager entityManager, Entity entity)
		{


			if (entityManager.HasComponent<InterpolationComponent_Translation>(entity))
			{
				entityManager.RemoveComponent<InterpolationComponent_Translation>(entity);
			}
			if (entityManager.HasComponent<Sample_Translation>(entity))
			{
				entityManager.RemoveComponent<Sample_Translation>(entity);
			}



			if (entityManager.HasComponent<InterpolationComponent_Rotation>(entity))
			{
				entityManager.RemoveComponent<InterpolationComponent_Rotation>(entity);
			}
			if (entityManager.HasComponent<Sample_Rotation>(entity))
			{
				entityManager.RemoveComponent<Sample_Rotation>(entity);
			}




































































		}
	}
}
// ------------------ end of ReceiveUpdate.cs -----------------
#endregion