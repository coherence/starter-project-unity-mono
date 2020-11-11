


#region DetectAddedComponent
// -----------------------------------
//  DetectAddedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.Schema
{
	using global::Coherence.Generated.FirstProject;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;

	[UpdateInGroup(typeof(PresentationSystemGroup))]
	[UpdateBefore(typeof(DetectRemovedComponentsSystem))]
    public class DetectAddedComponentsSystem : SystemBase
    {
        protected override void OnUpdate()
        {

            Entities.WithAll<Translation, Simulated>().WithNone<WorldPosition_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new WorldPosition_Sync 
				{
					howImportantAreYou = 1000
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<Rotation, Simulated>().WithNone<WorldOrientation_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new WorldOrientation_Sync 
				{
					howImportantAreYou = 1000
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.LocalUser, Simulated>().WithNone<LocalUser_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new LocalUser_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.WorldPositionQuery, Simulated>().WithNone<WorldPositionQuery_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new WorldPositionQuery_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.SessionBased, Simulated>().WithNone<SessionBased_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new SessionBased_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericPrefabReference, Simulated>().WithNone<GenericPrefabReference_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericPrefabReference_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericScale, Simulated>().WithNone<GenericScale_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericScale_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt0, Simulated>().WithNone<GenericFieldInt0_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt0_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt1, Simulated>().WithNone<GenericFieldInt1_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt1_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt2, Simulated>().WithNone<GenericFieldInt2_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt2_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt3, Simulated>().WithNone<GenericFieldInt3_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt3_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt4, Simulated>().WithNone<GenericFieldInt4_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt4_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt5, Simulated>().WithNone<GenericFieldInt5_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt5_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt6, Simulated>().WithNone<GenericFieldInt6_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt6_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt7, Simulated>().WithNone<GenericFieldInt7_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt7_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt8, Simulated>().WithNone<GenericFieldInt8_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt8_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldInt9, Simulated>().WithNone<GenericFieldInt9_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldInt9_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat0, Simulated>().WithNone<GenericFieldFloat0_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat0_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat1, Simulated>().WithNone<GenericFieldFloat1_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat1_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat2, Simulated>().WithNone<GenericFieldFloat2_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat2_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat3, Simulated>().WithNone<GenericFieldFloat3_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat3_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat4, Simulated>().WithNone<GenericFieldFloat4_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat4_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat5, Simulated>().WithNone<GenericFieldFloat5_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat5_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat6, Simulated>().WithNone<GenericFieldFloat6_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat6_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat7, Simulated>().WithNone<GenericFieldFloat7_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat7_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat8, Simulated>().WithNone<GenericFieldFloat8_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat8_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldFloat9, Simulated>().WithNone<GenericFieldFloat9_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldFloat9_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldVector0, Simulated>().WithNone<GenericFieldVector0_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldVector0_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldVector1, Simulated>().WithNone<GenericFieldVector1_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldVector1_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldVector2, Simulated>().WithNone<GenericFieldVector2_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldVector2_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldVector3, Simulated>().WithNone<GenericFieldVector3_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldVector3_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldString0, Simulated>().WithNone<GenericFieldString0_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldString0_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldString1, Simulated>().WithNone<GenericFieldString1_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldString1_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldString2, Simulated>().WithNone<GenericFieldString2_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldString2_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

            Entities.WithAll<global::Coherence.Generated.FirstProject.GenericFieldString4, Simulated>().WithNone<GenericFieldString4_Sync>().ForEach((Entity entity) =>
			{

				EntityManager.AddComponentData(entity, new GenericFieldString4_Sync 
				{
					howImportantAreYou = 600
				});
			}).WithStructuralChanges().Run();

			Dependency.Complete();
        }
    }
}
// ------------------ end of DetectAddedComponent.cs -----------------
#endregion
