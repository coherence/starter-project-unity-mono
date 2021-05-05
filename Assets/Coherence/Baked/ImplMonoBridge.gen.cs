


#region ImplMonoBridge
// -----------------------------------
//  ImplMonoBridge.cs
// -----------------------------------
			
namespace Coherence.Toolkit
{
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Transforms;
	using UnityEngine;
	using global::Coherence.Generated;

	public class CoherenceMonoBridgeImpl
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			CoherenceMonoBridge.CreateEntityQueryRemote = CreateEntityQueryRemote;
			CoherenceMonoBridge.CreateEntityQueryLocal = CreateEntityQueryLocal;
			CoherenceMonoBridge.GetSpawnInfo = GetSpawnInfo;
			CoherenceMonoBridge.GetPersistenceUuid = GetPersistenceUuid;
		}

		private static string GetPersistenceUuid(EntityManager entityManager, Entity entity)
		{
			if (entityManager.HasComponent<Persistence>(entity))
			{
				var ret = entityManager.GetComponentData<Persistence>(entity);
				return ret.uuid.ToString();
			}

			return null;
		}

		private static EntityQuery CreateEntityQueryRemote(EntityManager entityManager)
		{
			return entityManager.CreateEntityQuery(
					typeof(GenericPrefabReference),
					typeof(Translation),
					ComponentType.Exclude<Simulated>());
		}

		private static EntityQuery CreateEntityQueryLocal(EntityManager entityManager)
		{
			return entityManager.CreateEntityQuery(
					typeof(GenericPrefabReference),
					typeof(Translation),
					typeof(Simulated));
		}

		static CoherenceMonoBridge.SpawnInfo GetSpawnInfo(EntityManager entityManager, Entity entity) 
		{
			var info = new CoherenceMonoBridge.SpawnInfo();
			info.prefabName = entityManager.GetComponentData<GenericPrefabReference>(entity).prefab;
			info.position = entityManager.GetComponentData<Translation>(entity).Value;

			if (entityManager.HasComponent<Rotation>(entity))
			{
				info.rotation = entityManager.GetComponentData<Rotation>(entity).Value;
			}
			else 
			{
				info.rotation = Quaternion.identity;
			}

			return info;
		}
	}
}

// ------------------ end of ImplMonoBridge.cs -----------------
#endregion
