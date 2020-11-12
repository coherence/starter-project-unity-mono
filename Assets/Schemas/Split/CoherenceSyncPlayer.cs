


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
	using Coherence.MonoBridge;

	public class CoherenceSyncPlayer : MonoBehaviour
	{
		private CoherenceSync coherenceSync;
		private EntityManager entityManager;
		private bool componentsInitialized = false;

		// Cached references to MonoBehaviours on this GameObject
		
		private Translation _Translation;
		
		
		// skip Rotation
		
		
		// skip LocalUser
		
		
		// skip WorldPositionQuery
		
		
		// skip SessionBased
		
		
		// skip GenericPrefabReference
		
		
		// skip GenericScale
		
		
		// skip GenericFieldInt0
		
		
		// skip GenericFieldInt1
		
		
		// skip GenericFieldInt2
		
		
		// skip GenericFieldInt3
		
		
		// skip GenericFieldInt4
		
		
		// skip GenericFieldInt5
		
		
		// skip GenericFieldInt6
		
		
		// skip GenericFieldInt7
		
		
		// skip GenericFieldInt8
		
		
		// skip GenericFieldInt9
		
		
		// skip GenericFieldFloat0
		
		
		// skip GenericFieldFloat1
		
		
		// skip GenericFieldFloat2
		
		
		// skip GenericFieldFloat3
		
		
		// skip GenericFieldFloat4
		
		
		// skip GenericFieldFloat5
		
		
		// skip GenericFieldFloat6
		
		
		// skip GenericFieldFloat7
		
		
		// skip GenericFieldFloat8
		
		
		// skip GenericFieldFloat9
		
		
		// skip GenericFieldVector0
		
		
		// skip GenericFieldVector1
		
		
		// skip GenericFieldVector2
		
		
		// skip GenericFieldVector3
		
		
		// skip GenericFieldString0
		
		
		// skip GenericFieldString1
		
		
		// skip GenericFieldString2
		
		
		// skip GenericFieldString4
		
		private ColorizeBehaviour _ColorizeBehaviour;
		

		protected void Awake()
		{
			entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			coherenceSync = GetComponent<CoherenceSync>();
			coherenceSync.usingReflection = false;

			
			_Translation = GetComponent<Translation>();
			
			
			// skip Rotation
			
			
			// skip LocalUser
			
			
			// skip WorldPositionQuery
			
			
			// skip SessionBased
			
			
			// skip GenericPrefabReference
			
			
			// skip GenericScale
			
			
			// skip GenericFieldInt0
			
			
			// skip GenericFieldInt1
			
			
			// skip GenericFieldInt2
			
			
			// skip GenericFieldInt3
			
			
			// skip GenericFieldInt4
			
			
			// skip GenericFieldInt5
			
			
			// skip GenericFieldInt6
			
			
			// skip GenericFieldInt7
			
			
			// skip GenericFieldInt8
			
			
			// skip GenericFieldInt9
			
			
			// skip GenericFieldFloat0
			
			
			// skip GenericFieldFloat1
			
			
			// skip GenericFieldFloat2
			
			
			// skip GenericFieldFloat3
			
			
			// skip GenericFieldFloat4
			
			
			// skip GenericFieldFloat5
			
			
			// skip GenericFieldFloat6
			
			
			// skip GenericFieldFloat7
			
			
			// skip GenericFieldFloat8
			
			
			// skip GenericFieldFloat9
			
			
			// skip GenericFieldVector0
			
			
			// skip GenericFieldVector1
			
			
			// skip GenericFieldVector2
			
			
			// skip GenericFieldVector3
			
			
			// skip GenericFieldString0
			
			
			// skip GenericFieldString1
			
			
			// skip GenericFieldString2
			
			
			// skip GenericFieldString4
			
			_ColorizeBehaviour = GetComponent<ColorizeBehaviour>();
			
		}

		private void InitializeComponents()
		{
			if (!coherenceSync.isSimulated) return;

			var entity = coherenceSync.LinkedEntity;

			
			
			entityManager.AddComponent<Translation>(entity);
			
			
			
			// skip Rotation
			
			
			
			// skip LocalUser
			
			
			
			// skip WorldPositionQuery
			
			
			
			// skip SessionBased
			
			
			
			// skip GenericPrefabReference
			
			
			
			// skip GenericScale
			
			
			
			// skip GenericFieldInt0
			
			
			
			// skip GenericFieldInt1
			
			
			
			// skip GenericFieldInt2
			
			
			
			// skip GenericFieldInt3
			
			
			
			// skip GenericFieldInt4
			
			
			
			// skip GenericFieldInt5
			
			
			
			// skip GenericFieldInt6
			
			
			
			// skip GenericFieldInt7
			
			
			
			// skip GenericFieldInt8
			
			
			
			// skip GenericFieldInt9
			
			
			
			// skip GenericFieldFloat0
			
			
			
			// skip GenericFieldFloat1
			
			
			
			// skip GenericFieldFloat2
			
			
			
			// skip GenericFieldFloat3
			
			
			
			// skip GenericFieldFloat4
			
			
			
			// skip GenericFieldFloat5
			
			
			
			// skip GenericFieldFloat6
			
			
			
			// skip GenericFieldFloat7
			
			
			
			// skip GenericFieldFloat8
			
			
			
			// skip GenericFieldFloat9
			
			
			
			// skip GenericFieldVector0
			
			
			
			// skip GenericFieldVector1
			
			
			
			// skip GenericFieldVector2
			
			
			
			// skip GenericFieldVector3
			
			
			
			// skip GenericFieldString0
			
			
			
			// skip GenericFieldString1
			
			
			
			// skip GenericFieldString2
			
			
			
			// skip GenericFieldString4
			
			
			
			entityManager.AddComponent<ColorizeBehaviour>(entity);
			
			

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

		private void SyncEcsBaked()
		{
			var entity = coherenceSync.LinkedEntity;

			if (coherenceSync.isSimulated)
			{
				
				
				entityManager.SetComponentData(entity, new Translation()
				{
					
					Value = _Translation.Value,
					
				});
				
				
				
				// skip Rotation
				
				
				
				// skip LocalUser
				
				
				
				// skip WorldPositionQuery
				
				
				
				// skip SessionBased
				
				
				
				// skip GenericPrefabReference
				
				
				
				// skip GenericScale
				
				
				
				// skip GenericFieldInt0
				
				
				
				// skip GenericFieldInt1
				
				
				
				// skip GenericFieldInt2
				
				
				
				// skip GenericFieldInt3
				
				
				
				// skip GenericFieldInt4
				
				
				
				// skip GenericFieldInt5
				
				
				
				// skip GenericFieldInt6
				
				
				
				// skip GenericFieldInt7
				
				
				
				// skip GenericFieldInt8
				
				
				
				// skip GenericFieldInt9
				
				
				
				// skip GenericFieldFloat0
				
				
				
				// skip GenericFieldFloat1
				
				
				
				// skip GenericFieldFloat2
				
				
				
				// skip GenericFieldFloat3
				
				
				
				// skip GenericFieldFloat4
				
				
				
				// skip GenericFieldFloat5
				
				
				
				// skip GenericFieldFloat6
				
				
				
				// skip GenericFieldFloat7
				
				
				
				// skip GenericFieldFloat8
				
				
				
				// skip GenericFieldFloat9
				
				
				
				// skip GenericFieldVector0
				
				
				
				// skip GenericFieldVector1
				
				
				
				// skip GenericFieldVector2
				
				
				
				// skip GenericFieldVector3
				
				
				
				// skip GenericFieldString0
				
				
				
				// skip GenericFieldString1
				
				
				
				// skip GenericFieldString2
				
				
				
				// skip GenericFieldString4
				
				
				
				entityManager.SetComponentData(entity, new ColorizeBehaviour()
				{
					
					iii = _ColorizeBehaviour.iii,
					
					fff = _ColorizeBehaviour.fff,
					
					bbb = _ColorizeBehaviour.bbb,
					
					target = _ColorizeBehaviour.target,
					
					whatever_works = _ColorizeBehaviour.whatever_works,
					
					name2 = _ColorizeBehaviour.name2,
					
				});
				
				
			}
			else
			{
				
				

				  
				  _Translation.Value = entityManager.GetComponentData<Translation>(entity).Value; 
				  

				
				
				
				// skip Rotation
				
				
				
				// skip LocalUser
				
				
				
				// skip WorldPositionQuery
				
				
				
				// skip SessionBased
				
				
				
				// skip GenericPrefabReference
				
				
				
				// skip GenericScale
				
				
				
				// skip GenericFieldInt0
				
				
				
				// skip GenericFieldInt1
				
				
				
				// skip GenericFieldInt2
				
				
				
				// skip GenericFieldInt3
				
				
				
				// skip GenericFieldInt4
				
				
				
				// skip GenericFieldInt5
				
				
				
				// skip GenericFieldInt6
				
				
				
				// skip GenericFieldInt7
				
				
				
				// skip GenericFieldInt8
				
				
				
				// skip GenericFieldInt9
				
				
				
				// skip GenericFieldFloat0
				
				
				
				// skip GenericFieldFloat1
				
				
				
				// skip GenericFieldFloat2
				
				
				
				// skip GenericFieldFloat3
				
				
				
				// skip GenericFieldFloat4
				
				
				
				// skip GenericFieldFloat5
				
				
				
				// skip GenericFieldFloat6
				
				
				
				// skip GenericFieldFloat7
				
				
				
				// skip GenericFieldFloat8
				
				
				
				// skip GenericFieldFloat9
				
				
				
				// skip GenericFieldVector0
				
				
				
				// skip GenericFieldVector1
				
				
				
				// skip GenericFieldVector2
				
				
				
				// skip GenericFieldVector3
				
				
				
				// skip GenericFieldString0
				
				
				
				// skip GenericFieldString1
				
				
				
				// skip GenericFieldString2
				
				
				
				// skip GenericFieldString4
				
				
				

				  
				  _ColorizeBehaviour.iii = entityManager.GetComponentData<ColorizeBehaviour>(entity).iii; 
				  
				  _ColorizeBehaviour.fff = entityManager.GetComponentData<ColorizeBehaviour>(entity).fff; 
				  
				  _ColorizeBehaviour.bbb = entityManager.GetComponentData<ColorizeBehaviour>(entity).bbb; 
				  
				  _ColorizeBehaviour.target = entityManager.GetComponentData<ColorizeBehaviour>(entity).target; 
				  
				  _ColorizeBehaviour.whatever_works = entityManager.GetComponentData<ColorizeBehaviour>(entity).whatever_works; 
				  
				  _ColorizeBehaviour.name2 = entityManager.GetComponentData<ColorizeBehaviour>(entity).name2; 
				  

				
				
			}
		}
	}
}

// ------------------ end of CoherenceSyncPlayer.cs -----------------
#endregion
