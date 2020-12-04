


#region RuntimeSystem
// -----------------------------------
//  RuntimeSystem.cs
// -----------------------------------
			
namespace Coherence.Sdk.Unity
{
	using Coherence.Generated.Internal.IntegrationTests;
	using global::Unity.Entities;
	using global::Unity.Transforms;
	using Coherence.Log;
	using Replication.Client.Unity.Ecs;
	using global::Coherence.Generated.FirstProject;

	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public class CoherenceRuntimeSystem : ComponentSystem
	{
		protected override void OnCreate()
		{
			#region Register all known component types and their enums
			           GlobalLookups.Register<Translation>(TypeEnums.InternalWorldPosition);
           GlobalLookups.Register<Rotation>(TypeEnums.InternalWorldOrientation);
           GlobalLookups.Register<LocalUser>(TypeEnums.InternalLocalUser);
           GlobalLookups.Register<WorldPositionQuery>(TypeEnums.InternalWorldPositionQuery);
           GlobalLookups.Register<SessionBased>(TypeEnums.InternalSessionBased);

			#endregion

			#region Register all known component types and their component type id
			           GlobalTypeIdLookups.Register<Translation>(TypeIds.InternalWorldPosition);
           GlobalTypeIdLookups.Register<Rotation>(TypeIds.InternalWorldOrientation);
           GlobalTypeIdLookups.Register<LocalUser>(TypeIds.InternalLocalUser);
           GlobalTypeIdLookups.Register<WorldPositionQuery>(TypeIds.InternalWorldPositionQuery);
           GlobalTypeIdLookups.Register<SessionBased>(TypeIds.InternalSessionBased);

			#endregion

			base.OnCreate();
		}

		protected override void OnUpdate()
		{
		}
	}
}

// ------------------ end of RuntimeSystem.cs -----------------
#endregion
