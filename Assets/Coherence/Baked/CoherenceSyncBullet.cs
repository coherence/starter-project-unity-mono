


#region CoherenceSyncBullet
// -----------------------------------
//  CoherenceSyncBullet.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using UnityEngine;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	using Coherence.Toolkit;
	using Coherence.Replication.Client.Unity.Ecs;

	public class CoherenceSyncBullet : CoherenceSyncBaked
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

			

			

			coherenceSync.OnSpawnFromNetwork += OnSpawnFromNetwork;
		}

		private void OnSpawnFromNetwork()
		{
			InitializeComponents();
			SyncEcsBaked();
		}

		public override void InitializeComponents()
		{
			if (!coherenceSync.isSimulated) return;

			var entity = coherenceSync.LinkedEntity;

			

			if (coherenceSync.lifetimeType == CoherenceSync.LifetimeType.Persistent)
			{
				entityManager.AddComponentData(entity, new Persistence()
				{
					uuid = coherenceSync.persistenceUUID,
					expiry = coherenceSync.GetPersistenceExpiryString()
				});
			}

			if (coherenceSync.authorityTransferType != CoherenceSync.AuthorityTransferType.NotTransferable)
			{
				entityManager.AddComponent<AuthorityTransfer>(entity);
			}

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

// ------------------ end of CoherenceSyncBullet.cs -----------------
#endregion
