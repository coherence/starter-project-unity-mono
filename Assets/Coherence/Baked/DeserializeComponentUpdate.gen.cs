


#region DeserializeComponentUpdate
// -----------------------------------
//  DeserializeComponentUpdate.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal.IntegrationTests
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
				
			case TypeIds.InternalSessionBased:
				DeserializeSessionBased(entityManager, entity, componentOwnership, simulationFrame, inProtocolStream, justCreated, bitStream);
				break;
				

			default:
				Log.Warning("couldn't find component", "componentType", componentType);
				break;
			}
		}
		
        public void CreateIfNeededAndReadComponentDataUpdate(EntityManager entityManager, Entity entity, uint componentType, AbsoluteSimulationFrame simulationFrame, IInBitStream bitStream)
        {
#region Commands

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
