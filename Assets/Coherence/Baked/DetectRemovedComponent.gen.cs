


#region DetectRemovedComponent
// -----------------------------------
//  DetectRemovedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using Coherence;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Transforms;
	using Coherence.DeltaEcs;

	[UpdateInGroup(typeof(GatherChangesGroup))]
	public class DetectRemovedComponentsSystem : SystemBase
	{
		private NativeHashMap<EntityComponentTuple, ComponentChange> componentChanges;
		private NetworkSystem networkSystem;
		private FrameCountSystem frameCountSystem;
		private EndInitializationEntityCommandBufferSystem endInitializationEntityCommandBufferSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			componentChanges = new NativeHashMap<EntityComponentTuple, ComponentChange>(65536, Allocator.Persistent);
			networkSystem = World.GetExistingSystem<NetworkSystem>();
			frameCountSystem = World.GetExistingSystem<FrameCountSystem>();
			endInitializationEntityCommandBufferSystem = World.GetExistingSystem<EndInitializationEntityCommandBufferSystem>();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			componentChanges.Dispose();
		}

		protected override void OnUpdate()
		{
			var localComponentChanges = componentChanges.AsParallelWriter();
			var simulationFrame = frameCountSystem.SimulationFrame;
			var commandBufferWriter = endInitializationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();


			Entities.WithNone<Translation>().ForEach((Entity entity, int entityInQueryIndex, ref WorldPosition_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalWorldPosition,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<WorldPosition_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<Rotation>().ForEach((Entity entity, int entityInQueryIndex, ref WorldOrientation_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalWorldOrientation,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<WorldOrientation_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.LocalUser>().ForEach((Entity entity, int entityInQueryIndex, ref LocalUser_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalLocalUser,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<LocalUser_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.WorldPositionQuery>().ForEach((Entity entity, int entityInQueryIndex, ref WorldPositionQuery_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalWorldPositionQuery,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<WorldPositionQuery_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.ArchetypeComponent>().ForEach((Entity entity, int entityInQueryIndex, ref ArchetypeComponent_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalArchetypeComponent,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<ArchetypeComponent_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.Persistence>().ForEach((Entity entity, int entityInQueryIndex, ref Persistence_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalPersistence,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<Persistence_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.ConnectedEntity>().ForEach((Entity entity, int entityInQueryIndex, ref ConnectedEntity_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalConnectedEntity,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<ConnectedEntity_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericPrefabReference>().ForEach((Entity entity, int entityInQueryIndex, ref GenericPrefabReference_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericPrefabReference,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericPrefabReference_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericScale>().ForEach((Entity entity, int entityInQueryIndex, ref GenericScale_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericScale,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericScale_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt1>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt1_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt1,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt1_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt2>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt2_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt2,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt2_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt3>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt3_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt3,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt3_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt4>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt4_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt4,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt4_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt5>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt5_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt5,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt5_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt6>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt6_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt6,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt6_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt7>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt7_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt7,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt7_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt8>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt8_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt8,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt8_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldInt9>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt9_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldInt9,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldInt9_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool1>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool1_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool1,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool1_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool2>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool2_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool2,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool2_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool3>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool3_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool3,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool3_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool4>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool4_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool4,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool4_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool5>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool5_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool5,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool5_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool6>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool6_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool6,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool6_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool7>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool7_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool7,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool7_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool8>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool8_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool8,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool8_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldBool9>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool9_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldBool9,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldBool9_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat1>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat1_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat1,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat1_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat2>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat2_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat2,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat2_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat3>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat3_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat3,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat3_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat4>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat4_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat4,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat4_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat5>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat5_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat5,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat5_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat6>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat6_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat6,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat6_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat7>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat7_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat7,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat7_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat8>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat8_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat8,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat8_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldFloat9>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat9_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldFloat9,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldFloat9_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldVector0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector1>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector1_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector1,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldVector1_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector2>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector2_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector2,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldVector2_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldVector3>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector3_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldVector3,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldVector3_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldString0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldString0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldString1>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString1_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString1,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldString1_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldString2>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString2_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString2,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldString2_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldString3>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString3_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString3,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldString3_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldString4>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString4_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldString4,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldString4_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldQuaternion0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldQuaternion0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldQuaternion0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldQuaternion0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity0>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity0_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity0,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity0_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity1>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity1_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity1,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity1_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity2>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity2_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity2,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity2_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity3>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity3_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity3,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity3_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity4>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity4_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity4,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity4_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity5>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity5_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity5,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity5_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity6>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity6_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity6,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity6_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity7>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity7_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity7,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity7_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity8>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity8_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity8,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity8_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			Entities.WithNone<global::Coherence.Generated.GenericFieldEntity9>().ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity9_Sync sync, in Simulated sim) =>
			{

				var componentChange = new ComponentChange
				{
					entity = entity,
					componentType = TypeIds.InternalGenericFieldEntity9,
					state = ComponentState.Destruct,
					priority = (int)sync.howImportantAreYou,
					simulationFrame = simulationFrame
				};

				localComponentChanges.TryAdd(new EntityComponentTuple(entity, componentChange.componentType), componentChange);

				commandBufferWriter.RemoveComponent<GenericFieldEntity9_Sync>(entityInQueryIndex, entity);
			}).ScheduleParallel();


			Dependency.Complete();

			foreach (var change in componentChanges.GetValueArray(Allocator.Temp))
			{
				networkSystem.changeBuffer.UpdateEntity(change.entity, change);
			}

			componentChanges.Clear();
		}
	}
}
// ------------------ end of DetectRemovedComponent.cs -----------------
#endregion
