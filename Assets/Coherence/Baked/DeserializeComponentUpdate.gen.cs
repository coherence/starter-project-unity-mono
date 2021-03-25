


#region DeserializeComponentUpdate
// -----------------------------------
//  DeserializeComponentUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using Coherence.Brook;
	using Coherence.Log;
	using Unity.Entities;
	using Unity.Transforms;
	using DeltaEcs;
	using global::Coherence.Generated;
	using Coherence.SimulationFrame;
	using Replication.Client.Unity.Ecs;
	using Coherence.Replication.Unity;

	public class DeserializeComponentUpdateGenerated
	{
		private UnityReaders unityReaders;

		public DeserializeComponentUpdateGenerated(UnityMapper mapper)
		{
			unityReaders = new UnityReaders(mapper);
		}

		private void InterpolateTranslation(EntityManager entityManager, Entity entity, AbsoluteSimulationFrame simulationFrame, Translation tempComponentData)
		{
			// Ensure entities with interpolation also have Interpolation components and Sample components
			if (!entityManager.HasComponent<InterpolationComponent_Translation>(entity))
			{
				entityManager.AddComponent<InterpolationComponent_Translation>(entity);
				entityManager.AddComponent<Sample_Translation>(entity);
			}

			// Append buffer for components that use interpolation
			InterpolationSystem_Translation
				.AppendBuffer(entity, tempComponentData, entityManager.World, (ulong) simulationFrame.Frame);
		}

		private void InterpolateRotation(EntityManager entityManager, Entity entity, AbsoluteSimulationFrame simulationFrame, Rotation tempComponentData)
		{
			// Ensure entities with interpolation also have Interpolation components and Sample components
			if (!entityManager.HasComponent<InterpolationComponent_Rotation>(entity))
			{
				entityManager.AddComponent<InterpolationComponent_Rotation>(entity);
				entityManager.AddComponent<Sample_Rotation>(entity);
			}

			// Append buffer for components that use interpolation
			InterpolationSystem_Rotation
				.AppendBuffer(entity, tempComponentData, entityManager.World, (ulong) simulationFrame.Frame);
		}

		private void DeserializeWorldPosition(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<Translation>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'Translation' to entity {entity}");
				entityManager.AddComponent<Translation>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new Translation();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			var tempComponentData = new Translation();
			unityReaders.Read(ref tempComponentData, protocolStream);
			if (justCreated) // Hack
			{
				entityManager.SetComponentData(entity, tempComponentData);
			}

			InterpolateTranslation(entityManager, entity, simulationFrame, tempComponentData);
		}

		private void DeserializeWorldOrientation(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<Rotation>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'Rotation' to entity {entity}");
				entityManager.AddComponent<Rotation>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new Rotation();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			var tempComponentData = new Rotation();
			unityReaders.Read(ref tempComponentData, protocolStream);
			if (justCreated) // Hack
			{
				entityManager.SetComponentData(entity, tempComponentData);
			}

			InterpolateRotation(entityManager, entity, simulationFrame, tempComponentData);
		}

		private void DeserializeLocalUser(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<LocalUser>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'LocalUser' to entity {entity}");
				entityManager.AddComponent<LocalUser>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new LocalUser();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<LocalUser>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeWorldPositionQuery(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<WorldPositionQuery>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'WorldPositionQuery' to entity {entity}");
				entityManager.AddComponent<WorldPositionQuery>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new WorldPositionQuery();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<WorldPositionQuery>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeArchetypeComponent(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<ArchetypeComponent>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'ArchetypeComponent' to entity {entity}");
				entityManager.AddComponent<ArchetypeComponent>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new ArchetypeComponent();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<ArchetypeComponent>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializePersistence(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<Persistence>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'Persistence' to entity {entity}");
				entityManager.AddComponent<Persistence>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new Persistence();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<Persistence>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericPrefabReference(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericPrefabReference>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericPrefabReference' to entity {entity}");
				entityManager.AddComponent<GenericPrefabReference>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericPrefabReference();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericPrefabReference>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericScale(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericScale>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericScale' to entity {entity}");
				entityManager.AddComponent<GenericScale>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericScale();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericScale>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt0' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt1(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt1>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt1' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt1>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt1();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt1>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt2(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt2>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt2' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt2>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt2();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt2>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt3(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt3>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt3' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt3>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt3();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt3>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt4(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt4>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt4' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt4>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt4();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt4>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt5(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt5>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt5' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt5>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt5();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt5>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt6(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt6>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt6' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt6>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt6();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt6>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt7(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt7>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt7' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt7>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt7();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt7>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt8(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt8>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt8' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt8>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt8();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt8>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldInt9(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldInt9>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldInt9' to entity {entity}");
				entityManager.AddComponent<GenericFieldInt9>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldInt9();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldInt9>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat0' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat1(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat1>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat1' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat1>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat1();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat1>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat2(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat2>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat2' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat2>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat2();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat2>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat3(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat3>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat3' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat3>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat3();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat3>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat4(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat4>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat4' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat4>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat4();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat4>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat5(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat5>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat5' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat5>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat5();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat5>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat6(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat6>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat6' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat6>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat6();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat6>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat7(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat7>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat7' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat7>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat7();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat7>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat8(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat8>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat8' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat8>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat8();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat8>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldFloat9(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldFloat9>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldFloat9' to entity {entity}");
				entityManager.AddComponent<GenericFieldFloat9>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldFloat9();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldFloat9>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldVector0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldVector0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldVector0' to entity {entity}");
				entityManager.AddComponent<GenericFieldVector0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldVector0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldVector0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldVector1(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldVector1>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldVector1' to entity {entity}");
				entityManager.AddComponent<GenericFieldVector1>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldVector1();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldVector1>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldVector2(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldVector2>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldVector2' to entity {entity}");
				entityManager.AddComponent<GenericFieldVector2>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldVector2();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldVector2>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldVector3(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldVector3>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldVector3' to entity {entity}");
				entityManager.AddComponent<GenericFieldVector3>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldVector3();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldVector3>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldString0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldString0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldString0' to entity {entity}");
				entityManager.AddComponent<GenericFieldString0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldString0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldString0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldString1(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldString1>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldString1' to entity {entity}");
				entityManager.AddComponent<GenericFieldString1>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldString1();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldString1>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldString2(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldString2>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldString2' to entity {entity}");
				entityManager.AddComponent<GenericFieldString2>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldString2();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldString2>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldString4(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldString4>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldString4' to entity {entity}");
				entityManager.AddComponent<GenericFieldString4>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldString4();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldString4>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldQuaternion0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldQuaternion0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldQuaternion0' to entity {entity}");
				entityManager.AddComponent<GenericFieldQuaternion0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldQuaternion0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldQuaternion0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		public void ReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
			ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, false);
		}

		public void ReadComponentDataUpdateEx(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream, bool justCreated)
		{
			var componentOwnership = Deserializator.ReadComponentOwnership(bitStream); // Read bit from stream...
			componentOwnership = entityManager.HasComponent<Simulated>(entity); // Then overwrite it with entity ownership.
			var inProtocolStream = new Coherence.FieldStream.Deserialize.Streams.InBitStream(bitStream);
			switch (componentType)
			{
			case TypeIds.InternalWorldPosition:
				DeserializeWorldPosition(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalWorldOrientation:
				DeserializeWorldOrientation(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalLocalUser:
				DeserializeLocalUser(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalWorldPositionQuery:
				DeserializeWorldPositionQuery(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalArchetypeComponent:
				DeserializeArchetypeComponent(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalPersistence:
				DeserializePersistence(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericPrefabReference:
				DeserializeGenericPrefabReference(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericScale:
				DeserializeGenericScale(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt0:
				DeserializeGenericFieldInt0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt1:
				DeserializeGenericFieldInt1(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt2:
				DeserializeGenericFieldInt2(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt3:
				DeserializeGenericFieldInt3(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt4:
				DeserializeGenericFieldInt4(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt5:
				DeserializeGenericFieldInt5(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt6:
				DeserializeGenericFieldInt6(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt7:
				DeserializeGenericFieldInt7(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt8:
				DeserializeGenericFieldInt8(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldInt9:
				DeserializeGenericFieldInt9(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat0:
				DeserializeGenericFieldFloat0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat1:
				DeserializeGenericFieldFloat1(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat2:
				DeserializeGenericFieldFloat2(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat3:
				DeserializeGenericFieldFloat3(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat4:
				DeserializeGenericFieldFloat4(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat5:
				DeserializeGenericFieldFloat5(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat6:
				DeserializeGenericFieldFloat6(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat7:
				DeserializeGenericFieldFloat7(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat8:
				DeserializeGenericFieldFloat8(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldFloat9:
				DeserializeGenericFieldFloat9(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldVector0:
				DeserializeGenericFieldVector0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldVector1:
				DeserializeGenericFieldVector1(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldVector2:
				DeserializeGenericFieldVector2(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldVector3:
				DeserializeGenericFieldVector3(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldString0:
				DeserializeGenericFieldString0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldString1:
				DeserializeGenericFieldString1(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldString2:
				DeserializeGenericFieldString2(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldString4:
				DeserializeGenericFieldString4(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldQuaternion0:
				DeserializeGenericFieldQuaternion0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			default:
				Log.Warning("couldn't find component", "componentType", componentType);
				break;
			}
		}

		public void CreateIfNeededAndReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
#region Commands

			{
				var hasBuffer = entityManager.HasComponent<AuthorityTransfer>(entity);
				if (!hasBuffer)
				{
					entityManager.AddBuffer<AuthorityTransfer>(entity);
				}

				var hasRequestBuffer = entityManager.HasComponent<AuthorityTransferRequest>(entity);
				if (!hasRequestBuffer)
				{
					entityManager.AddBuffer<AuthorityTransferRequest>(entity);
				}
			}

			{
				var hasBuffer = entityManager.HasComponent<GenericCommand>(entity);
				if (!hasBuffer)
				{
					entityManager.AddBuffer<GenericCommand>(entity);
				}

				var hasRequestBuffer = entityManager.HasComponent<GenericCommandRequest>(entity);
				if (!hasRequestBuffer)
				{
					entityManager.AddBuffer<GenericCommandRequest>(entity);
				}
			}

#endregion

			switch (componentType)
			{
				case TypeIds.InternalWorldPosition:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<Translation>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<WorldPosition_Sync>(entity) && entityManager.GetComponentData<WorldPosition_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new Translation());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalWorldOrientation:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<Rotation>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<WorldOrientation_Sync>(entity) && entityManager.GetComponentData<WorldOrientation_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new Rotation());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalLocalUser:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<LocalUser>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<LocalUser_Sync>(entity) && entityManager.GetComponentData<LocalUser_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new LocalUser());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalWorldPositionQuery:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<WorldPositionQuery>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<WorldPositionQuery_Sync>(entity) && entityManager.GetComponentData<WorldPositionQuery_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new WorldPositionQuery());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalArchetypeComponent:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<ArchetypeComponent>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<ArchetypeComponent_Sync>(entity) && entityManager.GetComponentData<ArchetypeComponent_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new ArchetypeComponent());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalPersistence:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<Persistence>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<Persistence_Sync>(entity) && entityManager.GetComponentData<Persistence_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new Persistence());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericPrefabReference:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericPrefabReference>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericPrefabReference_Sync>(entity) && entityManager.GetComponentData<GenericPrefabReference_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericPrefabReference());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericScale:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericScale>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericScale_Sync>(entity) && entityManager.GetComponentData<GenericScale_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericScale());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt0>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt0_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt0_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt1:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt1>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt1_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt1_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt1());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt2:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt2>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt2_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt2_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt3:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt3>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt3_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt3_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt3());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt4:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt4>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt4_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt4_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt4());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt5:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt5>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt5_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt5_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt5());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt6:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt6>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt6_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt6_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt6());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt7:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt7>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt7_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt7_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt7());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt8:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt8>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt8_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt8_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt8());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldInt9:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldInt9>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldInt9_Sync>(entity) && entityManager.GetComponentData<GenericFieldInt9_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt9());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat0>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat0_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat0_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat1:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat1>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat1_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat1_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat1());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat2:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat2>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat2_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat2_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat3:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat3>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat3_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat3_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat3());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat4:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat4>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat4_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat4_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat4());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat5:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat5>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat5_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat5_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat5());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat6:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat6>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat6_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat6_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat6());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat7:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat7>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat7_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat7_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat7());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat8:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat8>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat8_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat8_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat8());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat9:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat9>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldFloat9_Sync>(entity) && entityManager.GetComponentData<GenericFieldFloat9_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldFloat9());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldVector0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldVector0>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldVector0_Sync>(entity) && entityManager.GetComponentData<GenericFieldVector0_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldVector0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldVector1:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldVector1>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldVector1_Sync>(entity) && entityManager.GetComponentData<GenericFieldVector1_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldVector1());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldVector2:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldVector2>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldVector2_Sync>(entity) && entityManager.GetComponentData<GenericFieldVector2_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldVector2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldVector3:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldVector3>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldVector3_Sync>(entity) && entityManager.GetComponentData<GenericFieldVector3_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldVector3());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldString0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldString0>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldString0_Sync>(entity) && entityManager.GetComponentData<GenericFieldString0_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldString0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldString1:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldString1>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldString1_Sync>(entity) && entityManager.GetComponentData<GenericFieldString1_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldString1());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldString2:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldString2>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldString2_Sync>(entity) && entityManager.GetComponentData<GenericFieldString2_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldString2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldString4:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldString4>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldString4_Sync>(entity) && entityManager.GetComponentData<GenericFieldString4_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldString4());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldQuaternion0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldQuaternion0>(entity);
					var componentHasBeenRemoved = entityManager.HasComponent<GenericFieldQuaternion0_Sync>(entity) && entityManager.GetComponentData<GenericFieldQuaternion0_Sync>(entity).deletedAtTime > 0;
					if (!hasComponentData && !componentHasBeenRemoved)
					{
						entityManager.AddComponentData(entity, new GenericFieldQuaternion0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				default:
				{
					Log.Warning("can not create component type");
					break;
				}
			}
		}
	}

	public class ComponentDeserializeWrapper : ISchemaSpecificComponentDeserialize
	{
		private DeserializeComponentUpdateGenerated deserializeComponentUpdateGenerated;

		public ComponentDeserializeWrapper(UnityMapper mapper)
		{
			deserializeComponentUpdateGenerated = new DeserializeComponentUpdateGenerated(mapper);
		}

		public void CreateIfNeededAndReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
			deserializeComponentUpdateGenerated.CreateIfNeededAndReadComponentDataUpdate(entityManager, entity, componentType, simulationFrame, bitStream);
		}

		public void ReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
		{
			deserializeComponentUpdateGenerated.ReadComponentDataUpdate(entityManager, entity, componentType, simulationFrame, bitStream);
		}
	}
}

// ------------------ end of DeserializeComponentUpdate.cs -----------------
#endregion
