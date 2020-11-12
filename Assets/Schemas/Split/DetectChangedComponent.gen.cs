


#region DetectChangedComponent
// -----------------------------------
//  DetectChangedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.Schema
{
	using global::Coherence.Generated.FirstProject;
	using Coherence.Replication.Client.Unity.Ecs;
	using Coherence.Replication.Unity;
	using Unity.Entities;
	using Unity.Transforms;

	[UpdateInGroup(typeof(PresentationSystemGroup))]
	[UpdateBefore(typeof(SyncSendSystem))]
    public class DetectChangedComponentsSystem : SystemBase
    {
	    protected override void OnUpdate()
	    {
		    var componentChanges = World.GetExistingSystem<SyncSendSystem>().ComponentChanges;
		    var localComponentChanges = componentChanges.AsParallelWriter();


			Entities.ForEach((Entity entity, ref WorldPosition_Sync sync, in Translation data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalWorldPosition,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref WorldOrientation_Sync sync, in Rotation data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalWorldOrientation,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref LocalUser_Sync sync, in global::Coherence.Generated.FirstProject.LocalUser data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.localIndex != sync.lastSentData.localIndex) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalLocalUser,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref WorldPositionQuery_Sync sync, in global::Coherence.Generated.FirstProject.WorldPositionQuery data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.position.Equals(sync.lastSentData.position) ) mask |= 0b00000000000000000000000000000001;



                if (data.radius != sync.lastSentData.radius) mask |= 0b00000000000000000000000000000010;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalWorldPositionQuery,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref SessionBased_Sync sync, in global::Coherence.Generated.FirstProject.SessionBased data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalSessionBased,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericPrefabReference_Sync sync, in global::Coherence.Generated.FirstProject.GenericPrefabReference data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.prefab.Equals(sync.lastSentData.prefab) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericPrefabReference,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericScale_Sync sync, in global::Coherence.Generated.FirstProject.GenericScale data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericScale,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt0_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt0 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt0,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt1_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt1 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt1,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt2_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt2 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt2,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt3_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt3 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt3,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt4_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt4 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt4,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt5_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt5 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt5,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt6_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt6 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt6,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt7_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt7 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt7,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt8_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt8 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt8,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt9_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldInt9 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldInt9,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat0_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat0 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat0,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat1_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat1 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat1,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat2_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat2 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat2,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat3_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat3 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat3,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat4_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat4 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat4,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat5_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat5 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat5,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat6_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat6 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat6,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat7_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat7 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat7,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat8_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat8 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat8,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat9_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldFloat9 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldFloat9,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector0_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldVector0 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldVector0,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector1_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldVector1 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldVector1,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector2_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldVector2 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldVector2,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector3_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldVector3 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.Value.Equals(sync.lastSentData.Value) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldVector3,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString0_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldString0 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.name.Equals(sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldString0,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString1_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldString1 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.name.Equals(sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldString1,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString2_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldString2 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.name.Equals(sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldString2,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString4_Sync sync, in global::Coherence.Generated.FirstProject.GenericFieldString4 data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (!data.name.Equals(sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalGenericFieldString4,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref ColorizeBehaviour_Sync sync, in global::Coherence.Generated.FirstProject.ColorizeBehaviour data, in Simulated simulate) =>
			{
				uint mask = 0;
				if (!sync.hasBeenSerialized) 
				{ 
					mask = 0xffffffff;
				}


                if (data.iii != sync.lastSentData.iii) mask |= 0b00000000000000000000000000000001;



                if (data.fff != sync.lastSentData.fff) mask |= 0b00000000000000000000000000000010;



                if (data.bbb != sync.lastSentData.bbb) mask |= 0b00000000000000000000000000000100;



                if (!data.target.Equals(sync.lastSentData.target) ) mask |= 0b00000000000000000000000000001000;



                if (!data.whatever_works.Equals(sync.lastSentData.whatever_works) ) mask |= 0b00000000000000000000000000010000;



                if (!data.name2.Equals(sync.lastSentData.name2) ) mask |= 0b00000000000000000000000000100000;



				if (mask != 0 || sync.resendMask != 0)
				{
					sync.accumulatedPriority += sync.howImportantAreYou;
					var componentChange = new ComponentChange
					{
						entity = entity,
						componentType = TypeIds.InternalColorizeBehaviour,
						mask = mask,
						resendMask = sync.resendMask,
						entityHasReceivedConstructor = simulate.hasReceivedConstructor,
                        componentHasReceivedConstructor = sync.hasReceivedConstructor,
					};
					
					localComponentChanges.Add(sync.accumulatedPriority, componentChange);
				}
			}).ScheduleParallel();

		
			Dependency.Complete();
        }
    }
}
// ------------------ end of DetectChangedComponent.cs -----------------
#endregion
