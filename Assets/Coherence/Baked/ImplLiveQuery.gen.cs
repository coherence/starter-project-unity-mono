


#region ImplLiveQuery
// -----------------------------------
//  ImplLiveQuery.cs
// -----------------------------------
			
namespace Coherence.Toolkit
{
	using Coherence.Toolkit;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Collections;
	using Unity.Transforms;
	using Unity.Entities;
	using UnityEngine;
	using System;
	using global::Coherence.Generated.FirstProject;

	public class CoherenceLiveQueryImpl : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			CoherenceLiveQuery.CreateLiveQuery = CreateLiveQuery;
		}

		private static void CreateLiveQuery(EntityManager entityManager, float radius) {
			Entity liveQuery = entityManager.CreateEntity(
				typeof(Simulated),
				typeof(WorldPositionQuery),
				typeof(SessionBased));

			entityManager.SetComponentData(liveQuery, new WorldPositionQuery()
			{
				radius = radius
			});
		}
	}
}

// ------------------ end of ImplLiveQuery.cs -----------------
#endregion
