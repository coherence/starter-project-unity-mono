


#region RuntimeSystem
// -----------------------------------
//  RuntimeSystem.cs
// -----------------------------------
			
namespace Coherence.Sdk.Unity
{
	using Coherence.Generated.Internal.Schema;
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
           GlobalLookups.Register<GenericPrefabReference>(TypeEnums.InternalGenericPrefabReference);
           GlobalLookups.Register<GenericScale>(TypeEnums.InternalGenericScale);
           GlobalLookups.Register<GenericFieldInt0>(TypeEnums.InternalGenericFieldInt0);
           GlobalLookups.Register<GenericFieldInt1>(TypeEnums.InternalGenericFieldInt1);
           GlobalLookups.Register<GenericFieldInt2>(TypeEnums.InternalGenericFieldInt2);
           GlobalLookups.Register<GenericFieldInt3>(TypeEnums.InternalGenericFieldInt3);
           GlobalLookups.Register<GenericFieldInt4>(TypeEnums.InternalGenericFieldInt4);
           GlobalLookups.Register<GenericFieldInt5>(TypeEnums.InternalGenericFieldInt5);
           GlobalLookups.Register<GenericFieldInt6>(TypeEnums.InternalGenericFieldInt6);
           GlobalLookups.Register<GenericFieldInt7>(TypeEnums.InternalGenericFieldInt7);
           GlobalLookups.Register<GenericFieldInt8>(TypeEnums.InternalGenericFieldInt8);
           GlobalLookups.Register<GenericFieldInt9>(TypeEnums.InternalGenericFieldInt9);
           GlobalLookups.Register<GenericFieldFloat0>(TypeEnums.InternalGenericFieldFloat0);
           GlobalLookups.Register<GenericFieldFloat1>(TypeEnums.InternalGenericFieldFloat1);
           GlobalLookups.Register<GenericFieldFloat2>(TypeEnums.InternalGenericFieldFloat2);
           GlobalLookups.Register<GenericFieldFloat3>(TypeEnums.InternalGenericFieldFloat3);
           GlobalLookups.Register<GenericFieldFloat4>(TypeEnums.InternalGenericFieldFloat4);
           GlobalLookups.Register<GenericFieldFloat5>(TypeEnums.InternalGenericFieldFloat5);
           GlobalLookups.Register<GenericFieldFloat6>(TypeEnums.InternalGenericFieldFloat6);
           GlobalLookups.Register<GenericFieldFloat7>(TypeEnums.InternalGenericFieldFloat7);
           GlobalLookups.Register<GenericFieldFloat8>(TypeEnums.InternalGenericFieldFloat8);
           GlobalLookups.Register<GenericFieldFloat9>(TypeEnums.InternalGenericFieldFloat9);
           GlobalLookups.Register<GenericFieldVector0>(TypeEnums.InternalGenericFieldVector0);
           GlobalLookups.Register<GenericFieldVector1>(TypeEnums.InternalGenericFieldVector1);
           GlobalLookups.Register<GenericFieldVector2>(TypeEnums.InternalGenericFieldVector2);
           GlobalLookups.Register<GenericFieldVector3>(TypeEnums.InternalGenericFieldVector3);
           GlobalLookups.Register<GenericFieldString0>(TypeEnums.InternalGenericFieldString0);
           GlobalLookups.Register<GenericFieldString1>(TypeEnums.InternalGenericFieldString1);
           GlobalLookups.Register<GenericFieldString2>(TypeEnums.InternalGenericFieldString2);
           GlobalLookups.Register<GenericFieldString4>(TypeEnums.InternalGenericFieldString4);
           GlobalLookups.Register<NPCBehaviour>(TypeEnums.InternalNPCBehaviour);
           GlobalLookups.Register<RotationBehaviour>(TypeEnums.InternalRotationBehaviour);
           GlobalLookups.Register<ShowNameAndState>(TypeEnums.InternalShowNameAndState);
           GlobalLookups.Register<PlayerBehaviour>(TypeEnums.InternalPlayerBehaviour);
           GlobalLookups.Register<Bullet>(TypeEnums.InternalBullet);
           GlobalLookups.Register<ColorizeBehaviour>(TypeEnums.InternalColorizeBehaviour);
           GlobalLookups.Register<Controller>(TypeEnums.InternalController);

			#endregion

