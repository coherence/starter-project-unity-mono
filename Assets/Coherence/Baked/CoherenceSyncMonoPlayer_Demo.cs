


#region CoherenceSyncMonoPlayer_Demo
// -----------------------------------
//  CoherenceSyncMonoPlayer_Demo.cs
// -----------------------------------
			
namespace Coherence.Generated.FirstProject
{
	using UnityEngine;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	using Coherence.Toolkit;
	using Coherence.Replication.Client.Unity.Ecs;

	public class CoherenceSyncMonoPlayer_Demo : CoherenceSyncBaked
	{
		private CoherenceSync coherenceSync;
		private EntityManager entityManager;
		private bool componentsInitialized = false;

		// Cached references to MonoBehaviours on this GameObject
		

		protected void Awake()
		{
			entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			coherenceSync = GetComponent<CoherenceSync>();
			coherenceSync.usingReflection = false;

			

			
		}

		private void InitializeComponents()
		{
			if (!coherenceSync.isSimulated) return;

			var entity = coherenceSync.LinkedEntity;

			

			entityManager.AddComponent<SessionBased>(entity);
			entityManager.AddComponent<Simulated>(entity);

			componentsInitialized = true;
		}

		void Update()
		{
			if (!coherenceSync.EcsEntityExists())
			{
				return;
			}

			if (!componentsInitialized)
			{
				InitializeComponents();
			}

			

			SyncEcsBaked();
		}

		

		static FixedString64 ObjectToFixedString64(object o)
		{
			return new FixedString64((string)o);
		}

		private void SyncEcsBaked()
		{
			var entity = coherenceSync.LinkedEntity;

			if (coherenceSync.isSimulated)
			{
				
			}
			else
			{
				
			}
		}
	}
}

// ------------------ end of CoherenceSyncMonoPlayer_Demo.cs -----------------
#endregion
