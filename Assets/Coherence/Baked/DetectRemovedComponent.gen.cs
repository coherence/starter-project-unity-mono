


#region DetectRemovedComponent
// -----------------------------------
//  DetectRemovedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
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

			Entities.WithNone<global::Coherence.Generated.LocalUser>().ForEach((Entity entity, ref LocalUser_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.WorldPositionQuery>().ForEach((Entity entity, ref WorldPositionQuery_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.ArchetypeComponent>().ForEach((Entity entity, ref ArchetypeComponent_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalArchetypeComponent,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.Persistence>().ForEach((Entity entity, ref Persistence_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalPersistence,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericPrefabReference>().ForEach((Entity entity, ref GenericPrefabReference_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericScale>().ForEach((Entity entity, ref GenericScale_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt0>().ForEach((Entity entity, ref GenericFieldInt0_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt1>().ForEach((Entity entity, ref GenericFieldInt1_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt2>().ForEach((Entity entity, ref GenericFieldInt2_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt3>().ForEach((Entity entity, ref GenericFieldInt3_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt4>().ForEach((Entity entity, ref GenericFieldInt4_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt5>().ForEach((Entity entity, ref GenericFieldInt5_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt6>().ForEach((Entity entity, ref GenericFieldInt6_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt7>().ForEach((Entity entity, ref GenericFieldInt7_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt8>().ForEach((Entity entity, ref GenericFieldInt8_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt9>().ForEach((Entity entity, ref GenericFieldInt9_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool0>().ForEach((Entity entity, ref GenericFieldBool0_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool0,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool1>().ForEach((Entity entity, ref GenericFieldBool1_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool1,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool2>().ForEach((Entity entity, ref GenericFieldBool2_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool2,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool3>().ForEach((Entity entity, ref GenericFieldBool3_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool3,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool4>().ForEach((Entity entity, ref GenericFieldBool4_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool4,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool5>().ForEach((Entity entity, ref GenericFieldBool5_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool5,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool6>().ForEach((Entity entity, ref GenericFieldBool6_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool6,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool7>().ForEach((Entity entity, ref GenericFieldBool7_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool7,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool8>().ForEach((Entity entity, ref GenericFieldBool8_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool8,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool9>().ForEach((Entity entity, ref GenericFieldBool9_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldBool9,
                    mask = 0,
                    resendMask = 0,
                });
            }).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat0>().ForEach((Entity entity, ref GenericFieldFloat0_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat1>().ForEach((Entity entity, ref GenericFieldFloat1_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat2>().ForEach((Entity entity, ref GenericFieldFloat2_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat3>().ForEach((Entity entity, ref GenericFieldFloat3_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat4>().ForEach((Entity entity, ref GenericFieldFloat4_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat5>().ForEach((Entity entity, ref GenericFieldFloat5_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat6>().ForEach((Entity entity, ref GenericFieldFloat6_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat7>().ForEach((Entity entity, ref GenericFieldFloat7_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat8>().ForEach((Entity entity, ref GenericFieldFloat8_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat9>().ForEach((Entity entity, ref GenericFieldFloat9_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector0>().ForEach((Entity entity, ref GenericFieldVector0_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector1>().ForEach((Entity entity, ref GenericFieldVector1_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector2>().ForEach((Entity entity, ref GenericFieldVector2_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector3>().ForEach((Entity entity, ref GenericFieldVector3_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldString0>().ForEach((Entity entity, ref GenericFieldString0_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldString1>().ForEach((Entity entity, ref GenericFieldString1_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldString2>().ForEach((Entity entity, ref GenericFieldString2_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldString4>().ForEach((Entity entity, ref GenericFieldString4_Sync sync, in Simulated sim) =>
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

			Entities.WithNone<global::Coherence.Generated.GenericFieldQuaternion0>().ForEach((Entity entity, ref GenericFieldQuaternion0_Sync sync, in Simulated sim) =>
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
                    componentType = TypeIds.InternalGenericFieldQuaternion0,
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
