


#region ReceiveUpdate
// -----------------------------------
//  ReceiveUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
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
		private UnityMapper mapper;
		private readonly ISchemaSpecificComponentDeserializerAndSkip componentSkip;
		private NativeHashMap<Entity, DetectedEntityDeletion> destroyedEntities;

		public ReceiveUpdate(ISchemaSpecificComponentDeserialize componentDeserialize,  ISchemaSpecificComponentDeserializerAndSkip componentSkip, UnityMapper mapper, NativeHashMap<Entity, DetectedEntityDeletion> destroyedEntities)
		{
			this.componentDeserialize = componentDeserialize;
			this.componentSkip = componentSkip;
			this.mapper = mapper;
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

				default:
				{
					Log.Warning($"Unknown component", "component", componentType);
					break;
				}
			}
		}

		private void UpdateComponents(EntityManager entityManager, Entity entity, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
			var componentCount = Deserializator.ReadComponentCount(bitStream);
			for (var i = 0; i < componentCount; i++)
			{
				var componentState = Deserializator.ReadComponentState(bitStream);
				var componentId = Deserializator.ReadComponentId(bitStream);
				switch (componentState)
				{
					case ComponentState.Construct:
						{
							var componentTypeId = Deserializator.ReadComponentTypeId(bitStream);

							componentDeserialize.CreateIfNeededAndReadComponentDataUpdate(entityManager,
								entity, componentTypeId, simulationFrame, bitStream);
						}
						break;
					case ComponentState.Update:
						{
							// TODO: lookup component ID from state.
							var updateComponentTypeId = componentId;
							componentDeserialize.ReadComponentDataUpdate(entityManager, entity,
								updateComponentTypeId, simulationFrame, bitStream);
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

			while (deserializeEntity.ReadEntity(bitStream, out var entityWithMeta))
			{
				var entity = mapper.ToUnityEntity(entityWithMeta.EntityId, false);

				// Skip locally destroyed entities
				if (destroyedEntities.ContainsKey(entity))
				{
					if (!entityWithMeta.IsDeleted)
					{
						DeserializeComponentSkip.SkipComponents(componentSkip, bitStream);
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
						DeserializeComponentSkip.SkipComponents(componentSkip, bitStream);
						Log.Warning($"Trying to update owned entity {entityWithMeta.EntityId}");
					}
					else
					{
						UpdateComponents(entityManager, entity, simulationFrame, bitStream);
					}
				} else if (!entityWithMeta.IsDeleted)
				{
					// An error has occurred if the entity is null unless it's because it was just deleted
					Log.Warning($"Entity is missing {entityWithMeta.EntityId}");
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
					Log.Debug($"Deleting entity {entityWithMeta.Ownership} {entityWithMeta.EntityId}");
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
							Log.Warning($"Entity has already been deleted: {entityWithMeta.EntityId} : {entity}");
						}
					}
					else
					{
						Log.Warning($"Attempted to delete missing entity: {entityWithMeta.EntityId}");
					}
				}
				else
				{
					Log.Warning($"Attempted to delete owned entity: {entityWithMeta.EntityId}");
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
						Log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
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
						Log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
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
						Log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
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
						Log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
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
						Log.Warning($"Entity or component has been destroyed: {entity} ComponentTypeId: {componentTypeId}");
					}
					break;
				}

				default:
				{
					Log.Warning($"Unknown component", "component", componentTypeId);
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
				Log.Warning($"Entity does not exist: {entity} ComponentTypeId: {componentTypeId}");
				return;
			}

			if (!entityManager.HasComponent<Simulated>(entity))
			{
				// Ownership may have been lost since the packet was sent
				Log.Trace($"Entity is missing Simulated: {entity} ComponentTypeId: {componentTypeId}");
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
						Log.Trace($"Sync component has been destroyed: {entity} WorldPosition_Sync");
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
						Log.Trace($"Sync component has been destroyed: {entity} WorldOrientation_Sync");
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
						Log.Trace($"Sync component has been destroyed: {entity} LocalUser_Sync");
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
						Log.Trace($"Sync component has been destroyed: {entity} WorldPositionQuery_Sync");
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
						Log.Trace($"Sync component has been destroyed: {entity} SessionBased_Sync");
					}
					break;
				}

				default:
				{
					Log.Warning($"Unknown component", "component", componentTypeId);
					break;
				}
			}
		}

		public void UpdateResendDestroyed(EntityManager entityManager, Coherence.Ecs.SerializeEntityID entityId, AbsoluteSimulationFrame simulationFrame)
		{
			var entity = mapper.ToUnityEntity(entityId, false);
			if (entity == default)
			{
				Log.Warning($"Destroyed entity {entityId} missing from mapper");
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
