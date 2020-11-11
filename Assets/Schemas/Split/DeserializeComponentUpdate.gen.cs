


#region DeserializeComponentUpdate
// -----------------------------------
//  DeserializeComponentUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.Schema
{
    using Coherence.Brook;
    using Coherence.Log;
	using Unity.Entities;
	using Unity.Transforms;
    using DeltaEcs;
    using global::Coherence.Generated.FirstProject;
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


        private void DeserializeWorldPosition(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
        {

            // If we own the entity, don't overwrite with downstream data from server
            // TODO: Server should never send downstream to the simulating client
            if (componentOwnership)
	        {
	            // Read and discard data (the stream must always be read) 
	            var temp = new Translation();
				unityReaders.Read(ref temp, protocolStream);
				return;
            }
            
    
			// Ensure entities with interpolation also have Interpolation components and Sample components
			if (!entityManager.HasComponent<InterpolationComponent_Translation>(entity))
			{
				entityManager.AddComponent<InterpolationComponent_Translation>(entity);
				entityManager.AddComponent<Sample_Translation>(entity);
			}

			// Append buffer for components that use interpolation
			var tempComponentData = new Translation();
			unityReaders.Read(ref tempComponentData, protocolStream);
			if (justCreated) // Hack
			{
				entityManager.SetComponentData(entity, tempComponentData);
			}
			InterpolationSystem_Translation.AppendBuffer(entity, tempComponentData, entityManager.World, (ulong) simulationFrame.Frame);
    

		}

        private void DeserializeWorldOrientation(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
        {

            // If we own the entity, don't overwrite with downstream data from server
            // TODO: Server should never send downstream to the simulating client
            if (componentOwnership)
	        {
	            // Read and discard data (the stream must always be read) 
	            var temp = new Rotation();
				unityReaders.Read(ref temp, protocolStream);
				return;
            }
            
    
			// Ensure entities with interpolation also have Interpolation components and Sample components
			if (!entityManager.HasComponent<InterpolationComponent_Rotation>(entity))
			{
				entityManager.AddComponent<InterpolationComponent_Rotation>(entity);
				entityManager.AddComponent<Sample_Rotation>(entity);
			}

			// Append buffer for components that use interpolation
			var tempComponentData = new Rotation();
			unityReaders.Read(ref tempComponentData, protocolStream);
			if (justCreated) // Hack
			{
				entityManager.SetComponentData(entity, tempComponentData);
			}
			InterpolationSystem_Rotation.AppendBuffer(entity, tempComponentData, entityManager.World, (ulong) simulationFrame.Frame);
    

		}

        private void DeserializeLocalUser(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
        {

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

        private void DeserializeSessionBased(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
        {

			// No need to read empty components, just ensure that it's there
            if (!entityManager.HasComponent<SessionBased>(entity))
		    {
				entityManager.AddComponent<SessionBased>(entity);
			}

		}

        private void DeserializeGenericPrefabReference(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
        {

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

        private void DeserializeColorizeBehaviour(EntityManager entityManager, Entity entity, bool componentOwnership, AbsoluteSimulationFrame simulationFrame, Coherence.Replication.Protocol.Definition.IInBitStream protocolStream, bool justCreated, IInBitStream bitStream)
        {

            // If we own the entity, don't overwrite with downstream data from server
            // TODO: Server should never send downstream to the simulating client
            if (componentOwnership)
	        {
	            // Read and discard data (the stream must always be read) 
	            var temp = new ColorizeBehaviour();
				unityReaders.Read(ref temp, protocolStream);
				return;
            }
            
    
			// Overwrite components that don't use interpolation
			var componentData = entityManager.GetComponentData<ColorizeBehaviour>(entity);
			unityReaders.Read(ref componentData, protocolStream);
			entityManager.SetComponentData(entity, componentData);
    

		}

        

        public void ReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream, ILog log)
        {
            ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, false, log);
		}

        public void ReadComponentDataUpdateEx(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream, bool justCreated, ILog log)
        {
            var componentOwnership = Deserializator.ReadComponentOwnership(bitStream, log); // Read bit from stream...
            componentOwnership = entityManager.HasComponent<Simulated>(entity); // Then overwrite it with entity ownership.
            var inProtocolStream = new Coherence.FieldStream.Deserialize.Streams.InBitStream(bitStream, log);
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
				
			case TypeIds.InternalSessionBased:
				DeserializeSessionBased(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
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
				
			case TypeIds.InternalColorizeBehaviour:
				DeserializeColorizeBehaviour(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
				

			default:
				log.Warning("couldn't find component", "componentType", componentType);
				break;
			}
		}
		
        public void CreateIfNeededAndReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream, ILog log)
        {
#region Commands

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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
                    break;
				}

				case TypeIds.InternalSessionBased:
                {
                    var justCreated = false;
                    var hasComponentData = entityManager.HasComponent<SessionBased>(entity);
                    var componentHasBeenRemoved = entityManager.HasComponent<SessionBased_Sync>(entity) && entityManager.GetComponentData<SessionBased_Sync>(entity).deletedAtTime > 0;
                    if (!hasComponentData && !componentHasBeenRemoved)
                    {
                        entityManager.AddComponentData(entity, new SessionBased());
                        justCreated = true;
                    }

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
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

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
                    break;
				}

				case TypeIds.InternalColorizeBehaviour:
                {
                    var justCreated = false;
                    var hasComponentData = entityManager.HasComponent<ColorizeBehaviour>(entity);
                    var componentHasBeenRemoved = entityManager.HasComponent<ColorizeBehaviour_Sync>(entity) && entityManager.GetComponentData<ColorizeBehaviour_Sync>(entity).deletedAtTime > 0;
                    if (!hasComponentData && !componentHasBeenRemoved)
                    {
                        entityManager.AddComponentData(entity, new ColorizeBehaviour());
                        justCreated = true;
                    }

                    ReadComponentDataUpdateEx(entityManager, entity, componentType, simulationFrame, bitStream, justCreated, log);
                    break;
				}

                default:
				{
                    log.Warning("can not create component type");
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

        public void CreateIfNeededAndReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream, ILog log)
        {
            deserializeComponentUpdateGenerated.CreateIfNeededAndReadComponentDataUpdate(entityManager, entity, componentType, simulationFrame, bitStream, log);
        }

        public void ReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream, ILog log)
        {
            deserializeComponentUpdateGenerated.ReadComponentDataUpdate(entityManager, entity, componentType, simulationFrame, bitStream, log);
        }
    }

}

// ------------------ end of DeserializeComponentUpdate.cs -----------------
#endregion
