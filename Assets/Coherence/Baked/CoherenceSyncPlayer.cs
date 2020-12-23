


#region CoherenceSyncPlayer
// -----------------------------------
//  CoherenceSyncPlayer.cs
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

			

			
		}

		private void InitializeComponents()
		{
			if (!coherenceSync.isSimulated) return;

			var entity = coherenceSync.LinkedEntity;

			entityManager.AddComponent<Translation>(entity);
			entityManager.AddComponent<Rotation>(entity);
			entityManager.AddComponent<GenericScale>(entity);
			

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
				
					  
						{
							var data = entityManager.GetComponentData<Translation>(entity);
						
							
							
							transform.position = data.Value; // float3
							
						
						}
					  
					
					  
						{
							var data = entityManager.GetComponentData<Rotation>(entity);
						
							
							
							transform.rotation = data.Value; // quaternion
							
						
						}
					  
					
					  
						{
							var data = entityManager.GetComponentData<GenericScale>(entity);
						
							
							
							transform.localScale = data.Value; // float3
							
						
						}
					  
					
			}
		}
	}
}

// ------------------ end of CoherenceSyncPlayer.cs -----------------
#endregion
