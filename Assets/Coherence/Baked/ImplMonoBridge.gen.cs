


#region ImplMonoBridge
// -----------------------------------
//  ImplMonoBridge.cs
// -----------------------------------
			
namespace Coherence.MonoBridge
{
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Transforms;
	using UnityEngine;
	using global::Coherence.Generated.FirstProject;

	public class CoherenceMonoBridgeImpl
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			CoherenceMonoBridge.CreateEntityQueryRemote = CreateEntityQueryRemote;
			CoherenceMonoBridge.CreateEntityQueryLocal = CreateEntityQueryLocal;
			CoherenceMonoBridge.GetPrefabName = GetPrefabName;
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
