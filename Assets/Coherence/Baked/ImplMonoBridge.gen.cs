


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
			CoherenceMonoBridge.GetPrefabName = GetPrefabName;
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

		static FixedString64 GetPrefabName(EntityManager entityManager, Entity entity) {
			return entityManager.GetComponentData<GenericPrefabReference>(entity).prefab;
		}
	}
}

// ------------------ end of ImplMonoBridge.cs -----------------
#endregion
