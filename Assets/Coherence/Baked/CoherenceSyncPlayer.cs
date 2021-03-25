


#region CoherenceSyncPlayer
// -----------------------------------
//  CoherenceSyncPlayer.cs
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
	using static Coherence.Toolkit.CoherenceSync;
	using global::Coherence.Generated.Internal;

	public class CoherenceSyncPlayer : CoherenceSyncBaked
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
			var entity = coherenceSync.LinkedEntity;

			if(coherenceSync.HasArchetype) {
				entityManager.AddComponent<LastObservedLod>(entity);
			}

			if (!coherenceSync.isSimulated) return;

			entityManager.AddComponent<Translation>(entity);
			entityManager.AddComponent<Rotation>(entity);
			entityManager.AddComponent<GenericScale>(entity);
			

			if (coherenceSync.HasArchetype)
			{
				int archetypeIndex = Archetype.IndexForName[coherenceSync.Archetype.ArchetypeName];
				entityManager.AddComponentData(entity, new ArchetypeComponent { index = archetypeIndex });
			}

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

			switch (coherenceSync.simulationType)
			{
				case SimulationType.SimulationServerWithClientInput:
					entityManager.AddComponent<InputClient>(entity);
					entityManager.AddComponent<LocalInputClient>(entity);
					break;
				default:
					break;
			}

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

		static float3 Vector3ToFloat(object o)
		{
			Vector3 v = (Vector3)o;
			return new float3(v.x, v.y, v.z);
		}

		private void SyncEcsBaked()
		{
			var entity = coherenceSync.LinkedEntity;

			if (coherenceSync.isSimulated)
			{
				
					  
						entityManager.SetComponentData(entity, new Translation() {
								
								Value = transform.position,
							});
					  
					
					  
						entityManager.SetComponentData(entity, new Rotation() {
								
								Value = transform.rotation,
							});
					  
					
					  
						entityManager.SetComponentData(entity, new GenericScale() {
								
								Value = transform.localScale,
							});
					  
					
			}
			else
			{
				
					  
						if (entityManager.HasComponent<Translation>(entity)) {
							var data = entityManager.GetComponentData<Translation>(entity);
						
							
							
							transform.position = data.Value; // float3
							
						
						}
					  
					
					  
						if (entityManager.HasComponent<Rotation>(entity)) {
							var data = entityManager.GetComponentData<Rotation>(entity);
						
							
							
							transform.rotation = data.Value; // quaternion
							
						
						}
					  
					
					  
						if (entityManager.HasComponent<GenericScale>(entity)) {
							var data = entityManager.GetComponentData<GenericScale>(entity);
						
							
							
							transform.localScale = data.Value; // float3
							
						
						}
					  
					

				if(coherenceSync.HasArchetype) {
					int level = entityManager.GetComponentData<LastObservedLod>(entity).Level;
					coherenceSync.Archetype.SetObservedLodLevel(level);
				}
			}
		}
	}
}

// ------------------ end of CoherenceSyncPlayer.cs -----------------
#endregion
