


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
		
		private Rotation _Rotation;
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		private ColorizeBehaviour _ColorizeBehaviour;
		

		protected void Awake()
		{
			entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			coherenceSync = GetComponent<CoherenceSync>();
			coherenceSync.usingReflection = false;

			
			_Translation = GetComponent<Translation>();
			
			_Rotation = GetComponent<Rotation>();
			
			
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
			
			
			
			entityManager.AddComponent<Rotation>(entity);
			
			
			
			// Won't add LocalUser -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add WorldPositionQuery -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add SessionBased -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericPrefabReference -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericScale -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt0 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt1 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt2 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt3 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt4 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt5 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt6 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt7 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt8 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldInt9 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat0 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat1 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat2 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat3 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat4 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat5 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat6 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat7 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat8 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldFloat9 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldVector0 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldVector1 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldVector2 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldVector3 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldString0 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldString1 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldString2 -- it's not part of this Prefab/GameObject
			
			
			
			// Won't add GenericFieldString4 -- it's not part of this Prefab/GameObject
			
			
			
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
					
					Value = _Translation.Value
					
				});
				
				
				
				entityManager.SetComponentData(entity, new Rotation()
				{
					
					Value = _Rotation.Value
					
				});
				
				
				
				// skipping LocalUser
				
				
				
				// skipping WorldPositionQuery
				
				
				
				// skipping SessionBased
				
				
				
				// skipping GenericPrefabReference
				
				
				
				// skipping GenericScale
				
				
				
				// skipping GenericFieldInt0
				
				
				
				// skipping GenericFieldInt1
				
				
				
				// skipping GenericFieldInt2
				
				
				
				// skipping GenericFieldInt3
				
				
				
				// skipping GenericFieldInt4
				
				
				
				// skipping GenericFieldInt5
				
				
				
				// skipping GenericFieldInt6
				
				
				
				// skipping GenericFieldInt7
				
				
				
				// skipping GenericFieldInt8
				
				
				
				// skipping GenericFieldInt9
				
				
				
				// skipping GenericFieldFloat0
				
				
				
				// skipping GenericFieldFloat1
				
				
				
				// skipping GenericFieldFloat2
				
				
				
				// skipping GenericFieldFloat3
				
				
				
				// skipping GenericFieldFloat4
				
				
				
				// skipping GenericFieldFloat5
				
				
				
				// skipping GenericFieldFloat6
				
				
				
				// skipping GenericFieldFloat7
				
				
				
				// skipping GenericFieldFloat8
				
				
				
				// skipping GenericFieldFloat9
				
				
				
				// skipping GenericFieldVector0
				
				
				
				// skipping GenericFieldVector1
				
				
				
				// skipping GenericFieldVector2
				
				
				
				// skipping GenericFieldVector3
				
				
				
				// skipping GenericFieldString0
				
				
				
				// skipping GenericFieldString1
				
				
				
				// skipping GenericFieldString2
				
				
				
				// skipping GenericFieldString4
				
				
				
				entityManager.SetComponentData(entity, new ColorizeBehaviour()
				{
					
					bleh = _ColorizeBehaviour.bleh
					
				});
				
				
			}
			else
			{
				
				

				  
				  _Translation.Value = entityManager.GetComponentData<Translation>(entity).Value; 
				  

				
				
				

				  
				  _Rotation.Value = entityManager.GetComponentData<Rotation>(entity).Value; 
				  

				
				
				
				// skipping LocalUser
				
				
				
				// skipping WorldPositionQuery
				
				
				
				// skipping SessionBased
				
				
				
				// skipping GenericPrefabReference
				
				
				
				// skipping GenericScale
				
				
				
				// skipping GenericFieldInt0
				
				
				
				// skipping GenericFieldInt1
				
				
				
				// skipping GenericFieldInt2
				
				
				
				// skipping GenericFieldInt3
				
				
				
				// skipping GenericFieldInt4
				
				
				
				// skipping GenericFieldInt5
				
				
				
				// skipping GenericFieldInt6
				
				
				
				// skipping GenericFieldInt7
				
				
				
				// skipping GenericFieldInt8
				
				
				
				// skipping GenericFieldInt9
				
				
				
				// skipping GenericFieldFloat0
				
				
				
				// skipping GenericFieldFloat1
				
				
				
				// skipping GenericFieldFloat2
				
				
				
				// skipping GenericFieldFloat3
				
				
				
				// skipping GenericFieldFloat4
				
				
				
				// skipping GenericFieldFloat5
				
				
				
				// skipping GenericFieldFloat6
				
				
				
				// skipping GenericFieldFloat7
				
				
				
				// skipping GenericFieldFloat8
				
				
				
				// skipping GenericFieldFloat9
				
				
				
				// skipping GenericFieldVector0
				
				
				
				// skipping GenericFieldVector1
				
				
				
				// skipping GenericFieldVector2
				
				
				
				// skipping GenericFieldVector3
				
				
				
				// skipping GenericFieldString0
				
				
				
				// skipping GenericFieldString1
				
				
				
				// skipping GenericFieldString2
				
				
				
				// skipping GenericFieldString4
				
				
				

				  
				  _ColorizeBehaviour.bleh = entityManager.GetComponentData<ColorizeBehaviour>(entity).bleh; 
				  

				
				
			}
		}
	}
}

// ------------------ end of CoherenceSyncPlayer.cs -----------------
#endregion
