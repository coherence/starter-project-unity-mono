


#region EntityReplacer
// -----------------------------------
//  EntityReplacer.cs
// -----------------------------------
			
namespace Coherence.Generated.FirstProject
{
	using Unity.Entities;
	using Unity.Transforms;
	using UnityEngine;
	using Coherence.Replication.Client.Unity.Ecs;

	public class EntityReplacer {
		public static void Replace(EntityManager entityManager, Entity networkEntity, Entity newEntity)
		{
#if UNITY_EDITOR
			entityManager.SetName(newEntity, $"{networkEntity} (remote)");
#endif

			CopyComponents(entityManager, networkEntity, newEntity);

			var mapper = entityManager.World.GetExistingSystem<SyncSendSystem>().Sender.Mapper;
			if (!mapper.ToCoherenceEntityId(networkEntity, out var entityId))
			{
				Debug.LogError("Networked Entity not found in mapper: " + networkEntity); // Should not happen
			}

			mapper.Remove(entityId);
			mapper.Add(entityId, newEntity);
			entityManager.DestroyEntity(networkEntity);

			Debug.Log(string.Format("Replaced networked Entity {0} with new Entity {1}.", networkEntity, newEntity));
		}

		private static void CopyComponents(EntityManager entityManager, Entity source, Entity destination)
		{
		
            if(entityManager.HasComponent<Translation>(source))
			{
		        // Translation is built in
                var data = entityManager.GetComponentData<Translation>(source);
                entityManager.SetComponentData<Translation>(destination, data);
		
			}
		
            if(entityManager.HasComponent<Rotation>(source))
			{
		        // Rotation is built in
                var data = entityManager.GetComponentData<Rotation>(source);
                entityManager.SetComponentData<Rotation>(destination, data);
		
			}
		
            if(entityManager.HasComponent<LocalUser>(source))
			{
		        // LocalUser has fields, will copy it.			
                if(!entityManager.HasComponent<LocalUser>(destination)) {
                    entityManager.AddComponentData<LocalUser>(destination, new LocalUser());
                }
				var data = entityManager.GetComponentData<LocalUser>(source);
				entityManager.SetComponentData<LocalUser>(destination, data);
		
			}
		
            if(entityManager.HasComponent<WorldPositionQuery>(source))
			{
		        // WorldPositionQuery has fields, will copy it.			
                if(!entityManager.HasComponent<WorldPositionQuery>(destination)) {
                    entityManager.AddComponentData<WorldPositionQuery>(destination, new WorldPositionQuery());
                }
				var data = entityManager.GetComponentData<WorldPositionQuery>(source);
				entityManager.SetComponentData<WorldPositionQuery>(destination, data);
		
			}
		
            if(entityManager.HasComponent<SessionBased>(source))
			{
		        // SessionBased has no fields, will just add it.
		        entityManager.AddComponentData<SessionBased>(destination, new SessionBased());
		
			}
		

        // Command buffers
        

		}
	}
}


// ------------------ end of EntityReplacer.cs -----------------
#endregion
