


#region DetectChangedComponent
// -----------------------------------
//  DetectChangedComponent.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal.IntegrationTests
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

		
			Dependency.Complete();
        }
    }
}
// ------------------ end of DetectChangedComponent.cs -----------------
#endregion
