


#region DetectChangedComponent
// -----------------------------------
//  DetectChangedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;
	using System.Diagnostics;
	using Unity.Collections;
	using Unity.Mathematics;
	using Coherence.DeltaEcs;

	[UpdateInGroup(typeof(GatherChangesGroup))]
	public class DetectChangedComponentsSystem : SystemBase
	{
		private NativeHashMap<EntityComponentTuple, ComponentChange> componentChanges;
		private NetworkSystem networkSystem;
		private FrameCountSystem frameCountSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			componentChanges = new NativeHashMap<EntityComponentTuple, ComponentChange>(65536, Allocator.Persistent);
			networkSystem = World.GetExistingSystem<NetworkSystem>();
			frameCountSystem = World.GetExistingSystem<FrameCountSystem>();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			componentChanges.Dispose();
		}

		static bool HasNoticeableDifference(float a, float b, float epsilonFloat)
		{
			return math.abs(a - b) > epsilonFloat;
		}

		static bool HasNoticeableDifference(FixedString64 a, FixedString64 b)
		{
			return !a.Equals(b);
		}

		static bool HasNoticeableDifference(int a, int b)
		{
			return !a.Equals(b);
		}

		static bool HasNoticeableDifference(float2 a, float2 b, float epsilonFloat)
		{
			if (HasNoticeableDifference(a.x, b.x, epsilonFloat)) return true;
			if (HasNoticeableDifference(a.y, b.y, epsilonFloat)) return true;

			return false;
		}

		static bool HasNoticeableDifference(float3 a, float3 b, float epsilonFloat)
		{
			if (HasNoticeableDifference(a.x, b.x, epsilonFloat)) return true;
			if (HasNoticeableDifference(a.y, b.y, epsilonFloat)) return true;
			if (HasNoticeableDifference(a.z, b.z, epsilonFloat)) return true;

			return false;
		}

		static bool HasNoticeableDifference(quaternion a, quaternion b, float epsilonFloat)
		{
			if (HasNoticeableDifference(a.value.x, b.value.x, epsilonFloat)) return true;
			if (HasNoticeableDifference(a.value.y, b.value.y, epsilonFloat)) return true;
			if (HasNoticeableDifference(a.value.z, b.value.z, epsilonFloat)) return true;
			if (HasNoticeableDifference(a.value.w, b.value.w, epsilonFloat)) return true;

			return false;
		}

		protected override void OnUpdate()
		{
			var localComponentChanges = componentChanges.AsParallelWriter();
			var simulationFrame = frameCountSystem.SimulationFrame;
			var time = Time.ElapsedTime;

			Entities.ForEach((Entity entity, ref WorldPosition_Sync sync, in Translation data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.Translation_value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.Value.x, CoherenceLimits.Translation_value_Min, CoherenceLimits.Translation_value_Max);
				CheckRange(data.Value.y, CoherenceLimits.Translation_value_Min, CoherenceLimits.Translation_value_Max);
				CheckRange(data.Value.z, CoherenceLimits.Translation_value_Min, CoherenceLimits.Translation_value_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalWorldPosition,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref WorldOrientation_Sync sync, in Rotation data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.Rotation_value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalWorldOrientation,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref LocalUser_Sync sync, in global::Coherence.Generated.LocalUser data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.localIndex != sync.lastSentData.localIndex) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.localIndex, CoherenceLimits.LocalUser_localIndex_Min, CoherenceLimits.LocalUser_localIndex_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalLocalUser,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref WorldPositionQuery_Sync sync, in global::Coherence.Generated.WorldPositionQuery data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.position, sync.lastSentData.position, CoherenceLimits.WorldPositionQuery_position_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (data.radius != sync.lastSentData.radius) mask |= 0b00000000000000000000000000000010;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.position.x, CoherenceLimits.WorldPositionQuery_position_Min, CoherenceLimits.WorldPositionQuery_position_Max);
				CheckRange(data.position.y, CoherenceLimits.WorldPositionQuery_position_Min, CoherenceLimits.WorldPositionQuery_position_Max);
				CheckRange(data.position.z, CoherenceLimits.WorldPositionQuery_position_Min, CoherenceLimits.WorldPositionQuery_position_Max);
				CheckRange(data.radius, CoherenceLimits.WorldPositionQuery_radius_Min, CoherenceLimits.WorldPositionQuery_radius_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalWorldPositionQuery,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref ArchetypeComponent_Sync sync, in global::Coherence.Generated.ArchetypeComponent data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.index != sync.lastSentData.index) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.index, CoherenceLimits.ArchetypeComponent_index_Min, CoherenceLimits.ArchetypeComponent_index_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalArchetypeComponent,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref Persistence_Sync sync, in global::Coherence.Generated.Persistence data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.uuid, sync.lastSentData.uuid) ) mask |= 0b00000000000000000000000000000001;

				if (HasNoticeableDifference(data.expiry, sync.lastSentData.expiry) ) mask |= 0b00000000000000000000000000000010;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalPersistence,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref ConnectedEntity_Sync sync, in global::Coherence.Generated.ConnectedEntity data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.value != sync.lastSentData.value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalConnectedEntity,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericPrefabReference_Sync sync, in global::Coherence.Generated.GenericPrefabReference data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.prefab, sync.lastSentData.prefab) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericPrefabReference,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericScale_Sync sync, in global::Coherence.Generated.GenericScale data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.GenericScale_Value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.Value.x, CoherenceLimits.GenericScale_Value_Min, CoherenceLimits.GenericScale_Value_Max);
				CheckRange(data.Value.y, CoherenceLimits.GenericScale_Value_Min, CoherenceLimits.GenericScale_Value_Max);
				CheckRange(data.Value.z, CoherenceLimits.GenericScale_Value_Min, CoherenceLimits.GenericScale_Value_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericScale,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt0_Sync sync, in global::Coherence.Generated.GenericFieldInt0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt0_number_Min, CoherenceLimits.GenericFieldInt0_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt1_Sync sync, in global::Coherence.Generated.GenericFieldInt1 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt1_number_Min, CoherenceLimits.GenericFieldInt1_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt1,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt2_Sync sync, in global::Coherence.Generated.GenericFieldInt2 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt2_number_Min, CoherenceLimits.GenericFieldInt2_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt2,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt3_Sync sync, in global::Coherence.Generated.GenericFieldInt3 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt3_number_Min, CoherenceLimits.GenericFieldInt3_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt3,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt4_Sync sync, in global::Coherence.Generated.GenericFieldInt4 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt4_number_Min, CoherenceLimits.GenericFieldInt4_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt4,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt5_Sync sync, in global::Coherence.Generated.GenericFieldInt5 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt5_number_Min, CoherenceLimits.GenericFieldInt5_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt5,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt6_Sync sync, in global::Coherence.Generated.GenericFieldInt6 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt6_number_Min, CoherenceLimits.GenericFieldInt6_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt6,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt7_Sync sync, in global::Coherence.Generated.GenericFieldInt7 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt7_number_Min, CoherenceLimits.GenericFieldInt7_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt7,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt8_Sync sync, in global::Coherence.Generated.GenericFieldInt8 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt8_number_Min, CoherenceLimits.GenericFieldInt8_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt8,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldInt9_Sync sync, in global::Coherence.Generated.GenericFieldInt9 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldInt9_number_Min, CoherenceLimits.GenericFieldInt9_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt9,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool0_Sync sync, in global::Coherence.Generated.GenericFieldBool0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool1_Sync sync, in global::Coherence.Generated.GenericFieldBool1 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool1,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool2_Sync sync, in global::Coherence.Generated.GenericFieldBool2 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool2,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool3_Sync sync, in global::Coherence.Generated.GenericFieldBool3 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool3,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool4_Sync sync, in global::Coherence.Generated.GenericFieldBool4 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool4,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool5_Sync sync, in global::Coherence.Generated.GenericFieldBool5 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool5,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool6_Sync sync, in global::Coherence.Generated.GenericFieldBool6 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool6,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool7_Sync sync, in global::Coherence.Generated.GenericFieldBool7 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool7,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool8_Sync sync, in global::Coherence.Generated.GenericFieldBool8 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool8,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldBool9_Sync sync, in global::Coherence.Generated.GenericFieldBool9 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool9,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat0_Sync sync, in global::Coherence.Generated.GenericFieldFloat0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat0_number_Min, CoherenceLimits.GenericFieldFloat0_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat1_Sync sync, in global::Coherence.Generated.GenericFieldFloat1 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat1_number_Min, CoherenceLimits.GenericFieldFloat1_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat1,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat2_Sync sync, in global::Coherence.Generated.GenericFieldFloat2 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat2_number_Min, CoherenceLimits.GenericFieldFloat2_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat2,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat3_Sync sync, in global::Coherence.Generated.GenericFieldFloat3 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat3_number_Min, CoherenceLimits.GenericFieldFloat3_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat3,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat4_Sync sync, in global::Coherence.Generated.GenericFieldFloat4 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat4_number_Min, CoherenceLimits.GenericFieldFloat4_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat4,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat5_Sync sync, in global::Coherence.Generated.GenericFieldFloat5 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat5_number_Min, CoherenceLimits.GenericFieldFloat5_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat5,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat6_Sync sync, in global::Coherence.Generated.GenericFieldFloat6 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat6_number_Min, CoherenceLimits.GenericFieldFloat6_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat6,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat7_Sync sync, in global::Coherence.Generated.GenericFieldFloat7 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat7_number_Min, CoherenceLimits.GenericFieldFloat7_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat7,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat8_Sync sync, in global::Coherence.Generated.GenericFieldFloat8 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat8_number_Min, CoherenceLimits.GenericFieldFloat8_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat8,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldFloat9_Sync sync, in global::Coherence.Generated.GenericFieldFloat9 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.number != sync.lastSentData.number) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.number, CoherenceLimits.GenericFieldFloat9_number_Min, CoherenceLimits.GenericFieldFloat9_number_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat9,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector0_Sync sync, in global::Coherence.Generated.GenericFieldVector0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.GenericFieldVector0_Value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.Value.x, CoherenceLimits.GenericFieldVector0_Value_Min, CoherenceLimits.GenericFieldVector0_Value_Max);
				CheckRange(data.Value.y, CoherenceLimits.GenericFieldVector0_Value_Min, CoherenceLimits.GenericFieldVector0_Value_Max);
				CheckRange(data.Value.z, CoherenceLimits.GenericFieldVector0_Value_Min, CoherenceLimits.GenericFieldVector0_Value_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector1_Sync sync, in global::Coherence.Generated.GenericFieldVector1 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.GenericFieldVector1_Value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.Value.x, CoherenceLimits.GenericFieldVector1_Value_Min, CoherenceLimits.GenericFieldVector1_Value_Max);
				CheckRange(data.Value.y, CoherenceLimits.GenericFieldVector1_Value_Min, CoherenceLimits.GenericFieldVector1_Value_Max);
				CheckRange(data.Value.z, CoherenceLimits.GenericFieldVector1_Value_Min, CoherenceLimits.GenericFieldVector1_Value_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector1,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector2_Sync sync, in global::Coherence.Generated.GenericFieldVector2 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.GenericFieldVector2_Value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.Value.x, CoherenceLimits.GenericFieldVector2_Value_Min, CoherenceLimits.GenericFieldVector2_Value_Max);
				CheckRange(data.Value.y, CoherenceLimits.GenericFieldVector2_Value_Min, CoherenceLimits.GenericFieldVector2_Value_Max);
				CheckRange(data.Value.z, CoherenceLimits.GenericFieldVector2_Value_Min, CoherenceLimits.GenericFieldVector2_Value_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector2,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldVector3_Sync sync, in global::Coherence.Generated.GenericFieldVector3 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.GenericFieldVector3_Value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}
				CheckRange(data.Value.x, CoherenceLimits.GenericFieldVector3_Value_Min, CoherenceLimits.GenericFieldVector3_Value_Max);
				CheckRange(data.Value.y, CoherenceLimits.GenericFieldVector3_Value_Min, CoherenceLimits.GenericFieldVector3_Value_Max);
				CheckRange(data.Value.z, CoherenceLimits.GenericFieldVector3_Value_Min, CoherenceLimits.GenericFieldVector3_Value_Max);

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector3,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString0_Sync sync, in global::Coherence.Generated.GenericFieldString0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.name, sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString1_Sync sync, in global::Coherence.Generated.GenericFieldString1 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.name, sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString1,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString2_Sync sync, in global::Coherence.Generated.GenericFieldString2 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.name, sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString2,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString3_Sync sync, in global::Coherence.Generated.GenericFieldString3 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.name, sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString3,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldString4_Sync sync, in global::Coherence.Generated.GenericFieldString4 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.name, sync.lastSentData.name) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString4,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldQuaternion0_Sync sync, in global::Coherence.Generated.GenericFieldQuaternion0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (HasNoticeableDifference(data.Value, sync.lastSentData.Value, CoherenceLimits.GenericFieldQuaternion0_Value_Epsilon) ) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldQuaternion0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity0_Sync sync, in global::Coherence.Generated.GenericFieldEntity0 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity0,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity1_Sync sync, in global::Coherence.Generated.GenericFieldEntity1 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity1,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity2_Sync sync, in global::Coherence.Generated.GenericFieldEntity2 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity2,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity3_Sync sync, in global::Coherence.Generated.GenericFieldEntity3 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity3,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity4_Sync sync, in global::Coherence.Generated.GenericFieldEntity4 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity4,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity5_Sync sync, in global::Coherence.Generated.GenericFieldEntity5 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity5,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity6_Sync sync, in global::Coherence.Generated.GenericFieldEntity6 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity6,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity7_Sync sync, in global::Coherence.Generated.GenericFieldEntity7 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity7,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity8_Sync sync, in global::Coherence.Generated.GenericFieldEntity8 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity8,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Entities.ForEach((Entity entity, ref GenericFieldEntity9_Sync sync, in global::Coherence.Generated.GenericFieldEntity9 data, in Simulated simulate) =>
			{
				if (time - sync.lastSampledTime < sync.minSamplingTime)
				{
					return;
				}


				uint mask = 0;
				if (!sync.hasBeenSerialized)
				{
					mask = 0xffffffff;
				}

				if (data.Value != sync.lastSentData.Value) mask |= 0b00000000000000000000000000000001;

				if (mask == 0)
				{
					return;
				}

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity9,
					state = sync.hasReceivedConstructor ? ComponentState.Update : ComponentState.Construct,
					priority = (int)sync.howImportantAreYou,
					mask = mask,
					simulationFrame = simulationFrame,
					componentHasReceivedConstructor = sync.hasReceivedConstructor,
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				sync.lastSentData = data;
				sync.hasBeenSerialized = true;
				sync.lastSampledTime = time;
			}).ScheduleParallel();

			Dependency.Complete();

			foreach (var change in componentChanges.GetValueArray(Allocator.Temp))
			{
				networkSystem.changeBuffer.UpdateEntity(change.entity, change);
			}

			componentChanges.Clear();
		}

		[Conditional("UNITY_EDITOR")]
		private static void CheckRange(float x, float min, float max)
		{
			if(x.CompareTo(min) < 0) {
				UnityEngine.Debug.LogWarning($"float out of range: {x} (must be at least {min}.) See https://docs.coherence.io/optimization/world-size");
				return;
			}
			if(x.CompareTo(max) >= 0) {
				UnityEngine.Debug.LogWarning($"float out of range: {x} (must be less than {max}.) See https://docs.coherence.io/optimization/world-size");
			}
		}

		[Conditional("UNITY_EDITOR")]
		private static void CheckRange(int x, int min, int max)
		{
			if(x.CompareTo(min) < 0) {
				UnityEngine.Debug.LogWarning($"int out of range: {x} (must be at least {min}.) See https://docs.coherence.io/optimization/world-size");
				return;
			}
			if(x.CompareTo(max) >= 0) {
				UnityEngine.Debug.LogWarning($"int out of range: {x} (must be less than {max}.) See https://docs.coherence.io/optimization/world-size");
			}
		}
	}
}
// ------------------ end of DetectChangedComponent.cs -----------------
#endregion