			#region Register all known component types and their component type id
			           GlobalTypeIdLookups.Register<Translation>(TypeIds.InternalWorldPosition);
           GlobalTypeIdLookups.Register<Rotation>(TypeIds.InternalWorldOrientation);
           GlobalTypeIdLookups.Register<LocalUser>(TypeIds.InternalLocalUser);
           GlobalTypeIdLookups.Register<WorldPositionQuery>(TypeIds.InternalWorldPositionQuery);
           GlobalTypeIdLookups.Register<SessionBased>(TypeIds.InternalSessionBased);
           GlobalTypeIdLookups.Register<GenericPrefabReference>(TypeIds.InternalGenericPrefabReference);
           GlobalTypeIdLookups.Register<GenericScale>(TypeIds.InternalGenericScale);
           GlobalTypeIdLookups.Register<GenericFieldInt0>(TypeIds.InternalGenericFieldInt0);
           GlobalTypeIdLookups.Register<GenericFieldInt1>(TypeIds.InternalGenericFieldInt1);
           GlobalTypeIdLookups.Register<GenericFieldInt2>(TypeIds.InternalGenericFieldInt2);
           GlobalTypeIdLookups.Register<GenericFieldInt3>(TypeIds.InternalGenericFieldInt3);
           GlobalTypeIdLookups.Register<GenericFieldInt4>(TypeIds.InternalGenericFieldInt4);
           GlobalTypeIdLookups.Register<GenericFieldInt5>(TypeIds.InternalGenericFieldInt5);
           GlobalTypeIdLookups.Register<GenericFieldInt6>(TypeIds.InternalGenericFieldInt6);
           GlobalTypeIdLookups.Register<GenericFieldInt7>(TypeIds.InternalGenericFieldInt7);
           GlobalTypeIdLookups.Register<GenericFieldInt8>(TypeIds.InternalGenericFieldInt8);
           GlobalTypeIdLookups.Register<GenericFieldInt9>(TypeIds.InternalGenericFieldInt9);
           GlobalTypeIdLookups.Register<GenericFieldFloat0>(TypeIds.InternalGenericFieldFloat0);
           GlobalTypeIdLookups.Register<GenericFieldFloat1>(TypeIds.InternalGenericFieldFloat1);
           GlobalTypeIdLookups.Register<GenericFieldFloat2>(TypeIds.InternalGenericFieldFloat2);
           GlobalTypeIdLookups.Register<GenericFieldFloat3>(TypeIds.InternalGenericFieldFloat3);
           GlobalTypeIdLookups.Register<GenericFieldFloat4>(TypeIds.InternalGenericFieldFloat4);
           GlobalTypeIdLookups.Register<GenericFieldFloat5>(TypeIds.InternalGenericFieldFloat5);
           GlobalTypeIdLookups.Register<GenericFieldFloat6>(TypeIds.InternalGenericFieldFloat6);
           GlobalTypeIdLookups.Register<GenericFieldFloat7>(TypeIds.InternalGenericFieldFloat7);
           GlobalTypeIdLookups.Register<GenericFieldFloat8>(TypeIds.InternalGenericFieldFloat8);
           GlobalTypeIdLookups.Register<GenericFieldFloat9>(TypeIds.InternalGenericFieldFloat9);
           GlobalTypeIdLookups.Register<GenericFieldVector0>(TypeIds.InternalGenericFieldVector0);
           GlobalTypeIdLookups.Register<GenericFieldVector1>(TypeIds.InternalGenericFieldVector1);
           GlobalTypeIdLookups.Register<GenericFieldVector2>(TypeIds.InternalGenericFieldVector2);
           GlobalTypeIdLookups.Register<GenericFieldVector3>(TypeIds.InternalGenericFieldVector3);
           GlobalTypeIdLookups.Register<GenericFieldString0>(TypeIds.InternalGenericFieldString0);
           GlobalTypeIdLookups.Register<GenericFieldString1>(TypeIds.InternalGenericFieldString1);
           GlobalTypeIdLookups.Register<GenericFieldString2>(TypeIds.InternalGenericFieldString2);
           GlobalTypeIdLookups.Register<GenericFieldString4>(TypeIds.InternalGenericFieldString4);
           GlobalTypeIdLookups.Register<NPCBehaviour>(TypeIds.InternalNPCBehaviour);
           GlobalTypeIdLookups.Register<RotationBehaviour>(TypeIds.InternalRotationBehaviour);
           GlobalTypeIdLookups.Register<ShowNameAndState>(TypeIds.InternalShowNameAndState);
           GlobalTypeIdLookups.Register<PlayerBehaviour>(TypeIds.InternalPlayerBehaviour);
           GlobalTypeIdLookups.Register<Bullet>(TypeIds.InternalBullet);
           GlobalTypeIdLookups.Register<ColorizeBehaviour>(TypeIds.InternalColorizeBehaviour);
           GlobalTypeIdLookups.Register<Controller>(TypeIds.InternalController);

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
