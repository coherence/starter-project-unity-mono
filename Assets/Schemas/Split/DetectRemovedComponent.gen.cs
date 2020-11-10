


#region DetectRemovedComponent
// -----------------------------------
//  DetectRemovedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.Schema
{
	using global::Coherence.Generated.FirstProject;
	using Coherence;
	using Coherence.Replication.Client.Unity.Ecs;
	using Coherence.Replication.Unity;
	using Unity.Entities;
	using Unity.Transforms;

    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateBefore(typeof(DetectChangedComponentsSystem))]
    public class DetectRemovedComponentsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var simulationFrame = World.GetOrCreateSystem<CoherenceSimulationSystemGroup>().SimulationFrame;
			var componentChanges = World.GetExistingSystem<SyncSendSystem>().ComponentChanges;
			var localComponentChanges = componentChanges.AsParallelWriter();
			

			Entities.WithNone<Translation>().ForEach((Entity entity, ref WorldPosition_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalWorldPosition,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<Rotation>().ForEach((Entity entity, ref WorldOrientation_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalWorldOrientation,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.LocalUser>().ForEach((Entity entity, ref LocalUser_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalLocalUser,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.WorldPositionQuery>().ForEach((Entity entity, ref WorldPositionQuery_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalWorldPositionQuery,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.SessionBased>().ForEach((Entity entity, ref SessionBased_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalSessionBased,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericPrefabReference>().ForEach((Entity entity, ref GenericPrefabReference_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericPrefabReference,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericScale>().ForEach((Entity entity, ref GenericScale_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericScale,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt0>().ForEach((Entity entity, ref GenericFieldInt0_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt0,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt1>().ForEach((Entity entity, ref GenericFieldInt1_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt1,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt2>().ForEach((Entity entity, ref GenericFieldInt2_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt2,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt3>().ForEach((Entity entity, ref GenericFieldInt3_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt3,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt4>().ForEach((Entity entity, ref GenericFieldInt4_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt4,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt5>().ForEach((Entity entity, ref GenericFieldInt5_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt5,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt6>().ForEach((Entity entity, ref GenericFieldInt6_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt6,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt7>().ForEach((Entity entity, ref GenericFieldInt7_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt7,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt8>().ForEach((Entity entity, ref GenericFieldInt8_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt8,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldInt9>().ForEach((Entity entity, ref GenericFieldInt9_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldInt9,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat0>().ForEach((Entity entity, ref GenericFieldFloat0_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat0,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat1>().ForEach((Entity entity, ref GenericFieldFloat1_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat1,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat2>().ForEach((Entity entity, ref GenericFieldFloat2_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat2,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat3>().ForEach((Entity entity, ref GenericFieldFloat3_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat3,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat4>().ForEach((Entity entity, ref GenericFieldFloat4_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat4,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat5>().ForEach((Entity entity, ref GenericFieldFloat5_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat5,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat6>().ForEach((Entity entity, ref GenericFieldFloat6_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat6,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat7>().ForEach((Entity entity, ref GenericFieldFloat7_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat7,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat8>().ForEach((Entity entity, ref GenericFieldFloat8_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat8,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldFloat9>().ForEach((Entity entity, ref GenericFieldFloat9_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldFloat9,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldVector0>().ForEach((Entity entity, ref GenericFieldVector0_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldVector0,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldVector1>().ForEach((Entity entity, ref GenericFieldVector1_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldVector1,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldVector2>().ForEach((Entity entity, ref GenericFieldVector2_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldVector2,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldVector3>().ForEach((Entity entity, ref GenericFieldVector3_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldVector3,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldString0>().ForEach((Entity entity, ref GenericFieldString0_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldString0,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldString1>().ForEach((Entity entity, ref GenericFieldString1_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldString1,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldString2>().ForEach((Entity entity, ref GenericFieldString2_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldString2,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.FirstProject.GenericFieldString4>().ForEach((Entity entity, ref GenericFieldString4_Sync sync, in Simulated sim) =>
            {
                if (sync.deleteHasBeenSerialized)
                {
                    return;
                }
                
                if (sync.deletedAtTime == default)
                {
                    sync.deletedAtTime = (long)simulationFrame;
                }

                localComponentChanges.Add(sync.accumulatedPriority, new ComponentChange
                {
                    entity = entity,
                    componentType = TypeIds.InternalGenericFieldString4,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();


			Dependency.Complete();
        }
    }
}
// ------------------ end of DetectRemovedComponent.cs -----------------
#endregion
