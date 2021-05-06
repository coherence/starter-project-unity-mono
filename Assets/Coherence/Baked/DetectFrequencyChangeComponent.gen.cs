


#region DetectFrequencyChangeComponent
// -----------------------------------
//  DetectFrequencyChangeComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;

	[UpdateInGroup(typeof(GatherChangesGroup))]
	[UpdateBefore(typeof(DetectRemovedComponentsSystem))]
	public class DetectFrequencyComponentsSystem : SystemBase
	{

		private EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			m_EndSimulationEcbSystem = World
				.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{

			var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref WorldPosition_Sync sync, in SetWorldPositionFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetWorldPositionFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref WorldOrientation_Sync sync, in SetWorldOrientationFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetWorldOrientationFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref LocalUser_Sync sync, in SetLocalUserFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetLocalUserFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref WorldPositionQuery_Sync sync, in SetWorldPositionQueryFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetWorldPositionQueryFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref ArchetypeComponent_Sync sync, in SetArchetypeComponentFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetArchetypeComponentFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref Persistence_Sync sync, in SetPersistenceFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetPersistenceFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref ConnectedEntity_Sync sync, in SetConnectedEntityFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetConnectedEntityFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericPrefabReference_Sync sync, in SetGenericPrefabReferenceFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericPrefabReferenceFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericScale_Sync sync, in SetGenericScaleFrequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericScaleFrequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt0_Sync sync, in SetGenericFieldInt0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt1_Sync sync, in SetGenericFieldInt1Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt1Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt2_Sync sync, in SetGenericFieldInt2Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt2Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt3_Sync sync, in SetGenericFieldInt3Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt3Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt4_Sync sync, in SetGenericFieldInt4Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt4Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt5_Sync sync, in SetGenericFieldInt5Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt5Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt6_Sync sync, in SetGenericFieldInt6Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt6Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt7_Sync sync, in SetGenericFieldInt7Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt7Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt8_Sync sync, in SetGenericFieldInt8Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt8Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldInt9_Sync sync, in SetGenericFieldInt9Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldInt9Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool0_Sync sync, in SetGenericFieldBool0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool1_Sync sync, in SetGenericFieldBool1Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool1Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool2_Sync sync, in SetGenericFieldBool2Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool2Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool3_Sync sync, in SetGenericFieldBool3Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool3Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool4_Sync sync, in SetGenericFieldBool4Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool4Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool5_Sync sync, in SetGenericFieldBool5Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool5Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool6_Sync sync, in SetGenericFieldBool6Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool6Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool7_Sync sync, in SetGenericFieldBool7Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool7Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool8_Sync sync, in SetGenericFieldBool8Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool8Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldBool9_Sync sync, in SetGenericFieldBool9Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldBool9Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat0_Sync sync, in SetGenericFieldFloat0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat1_Sync sync, in SetGenericFieldFloat1Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat1Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat2_Sync sync, in SetGenericFieldFloat2Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat2Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat3_Sync sync, in SetGenericFieldFloat3Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat3Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat4_Sync sync, in SetGenericFieldFloat4Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat4Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat5_Sync sync, in SetGenericFieldFloat5Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat5Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat6_Sync sync, in SetGenericFieldFloat6Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat6Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat7_Sync sync, in SetGenericFieldFloat7Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat7Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat8_Sync sync, in SetGenericFieldFloat8Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat8Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldFloat9_Sync sync, in SetGenericFieldFloat9Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldFloat9Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector0_Sync sync, in SetGenericFieldVector0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldVector0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector1_Sync sync, in SetGenericFieldVector1Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldVector1Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector2_Sync sync, in SetGenericFieldVector2Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldVector2Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldVector3_Sync sync, in SetGenericFieldVector3Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldVector3Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString0_Sync sync, in SetGenericFieldString0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldString0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString1_Sync sync, in SetGenericFieldString1Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldString1Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString2_Sync sync, in SetGenericFieldString2Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldString2Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString3_Sync sync, in SetGenericFieldString3Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldString3Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldString4_Sync sync, in SetGenericFieldString4Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldString4Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldQuaternion0_Sync sync, in SetGenericFieldQuaternion0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldQuaternion0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity0_Sync sync, in SetGenericFieldEntity0Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity0Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity1_Sync sync, in SetGenericFieldEntity1Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity1Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity2_Sync sync, in SetGenericFieldEntity2Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity2Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity3_Sync sync, in SetGenericFieldEntity3Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity3Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity4_Sync sync, in SetGenericFieldEntity4Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity4Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity5_Sync sync, in SetGenericFieldEntity5Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity5Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity6_Sync sync, in SetGenericFieldEntity6Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity6Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity7_Sync sync, in SetGenericFieldEntity7Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity7Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity8_Sync sync, in SetGenericFieldEntity8Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity8Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();
			Entities
			.ForEach((Entity entity, int entityInQueryIndex, ref GenericFieldEntity9_Sync sync, in SetGenericFieldEntity9Frequency data) =>
			{
				sync.minSamplingTime = 1/data.samplingFrequency;
				ecb.RemoveComponent<SetGenericFieldEntity9Frequency>(entityInQueryIndex, entity);
			}).ScheduleParallel();

			m_EndSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
		}
	}
}
// ------------------ end of DetectFrequencyChangeComponent.cs -----------------
#endregion
