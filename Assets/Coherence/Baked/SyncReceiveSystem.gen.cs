


#region SyncReceiveSystem
// -----------------------------------
//  SyncReceiveSystem.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using Coherence.SimulationFrame;
	using Protocol.Deserialize;
	using Unity.Entities;
	using Replication.Client.Unity.Ecs;

	[UpdateInGroup(typeof(InitializationSystemGroup))]
	[AlwaysUpdateSystem]
	public class SyncReceiveSystem : SystemBase
	{
		private Receiver receiver;
		private FrameCountSystem frameCountSystem;
		private bool hasInitialTime;
		private bool isBooted;

		private void BootUp()
		{
			var netSys = World.GetOrCreateSystem<NetworkSystem>();
			var deserializeComponents = new ComponentDeserializeWrapper(netSys.Mapper);
			var skipper = new DeserializeComponentsAndSkipWrapper(netSys.Mapper);
			var commandPerform = new PerformCommands(netSys.Mapper);
			var eventPerform = new PerformEvents(netSys.Mapper);
			var inputPerform = new PerformInputs(netSys.Mapper);
			var receiveUpdate = new ReceiveUpdate(deserializeComponents, skipper, netSys.Mapper);
			receiver = new Receiver(World, receiveUpdate, commandPerform, eventPerform, inputPerform);
			frameCountSystem = World.GetExistingSystem<FrameCountSystem>();
		}

		private void ChangeClockSpeed(ClockSpeedFactor factor)
		{
			const float desiredTimestep = 1f / 60f;
			var timeStep = 0f;
			if (factor.FactorTimesThousand <= 1)
			{
			}
			else
			{
				timeStep = desiredTimestep * 1000f / factor.FactorTimesThousand;
			}

			//simGroup.SetTimeStep(true, timeStep, maxSubsteps);
		}

		protected override void OnUpdate()
		{
			if (!isBooted)
			{
				isBooted = true;
				BootUp();
			}

			// Respond to network disconnect events
			Entities.ForEach((in DisconnectedEvent connected) =>
			{
				// Destroy all networked entities 
				World.GetOrCreateSystem<NetworkSystem>().Mapper.ClearAndDestroyEntities(EntityManager);
				
				// Clear the incoming packet repository
				receiver.ResetPacketRepository();
			}).WithStructuralChanges().WithoutBurst().Run();

			var simulationFrame = new AbsoluteSimulationFrame { Frame = (long)frameCountSystem.SimulationFrame };
			var adjust = receiver.OnUpdate(simulationFrame);

			if (adjust.State != SpecialCommandState.Ignore)
			{
				var needsToOverwriteTime = (!hasInitialTime && adjust.SimulationFrame.Frame != 0) || adjust.State == SpecialCommandState.Reset;

				if (needsToOverwriteTime)
				{
					frameCountSystem.SimulationFrame = (ulong)adjust.SimulationFrame.Frame;
					hasInitialTime = true;
				}

				ChangeClockSpeed(adjust.ClockSpeed);
			}
		}
	}
}

// ------------------ end of SyncReceiveSystem.cs -----------------
#endregion
