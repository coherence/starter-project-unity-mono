


#region ImplLiveQuery
// -----------------------------------
//  ImplLiveQuery.cs
// -----------------------------------
			
namespace Coherence.Toolkit
{
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using UnityEngine;
	using Unity.Mathematics;
	using global::Coherence.Generated;

	public class CoherenceLiveQueryImpl : MonoBehaviour
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			CoherenceLiveQuery.CreateLiveQuery = CreateLiveQuery;
			CoherenceLiveQuery.UpdateLiveQuery = UpdateLiveQuery;
		}

		private static Entity liveQuery = Entity.Null;

		private static void CreateLiveQuery(EntityManager entityManager, float radius, float3 pos) {
			liveQuery = entityManager.CreateEntity(
				typeof(Simulated),
				typeof(WorldPositionQuery));

			entityManager.SetComponentData(liveQuery, new WorldPositionQuery()
			{
				radius = radius,
				position = pos
			});
		}

		private static void UpdateLiveQuery(EntityManager entityManager, float radius, float3 pos)
		{
			if(liveQuery == Entity.Null) { return; }

			entityManager.SetComponentData(liveQuery, new WorldPositionQuery()
			{
				radius = radius,
				position = pos
			});
		}
	}
}

// ------------------ end of ImplLiveQuery.cs -----------------
#endregion
