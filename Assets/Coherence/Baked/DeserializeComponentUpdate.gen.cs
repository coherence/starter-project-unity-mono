


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
			if (!entityManager.HasComponent<InterpolationComponent_Translation_value>(entity))
			{
				var interpolationData = new InterpolationComponent_Translation_value()
				{
					setupID = InterpolationSetupID.DefaultTranslation
				};
				entityManager.AddComponentData(entity, interpolationData);
				entityManager.AddComponent<Sample_Translation_value>(entity);
			}

			// Append buffer for components that use interpolation
			InterpolationSystem_Translation
				.AppendvalueBuffer(entity, tempComponentData, entityManager.World, (ulong) simulationFrame.Frame);
		}

		private void InterpolateRotation(EntityManager entityManager, Entity entity, AbsoluteSimulationFrame simulationFrame, Rotation tempComponentData)
		{

			// Ensure entities with interpolation also have Interpolation components and Sample components
			if (!entityManager.HasComponent<InterpolationComponent_Rotation_value>(entity))
			{
				var interpolationData = new InterpolationComponent_Rotation_value()
				{
					setupID = InterpolationSetupID.DefaultRotation
				};
				entityManager.AddComponentData(entity, interpolationData);
				entityManager.AddComponent<Sample_Rotation_value>(entity);
			}

			// Append buffer for components that use interpolation
			InterpolationSystem_Rotation
				.AppendvalueBuffer(entity, tempComponentData, entityManager.World, (ulong) simulationFrame.Frame);
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

		private void DeserializeConnectedEntity(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<ConnectedEntity>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'ConnectedEntity' to entity {entity}");
				entityManager.AddComponent<ConnectedEntity>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new ConnectedEntity();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<ConnectedEntity>(entity);
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

		private void DeserializeGenericFieldBool0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool0' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool1(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool1>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool1' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool1>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool1();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool1>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool2(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool2>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool2' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool2>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool2();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool2>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool3(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool3>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool3' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool3>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool3();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool3>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool4(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool4>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool4' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool4>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool4();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool4>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool5(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool5>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool5' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool5>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool5();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool5>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool6(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool6>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool6' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool6>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool6();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool6>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool7(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool7>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool7' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool7>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool7();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool7>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool8(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool8>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool8' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool8>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool8();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool8>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldBool9(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldBool9>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldBool9' to entity {entity}");
				entityManager.AddComponent<GenericFieldBool9>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldBool9();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldBool9>(entity);
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

		private void DeserializeGenericFieldString3(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldString3>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldString3' to entity {entity}");
				entityManager.AddComponent<GenericFieldString3>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldString3();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldString3>(entity);
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

		private void DeserializeGenericFieldEntity0(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity0>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity0' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity0>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity0();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity0>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity1(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity1>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity1' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity1>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity1();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity1>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity2(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity2>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity2' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity2>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity2();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity2>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity3(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity3>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity3' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity3>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity3();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity3>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity4(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity4>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity4' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity4>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity4();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity4>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity5(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity5>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity5' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity5>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity5();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity5>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity6(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity6>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity6' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity6>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity6();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity6>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity7(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity7>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity7' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity7>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity7();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity7>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity8(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity8>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity8' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity8>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity8();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity8>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeGenericFieldEntity9(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<GenericFieldEntity9>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'GenericFieldEntity9' to entity {entity}");
				entityManager.AddComponent<GenericFieldEntity9>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new GenericFieldEntity9();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<GenericFieldEntity9>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializeCube_Cube(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<Cube_Cube>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'Cube_Cube' to entity {entity}");
				entityManager.AddComponent<Cube_Cube>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new Cube_Cube();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<Cube_Cube>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
		}

		private void DeserializePlayer_Controller(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
		{
			if (!entityManager.HasComponent<Player_Controller>(entity))
			{
				// UnityEngine.Debug.Log($"Had to add component 'Player_Controller' to entity {entity}");
				entityManager.AddComponent<Player_Controller>(entity);
			}

			// If we own the entity, don't overwrite with downstream data from server
			// TODO: Server should never send downstream to the simulating client
			if (componentOwnership)
			{
				// Read and discard data (the stream must always be read)
				var temp = new Player_Controller();
				unityReaders.Read(ref temp, protocolStream);
				return;
			}
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<Player_Controller>(entity);
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
			case TypeIds.InternalConnectedEntity:
				DeserializeConnectedEntity(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
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
			case TypeIds.InternalGenericFieldBool0:
				DeserializeGenericFieldBool0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool1:
				DeserializeGenericFieldBool1(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool2:
				DeserializeGenericFieldBool2(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool3:
				DeserializeGenericFieldBool3(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool4:
				DeserializeGenericFieldBool4(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool5:
				DeserializeGenericFieldBool5(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool6:
				DeserializeGenericFieldBool6(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool7:
				DeserializeGenericFieldBool7(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool8:
				DeserializeGenericFieldBool8(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldBool9:
				DeserializeGenericFieldBool9(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
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
			case TypeIds.InternalGenericFieldString3:
				DeserializeGenericFieldString3(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldString4:
				DeserializeGenericFieldString4(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldQuaternion0:
				DeserializeGenericFieldQuaternion0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity0:
				DeserializeGenericFieldEntity0(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity1:
				DeserializeGenericFieldEntity1(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity2:
				DeserializeGenericFieldEntity2(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity3:
				DeserializeGenericFieldEntity3(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity4:
				DeserializeGenericFieldEntity4(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity5:
				DeserializeGenericFieldEntity5(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity6:
				DeserializeGenericFieldEntity6(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity7:
				DeserializeGenericFieldEntity7(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity8:
				DeserializeGenericFieldEntity8(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalGenericFieldEntity9:
				DeserializeGenericFieldEntity9(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalCube_Cube:
				DeserializeCube_Cube(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
			case TypeIds.InternalPlayer_Controller:
				DeserializePlayer_Controller(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
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

			{
				var hasBuffer = entityManager.HasComponent<Player_Controller_Foo>(entity);
				if (!hasBuffer)
				{
					entityManager.AddBuffer<Player_Controller_Foo>(entity);
				}

				var hasRequestBuffer = entityManager.HasComponent<Player_Controller_FooRequest>(entity);
				if (!hasRequestBuffer)
				{
					entityManager.AddBuffer<Player_Controller_FooRequest>(entity);
				}
			}

			{
				var hasBuffer = entityManager.HasComponent<Player_Controller_Boo>(entity);
				if (!hasBuffer)
				{
					entityManager.AddBuffer<Player_Controller_Boo>(entity);
				}

				var hasRequestBuffer = entityManager.HasComponent<Player_Controller_BooRequest>(entity);
				if (!hasRequestBuffer)
				{
					entityManager.AddBuffer<Player_Controller_BooRequest>(entity);
				}
			}

#endregion

			switch (componentType)
			{
				case TypeIds.InternalWorldPosition:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<Translation>(entity);
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new Persistence());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalConnectedEntity:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<ConnectedEntity>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new ConnectedEntity());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericPrefabReference:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericPrefabReference>(entity);
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldInt9());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool0>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool1:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool1>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool1());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool2:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool2>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool3:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool3>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool3());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool4:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool4>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool4());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool5:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool5>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool5());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool6:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool6>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool6());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool7:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool7>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool7());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool8:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool8>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool8());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldBool9:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldBool9>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldBool9());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldFloat0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldFloat0>(entity);
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
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
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldString2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldString3:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldString3>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldString3());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldString4:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldString4>(entity);
					if (!hasComponentData)
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
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldQuaternion0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity0:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity0>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity0());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity1:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity1>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity1());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity2:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity2>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity2());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity3:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity3>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity3());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity4:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity4>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity4());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity5:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity5>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity5());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity6:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity6>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity6());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity7:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity7>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity7());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity8:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity8>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity8());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalGenericFieldEntity9:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<GenericFieldEntity9>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new GenericFieldEntity9());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalCube_Cube:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<Cube_Cube>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new Cube_Cube());
						justCreated = true;
					}

					ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated);
					break;
				}
				case TypeIds.InternalPlayer_Controller:
				{
					var justCreated = false;
					var hasComponentData = entityManager.HasComponent<Player_Controller>(entity);
					if (!hasComponentData)
					{
						entityManager.AddComponentData(entity, new Player_Controller());
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
