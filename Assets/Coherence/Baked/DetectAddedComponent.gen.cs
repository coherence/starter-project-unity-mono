


#region DetectAddedComponent
// -----------------------------------
//  DetectAddedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;

	[UpdateInGroup(typeof(GatherChangesGroup))]
	[UpdateBefore(typeof(DetectRemovedComponentsSystem))]
	public class DetectAddedComponentsSystem : SystemBase
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


			Entities.WithAll<Translation, Simulated>().WithNone<WorldPosition_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new WorldPosition_Sync
				{
					howImportantAreYou = 1000,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<Rotation, Simulated>().WithNone<WorldOrientation_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new WorldOrientation_Sync
				{
					howImportantAreYou = 1000,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.LocalUser, Simulated>().WithNone<LocalUser_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new LocalUser_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.WorldPositionQuery, Simulated>().WithNone<WorldPositionQuery_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new WorldPositionQuery_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.ArchetypeComponent, Simulated>().WithNone<ArchetypeComponent_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new ArchetypeComponent_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.Persistence, Simulated>().WithNone<Persistence_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new Persistence_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.ConnectedEntity, Simulated>().WithNone<ConnectedEntity_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new ConnectedEntity_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericPrefabReference, Simulated>().WithNone<GenericPrefabReference_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericPrefabReference_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericScale, Simulated>().WithNone<GenericScale_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericScale_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt0, Simulated>().WithNone<GenericFieldInt0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt1, Simulated>().WithNone<GenericFieldInt1_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt1_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt2, Simulated>().WithNone<GenericFieldInt2_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt2_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt3, Simulated>().WithNone<GenericFieldInt3_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt3_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt4, Simulated>().WithNone<GenericFieldInt4_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt4_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt5, Simulated>().WithNone<GenericFieldInt5_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt5_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt6, Simulated>().WithNone<GenericFieldInt6_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt6_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt7, Simulated>().WithNone<GenericFieldInt7_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt7_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt8, Simulated>().WithNone<GenericFieldInt8_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt8_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldInt9, Simulated>().WithNone<GenericFieldInt9_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldInt9_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool0, Simulated>().WithNone<GenericFieldBool0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool1, Simulated>().WithNone<GenericFieldBool1_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool1_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool2, Simulated>().WithNone<GenericFieldBool2_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool2_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool3, Simulated>().WithNone<GenericFieldBool3_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool3_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool4, Simulated>().WithNone<GenericFieldBool4_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool4_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool5, Simulated>().WithNone<GenericFieldBool5_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool5_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool6, Simulated>().WithNone<GenericFieldBool6_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool6_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool7, Simulated>().WithNone<GenericFieldBool7_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool7_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool8, Simulated>().WithNone<GenericFieldBool8_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool8_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldBool9, Simulated>().WithNone<GenericFieldBool9_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldBool9_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat0, Simulated>().WithNone<GenericFieldFloat0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat1, Simulated>().WithNone<GenericFieldFloat1_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat1_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat2, Simulated>().WithNone<GenericFieldFloat2_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat2_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat3, Simulated>().WithNone<GenericFieldFloat3_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat3_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat4, Simulated>().WithNone<GenericFieldFloat4_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat4_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat5, Simulated>().WithNone<GenericFieldFloat5_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat5_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat6, Simulated>().WithNone<GenericFieldFloat6_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat6_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat7, Simulated>().WithNone<GenericFieldFloat7_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat7_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat8, Simulated>().WithNone<GenericFieldFloat8_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat8_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldFloat9, Simulated>().WithNone<GenericFieldFloat9_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldFloat9_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldVector0, Simulated>().WithNone<GenericFieldVector0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldVector0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldVector1, Simulated>().WithNone<GenericFieldVector1_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldVector1_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldVector2, Simulated>().WithNone<GenericFieldVector2_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldVector2_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldVector3, Simulated>().WithNone<GenericFieldVector3_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldVector3_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldString0, Simulated>().WithNone<GenericFieldString0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldString0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldString1, Simulated>().WithNone<GenericFieldString1_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldString1_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldString2, Simulated>().WithNone<GenericFieldString2_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldString2_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldString3, Simulated>().WithNone<GenericFieldString3_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldString3_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldString4, Simulated>().WithNone<GenericFieldString4_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldString4_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldQuaternion0, Simulated>().WithNone<GenericFieldQuaternion0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldQuaternion0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity0, Simulated>().WithNone<GenericFieldEntity0_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity0_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity1, Simulated>().WithNone<GenericFieldEntity1_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity1_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity2, Simulated>().WithNone<GenericFieldEntity2_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity2_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity3, Simulated>().WithNone<GenericFieldEntity3_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity3_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity4, Simulated>().WithNone<GenericFieldEntity4_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity4_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity5, Simulated>().WithNone<GenericFieldEntity5_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity5_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity6, Simulated>().WithNone<GenericFieldEntity6_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity6_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity7, Simulated>().WithNone<GenericFieldEntity7_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity7_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity8, Simulated>().WithNone<GenericFieldEntity8_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity8_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			Entities.WithAll<global::Coherence.Generated.GenericFieldEntity9, Simulated>().WithNone<GenericFieldEntity9_Sync>().ForEach((Entity entity, int entityInQueryIndex) =>
			{

				ecb.AddComponent(entityInQueryIndex, entity, new GenericFieldEntity9_Sync
				{
					howImportantAreYou = 100,
					// 30Hz
					minSamplingTime = 0.033f
				});
			}).ScheduleParallel();

			m_EndSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
		}
	}
}
// ------------------ end of DetectAddedComponent.cs -----------------
#endregion
