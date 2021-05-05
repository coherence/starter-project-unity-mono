


#region EntityReplacer
// -----------------------------------
//  EntityReplacer.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using Unity.Entities;
	using Unity.Transforms;
	using UnityEngine;
	using Coherence.Replication.Client.Unity.Ecs;

	public class EntityReplacer {
		public static void Replace(EntityManager entityManager, Entity networkEntity, Entity newEntity)
		{
#if UNITY_EDITOR
			if (newEntity.Index<10000) // Prevents exceptions caused by a built-in limit of 16k unique entity names.
			{
				entityManager.SetName(newEntity, $"Entity({newEntity.Index}) (remote)");
			}
#endif

			CopyComponents(entityManager, networkEntity, newEntity);

			var mapper = entityManager.World.GetExistingSystem<SyncSendSystem>().Sender.Mapper;
			if (!mapper.ToCoherenceEntityId(networkEntity, out var entityId))
			{
				Debug.LogError("Networked Entity not found in mapper: " + networkEntity); // Should not happen
			}

			mapper.Remove(entityId);
			mapper.Add(entityId, newEntity);
			entityManager.AddComponent<Mapped>(newEntity);
			entityManager.DestroyEntity(networkEntity);
		}

		private static void CopyComponents(EntityManager entityManager, Entity source, Entity destination)
		{
			/////////////////////////////////////////////////////////////
			// Copy SDK components 
			// Simulated and Orphan are defined in the SDK, 
			// and should be copied similar to schema components below
			/////////////////////////////////////////////////////////////

			if (entityManager.HasComponent<Simulated>(source))
			{
				if (!entityManager.HasComponent<Simulated>(destination)) {
					entityManager.AddComponentData<Simulated>(destination, new Simulated());
				}
				var data = entityManager.GetComponentData<Simulated>(source);
				entityManager.SetComponentData<Simulated>(destination, data);
			}
		
			if (entityManager.HasComponent<Orphan>(source))
			{
				entityManager.AddComponentData<Orphan>(destination, new Orphan());
			}

			/////////////////////////////////////////////////////////////
			// Copy schema components
			/////////////////////////////////////////////////////////////

			if (entityManager.HasComponent<Translation>(source))
			{
				// Translation is built in
				var data = entityManager.GetComponentData<Translation>(source);
				entityManager.SetComponentData<Translation>(destination, data);
			}

			if (entityManager.HasComponent<Rotation>(source))
			{
				// Rotation is built in
				var data = entityManager.GetComponentData<Rotation>(source);
				entityManager.SetComponentData<Rotation>(destination, data);
			}

			if (entityManager.HasComponent<LocalUser>(source))
			{ 
				// LocalUser has fields, will copy it.			
				if (!entityManager.HasComponent<LocalUser>(destination)) {
					entityManager.AddComponentData<LocalUser>(destination, new LocalUser());
				}
				var data = entityManager.GetComponentData<LocalUser>(source);
				entityManager.SetComponentData<LocalUser>(destination, data);
			}

			if (entityManager.HasComponent<WorldPositionQuery>(source))
			{ 
				// WorldPositionQuery has fields, will copy it.			
				if (!entityManager.HasComponent<WorldPositionQuery>(destination)) {
					entityManager.AddComponentData<WorldPositionQuery>(destination, new WorldPositionQuery());
				}
				var data = entityManager.GetComponentData<WorldPositionQuery>(source);
				entityManager.SetComponentData<WorldPositionQuery>(destination, data);
			}

			if (entityManager.HasComponent<ArchetypeComponent>(source))
			{ 
				// ArchetypeComponent has fields, will copy it.			
				if (!entityManager.HasComponent<ArchetypeComponent>(destination)) {
					entityManager.AddComponentData<ArchetypeComponent>(destination, new ArchetypeComponent());
				}
				var data = entityManager.GetComponentData<ArchetypeComponent>(source);
				entityManager.SetComponentData<ArchetypeComponent>(destination, data);
			}

			if (entityManager.HasComponent<Persistence>(source))
			{ 
				// Persistence has fields, will copy it.			
				if (!entityManager.HasComponent<Persistence>(destination)) {
					entityManager.AddComponentData<Persistence>(destination, new Persistence());
				}
				var data = entityManager.GetComponentData<Persistence>(source);
				entityManager.SetComponentData<Persistence>(destination, data);
			}

			if (entityManager.HasComponent<ConnectedEntity>(source))
			{ 
				// ConnectedEntity has fields, will copy it.			
				if (!entityManager.HasComponent<ConnectedEntity>(destination)) {
					entityManager.AddComponentData<ConnectedEntity>(destination, new ConnectedEntity());
				}
				var data = entityManager.GetComponentData<ConnectedEntity>(source);
				entityManager.SetComponentData<ConnectedEntity>(destination, data);
			}

			if (entityManager.HasComponent<GenericPrefabReference>(source))
			{ 
				// GenericPrefabReference has fields, will copy it.			
				if (!entityManager.HasComponent<GenericPrefabReference>(destination)) {
					entityManager.AddComponentData<GenericPrefabReference>(destination, new GenericPrefabReference());
				}
				var data = entityManager.GetComponentData<GenericPrefabReference>(source);
				entityManager.SetComponentData<GenericPrefabReference>(destination, data);
			}

			if (entityManager.HasComponent<GenericScale>(source))
			{ 
				// GenericScale has fields, will copy it.			
				if (!entityManager.HasComponent<GenericScale>(destination)) {
					entityManager.AddComponentData<GenericScale>(destination, new GenericScale());
				}
				var data = entityManager.GetComponentData<GenericScale>(source);
				entityManager.SetComponentData<GenericScale>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt0>(source))
			{ 
				// GenericFieldInt0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt0>(destination)) {
					entityManager.AddComponentData<GenericFieldInt0>(destination, new GenericFieldInt0());
				}
				var data = entityManager.GetComponentData<GenericFieldInt0>(source);
				entityManager.SetComponentData<GenericFieldInt0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt1>(source))
			{ 
				// GenericFieldInt1 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt1>(destination)) {
					entityManager.AddComponentData<GenericFieldInt1>(destination, new GenericFieldInt1());
				}
				var data = entityManager.GetComponentData<GenericFieldInt1>(source);
				entityManager.SetComponentData<GenericFieldInt1>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt2>(source))
			{ 
				// GenericFieldInt2 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt2>(destination)) {
					entityManager.AddComponentData<GenericFieldInt2>(destination, new GenericFieldInt2());
				}
				var data = entityManager.GetComponentData<GenericFieldInt2>(source);
				entityManager.SetComponentData<GenericFieldInt2>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt3>(source))
			{ 
				// GenericFieldInt3 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt3>(destination)) {
					entityManager.AddComponentData<GenericFieldInt3>(destination, new GenericFieldInt3());
				}
				var data = entityManager.GetComponentData<GenericFieldInt3>(source);
				entityManager.SetComponentData<GenericFieldInt3>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt4>(source))
			{ 
				// GenericFieldInt4 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt4>(destination)) {
					entityManager.AddComponentData<GenericFieldInt4>(destination, new GenericFieldInt4());
				}
				var data = entityManager.GetComponentData<GenericFieldInt4>(source);
				entityManager.SetComponentData<GenericFieldInt4>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt5>(source))
			{ 
				// GenericFieldInt5 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt5>(destination)) {
					entityManager.AddComponentData<GenericFieldInt5>(destination, new GenericFieldInt5());
				}
				var data = entityManager.GetComponentData<GenericFieldInt5>(source);
				entityManager.SetComponentData<GenericFieldInt5>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt6>(source))
			{ 
				// GenericFieldInt6 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt6>(destination)) {
					entityManager.AddComponentData<GenericFieldInt6>(destination, new GenericFieldInt6());
				}
				var data = entityManager.GetComponentData<GenericFieldInt6>(source);
				entityManager.SetComponentData<GenericFieldInt6>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt7>(source))
			{ 
				// GenericFieldInt7 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt7>(destination)) {
					entityManager.AddComponentData<GenericFieldInt7>(destination, new GenericFieldInt7());
				}
				var data = entityManager.GetComponentData<GenericFieldInt7>(source);
				entityManager.SetComponentData<GenericFieldInt7>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt8>(source))
			{ 
				// GenericFieldInt8 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt8>(destination)) {
					entityManager.AddComponentData<GenericFieldInt8>(destination, new GenericFieldInt8());
				}
				var data = entityManager.GetComponentData<GenericFieldInt8>(source);
				entityManager.SetComponentData<GenericFieldInt8>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldInt9>(source))
			{ 
				// GenericFieldInt9 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldInt9>(destination)) {
					entityManager.AddComponentData<GenericFieldInt9>(destination, new GenericFieldInt9());
				}
				var data = entityManager.GetComponentData<GenericFieldInt9>(source);
				entityManager.SetComponentData<GenericFieldInt9>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool0>(source))
			{ 
				// GenericFieldBool0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool0>(destination)) {
					entityManager.AddComponentData<GenericFieldBool0>(destination, new GenericFieldBool0());
				}
				var data = entityManager.GetComponentData<GenericFieldBool0>(source);
				entityManager.SetComponentData<GenericFieldBool0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool1>(source))
			{ 
				// GenericFieldBool1 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool1>(destination)) {
					entityManager.AddComponentData<GenericFieldBool1>(destination, new GenericFieldBool1());
				}
				var data = entityManager.GetComponentData<GenericFieldBool1>(source);
				entityManager.SetComponentData<GenericFieldBool1>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool2>(source))
			{ 
				// GenericFieldBool2 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool2>(destination)) {
					entityManager.AddComponentData<GenericFieldBool2>(destination, new GenericFieldBool2());
				}
				var data = entityManager.GetComponentData<GenericFieldBool2>(source);
				entityManager.SetComponentData<GenericFieldBool2>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool3>(source))
			{ 
				// GenericFieldBool3 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool3>(destination)) {
					entityManager.AddComponentData<GenericFieldBool3>(destination, new GenericFieldBool3());
				}
				var data = entityManager.GetComponentData<GenericFieldBool3>(source);
				entityManager.SetComponentData<GenericFieldBool3>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool4>(source))
			{ 
				// GenericFieldBool4 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool4>(destination)) {
					entityManager.AddComponentData<GenericFieldBool4>(destination, new GenericFieldBool4());
				}
				var data = entityManager.GetComponentData<GenericFieldBool4>(source);
				entityManager.SetComponentData<GenericFieldBool4>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool5>(source))
			{ 
				// GenericFieldBool5 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool5>(destination)) {
					entityManager.AddComponentData<GenericFieldBool5>(destination, new GenericFieldBool5());
				}
				var data = entityManager.GetComponentData<GenericFieldBool5>(source);
				entityManager.SetComponentData<GenericFieldBool5>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool6>(source))
			{ 
				// GenericFieldBool6 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool6>(destination)) {
					entityManager.AddComponentData<GenericFieldBool6>(destination, new GenericFieldBool6());
				}
				var data = entityManager.GetComponentData<GenericFieldBool6>(source);
				entityManager.SetComponentData<GenericFieldBool6>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool7>(source))
			{ 
				// GenericFieldBool7 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool7>(destination)) {
					entityManager.AddComponentData<GenericFieldBool7>(destination, new GenericFieldBool7());
				}
				var data = entityManager.GetComponentData<GenericFieldBool7>(source);
				entityManager.SetComponentData<GenericFieldBool7>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool8>(source))
			{ 
				// GenericFieldBool8 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool8>(destination)) {
					entityManager.AddComponentData<GenericFieldBool8>(destination, new GenericFieldBool8());
				}
				var data = entityManager.GetComponentData<GenericFieldBool8>(source);
				entityManager.SetComponentData<GenericFieldBool8>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldBool9>(source))
			{ 
				// GenericFieldBool9 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldBool9>(destination)) {
					entityManager.AddComponentData<GenericFieldBool9>(destination, new GenericFieldBool9());
				}
				var data = entityManager.GetComponentData<GenericFieldBool9>(source);
				entityManager.SetComponentData<GenericFieldBool9>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat0>(source))
			{ 
				// GenericFieldFloat0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat0>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat0>(destination, new GenericFieldFloat0());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat0>(source);
				entityManager.SetComponentData<GenericFieldFloat0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat1>(source))
			{ 
				// GenericFieldFloat1 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat1>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat1>(destination, new GenericFieldFloat1());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat1>(source);
				entityManager.SetComponentData<GenericFieldFloat1>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat2>(source))
			{ 
				// GenericFieldFloat2 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat2>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat2>(destination, new GenericFieldFloat2());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat2>(source);
				entityManager.SetComponentData<GenericFieldFloat2>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat3>(source))
			{ 
				// GenericFieldFloat3 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat3>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat3>(destination, new GenericFieldFloat3());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat3>(source);
				entityManager.SetComponentData<GenericFieldFloat3>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat4>(source))
			{ 
				// GenericFieldFloat4 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat4>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat4>(destination, new GenericFieldFloat4());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat4>(source);
				entityManager.SetComponentData<GenericFieldFloat4>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat5>(source))
			{ 
				// GenericFieldFloat5 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat5>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat5>(destination, new GenericFieldFloat5());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat5>(source);
				entityManager.SetComponentData<GenericFieldFloat5>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat6>(source))
			{ 
				// GenericFieldFloat6 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat6>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat6>(destination, new GenericFieldFloat6());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat6>(source);
				entityManager.SetComponentData<GenericFieldFloat6>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat7>(source))
			{ 
				// GenericFieldFloat7 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat7>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat7>(destination, new GenericFieldFloat7());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat7>(source);
				entityManager.SetComponentData<GenericFieldFloat7>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat8>(source))
			{ 
				// GenericFieldFloat8 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat8>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat8>(destination, new GenericFieldFloat8());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat8>(source);
				entityManager.SetComponentData<GenericFieldFloat8>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldFloat9>(source))
			{ 
				// GenericFieldFloat9 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldFloat9>(destination)) {
					entityManager.AddComponentData<GenericFieldFloat9>(destination, new GenericFieldFloat9());
				}
				var data = entityManager.GetComponentData<GenericFieldFloat9>(source);
				entityManager.SetComponentData<GenericFieldFloat9>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldVector0>(source))
			{ 
				// GenericFieldVector0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldVector0>(destination)) {
					entityManager.AddComponentData<GenericFieldVector0>(destination, new GenericFieldVector0());
				}
				var data = entityManager.GetComponentData<GenericFieldVector0>(source);
				entityManager.SetComponentData<GenericFieldVector0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldVector1>(source))
			{ 
				// GenericFieldVector1 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldVector1>(destination)) {
					entityManager.AddComponentData<GenericFieldVector1>(destination, new GenericFieldVector1());
				}
				var data = entityManager.GetComponentData<GenericFieldVector1>(source);
				entityManager.SetComponentData<GenericFieldVector1>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldVector2>(source))
			{ 
				// GenericFieldVector2 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldVector2>(destination)) {
					entityManager.AddComponentData<GenericFieldVector2>(destination, new GenericFieldVector2());
				}
				var data = entityManager.GetComponentData<GenericFieldVector2>(source);
				entityManager.SetComponentData<GenericFieldVector2>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldVector3>(source))
			{ 
				// GenericFieldVector3 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldVector3>(destination)) {
					entityManager.AddComponentData<GenericFieldVector3>(destination, new GenericFieldVector3());
				}
				var data = entityManager.GetComponentData<GenericFieldVector3>(source);
				entityManager.SetComponentData<GenericFieldVector3>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldString0>(source))
			{ 
				// GenericFieldString0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldString0>(destination)) {
					entityManager.AddComponentData<GenericFieldString0>(destination, new GenericFieldString0());
				}
				var data = entityManager.GetComponentData<GenericFieldString0>(source);
				entityManager.SetComponentData<GenericFieldString0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldString1>(source))
			{ 
				// GenericFieldString1 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldString1>(destination)) {
					entityManager.AddComponentData<GenericFieldString1>(destination, new GenericFieldString1());
				}
				var data = entityManager.GetComponentData<GenericFieldString1>(source);
				entityManager.SetComponentData<GenericFieldString1>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldString2>(source))
			{ 
				// GenericFieldString2 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldString2>(destination)) {
					entityManager.AddComponentData<GenericFieldString2>(destination, new GenericFieldString2());
				}
				var data = entityManager.GetComponentData<GenericFieldString2>(source);
				entityManager.SetComponentData<GenericFieldString2>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldString3>(source))
			{ 
				// GenericFieldString3 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldString3>(destination)) {
					entityManager.AddComponentData<GenericFieldString3>(destination, new GenericFieldString3());
				}
				var data = entityManager.GetComponentData<GenericFieldString3>(source);
				entityManager.SetComponentData<GenericFieldString3>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldString4>(source))
			{ 
				// GenericFieldString4 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldString4>(destination)) {
					entityManager.AddComponentData<GenericFieldString4>(destination, new GenericFieldString4());
				}
				var data = entityManager.GetComponentData<GenericFieldString4>(source);
				entityManager.SetComponentData<GenericFieldString4>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldQuaternion0>(source))
			{ 
				// GenericFieldQuaternion0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldQuaternion0>(destination)) {
					entityManager.AddComponentData<GenericFieldQuaternion0>(destination, new GenericFieldQuaternion0());
				}
				var data = entityManager.GetComponentData<GenericFieldQuaternion0>(source);
				entityManager.SetComponentData<GenericFieldQuaternion0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity0>(source))
			{ 
				// GenericFieldEntity0 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity0>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity0>(destination, new GenericFieldEntity0());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity0>(source);
				entityManager.SetComponentData<GenericFieldEntity0>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity1>(source))
			{ 
				// GenericFieldEntity1 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity1>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity1>(destination, new GenericFieldEntity1());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity1>(source);
				entityManager.SetComponentData<GenericFieldEntity1>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity2>(source))
			{ 
				// GenericFieldEntity2 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity2>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity2>(destination, new GenericFieldEntity2());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity2>(source);
				entityManager.SetComponentData<GenericFieldEntity2>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity3>(source))
			{ 
				// GenericFieldEntity3 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity3>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity3>(destination, new GenericFieldEntity3());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity3>(source);
				entityManager.SetComponentData<GenericFieldEntity3>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity4>(source))
			{ 
				// GenericFieldEntity4 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity4>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity4>(destination, new GenericFieldEntity4());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity4>(source);
				entityManager.SetComponentData<GenericFieldEntity4>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity5>(source))
			{ 
				// GenericFieldEntity5 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity5>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity5>(destination, new GenericFieldEntity5());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity5>(source);
				entityManager.SetComponentData<GenericFieldEntity5>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity6>(source))
			{ 
				// GenericFieldEntity6 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity6>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity6>(destination, new GenericFieldEntity6());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity6>(source);
				entityManager.SetComponentData<GenericFieldEntity6>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity7>(source))
			{ 
				// GenericFieldEntity7 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity7>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity7>(destination, new GenericFieldEntity7());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity7>(source);
				entityManager.SetComponentData<GenericFieldEntity7>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity8>(source))
			{ 
				// GenericFieldEntity8 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity8>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity8>(destination, new GenericFieldEntity8());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity8>(source);
				entityManager.SetComponentData<GenericFieldEntity8>(destination, data);
			}

			if (entityManager.HasComponent<GenericFieldEntity9>(source))
			{ 
				// GenericFieldEntity9 has fields, will copy it.			
				if (!entityManager.HasComponent<GenericFieldEntity9>(destination)) {
					entityManager.AddComponentData<GenericFieldEntity9>(destination, new GenericFieldEntity9());
				}
				var data = entityManager.GetComponentData<GenericFieldEntity9>(source);
				entityManager.SetComponentData<GenericFieldEntity9>(destination, data);
			}
			// Interpolation components
			if (entityManager.HasComponent<InterpolationComponent_Translation_value>(source) &&
				!entityManager.HasComponent<InterpolationComponent_Translation_value>(destination)) {
				entityManager.AddComponentData(destination, 
					entityManager.GetComponentData<InterpolationComponent_Translation_value>(source));
			}
			// Sample buffers
			if (entityManager.HasComponent<Sample_Translation_value>(source) &&
				!entityManager.HasComponent<Sample_Translation_value>(destination)) {
				entityManager.AddBuffer<Sample_Translation_value>(destination);
				entityManager.GetBuffer<Sample_Translation_value>(destination).CopyFrom(
					entityManager.GetBuffer<Sample_Translation_value>(source));
			}
			// Interpolation components
			if (entityManager.HasComponent<InterpolationComponent_Rotation_value>(source) &&
				!entityManager.HasComponent<InterpolationComponent_Rotation_value>(destination)) {
				entityManager.AddComponentData(destination, 
					entityManager.GetComponentData<InterpolationComponent_Rotation_value>(source));
			}
			// Sample buffers
			if (entityManager.HasComponent<Sample_Rotation_value>(source) &&
				!entityManager.HasComponent<Sample_Rotation_value>(destination)) {
				entityManager.AddBuffer<Sample_Rotation_value>(destination);
				entityManager.GetBuffer<Sample_Rotation_value>(destination).CopyFrom(
					entityManager.GetBuffer<Sample_Rotation_value>(source));
			}
			// Command buffers
			if (entityManager.HasComponent<AuthorityTransfer>(source) &&
				!entityManager.HasComponent<AuthorityTransfer>(destination)) {
				entityManager.AddBuffer<AuthorityTransfer>(destination);
				entityManager.GetBuffer<AuthorityTransfer>(destination).CopyFrom(
					entityManager.GetBuffer<AuthorityTransfer>(source));
			}			
			if (entityManager.HasComponent<AuthorityTransferRequest>(source) &&
				!entityManager.HasComponent<AuthorityTransferRequest>(destination)) {
				entityManager.AddBuffer<AuthorityTransferRequest>(destination);
				entityManager.GetBuffer<AuthorityTransferRequest>(destination).CopyFrom(
					entityManager.GetBuffer<AuthorityTransferRequest>(source));
			}
			if (entityManager.HasComponent<GenericCommand>(source) &&
				!entityManager.HasComponent<GenericCommand>(destination)) {
				entityManager.AddBuffer<GenericCommand>(destination);
				entityManager.GetBuffer<GenericCommand>(destination).CopyFrom(
					entityManager.GetBuffer<GenericCommand>(source));
			}			
			if (entityManager.HasComponent<GenericCommandRequest>(source) &&
				!entityManager.HasComponent<GenericCommandRequest>(destination)) {
				entityManager.AddBuffer<GenericCommandRequest>(destination);
				entityManager.GetBuffer<GenericCommandRequest>(destination).CopyFrom(
					entityManager.GetBuffer<GenericCommandRequest>(source));
			}
			if (entityManager.HasComponent<Player_Controller_Blaj>(source) &&
				!entityManager.HasComponent<Player_Controller_Blaj>(destination)) {
				entityManager.AddBuffer<Player_Controller_Blaj>(destination);
				entityManager.GetBuffer<Player_Controller_Blaj>(destination).CopyFrom(
					entityManager.GetBuffer<Player_Controller_Blaj>(source));
			}			
			if (entityManager.HasComponent<Player_Controller_BlajRequest>(source) &&
				!entityManager.HasComponent<Player_Controller_BlajRequest>(destination)) {
				entityManager.AddBuffer<Player_Controller_BlajRequest>(destination);
				entityManager.GetBuffer<Player_Controller_BlajRequest>(destination).CopyFrom(
					entityManager.GetBuffer<Player_Controller_BlajRequest>(source));
			}

		}
	}
}


// ------------------ end of EntityReplacer.cs -----------------
#endregion
