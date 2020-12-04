


#region DetectAddedComponent
// -----------------------------------
//  DetectAddedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.IntegrationTests
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

			Dependency.Complete();
        }
    }
}
// ------------------ end of DetectAddedComponent.cs -----------------
#endregion
