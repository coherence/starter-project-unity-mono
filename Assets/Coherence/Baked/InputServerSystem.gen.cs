


#region InputServerSystem
// -----------------------------------
//  InputServerSystem.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Unity.Entities;
	using Coherence.Replication.Client.Unity.Ecs;
	using System.Collections.Generic;
	using System.Linq;
	using System;

	//Empty tag to mark which ClientInput entities are remote to this server
	public struct RemoteInputClient : IComponentData { }

	public class InputServerSystem : SystemBase
	{
		//the main issue with these as events is that they have to be called on the main thread or we can't trust the context.
		public System.Action<Entity, InputEvent> OnButtonInputEvent;
		public System.Action<Entity, InputEvent> OnJoystickInputEvent;
		public System.Action<Entity, InputEvent> OnMouseInputEvent;

		private Dictionary<Entity, List<InputEvent>> _inputEvents = new Dictionary<Entity, List<InputEvent>>();
		private Dictionary<Entity, ulong> _lastSentInputFrame = new Dictionary<Entity, ulong>();

		private CoherenceSystemGroup _coherenceSystemGroup;

		protected override void OnCreate()
		{
			base.OnCreate();

			_coherenceSystemGroup = World.GetExistingSystem<CoherenceSystemGroup>();
		}

		private void AddInputEvent(Entity entity, InputEvent inputEvent)
		{
			var inputEvents = _inputEvents[entity];
			inputEvents.Add(inputEvent);
		}

		private void ClearOldInputEvents()
		{
			var eventListsToRemove = _inputEvents.Where(kv => !EntityManager.Exists(kv.Key) || !EntityManager.HasComponent<RemoteInputClient>(kv.Key)).ToArray();
			foreach (var kv in eventListsToRemove)
			{
				_inputEvents.Remove(kv.Key);
			}
		}

		private void ProcessInputs(Entity entity, byte[] inputBytes)
		{
			if (inputBytes == null)
			{
				return;
			}

			//UnityEngine.Debug.LogWarning($"Processing {BitConverter.ToString(inputBytes)}");

			_inputEvents[entity].Clear();

			var reader = new Brook.OctetReader(inputBytes);
			var stream = new Brook.InBitStream(reader, inputBytes.Length * 8);

			var ok = true;
			while (ok)
			{
				var newInput = new InputEvent();
				ok = newInput.Deserialize(stream);

				if (ok)
				{
					AddInputEvent(entity, newInput);
				}
			}
		}

		private void PostInputEvents(Entity entity, ulong simulationFrame)
		{
			if (_lastSentInputFrame.ContainsKey(entity) && _lastSentInputFrame[entity] < simulationFrame)
			{
				var lastSentFrame = _lastSentInputFrame[entity];
				foreach (var e in _inputEvents[entity])
				{
					//Need to sort out the simulation frame sync bewtween clients.
					//if (e.simulationFrame > lastSentFrame && e.simulationFrame <= simulationFrame)
					{
						switch (e.type)
						{
							case InputType.Button: OnButtonInputEvent?.Invoke(entity, e); break;
							case InputType.StickVector: OnJoystickInputEvent?.Invoke(entity, e); break;
							case InputType.MouseVector: OnMouseInputEvent?.Invoke(entity, e); break;
						}
					}
				}

				_lastSentInputFrame[entity] = simulationFrame;
			}
		}

        private void ClaimRemotePlayer(Entity remotePlayerEntity, ulong simulationFrame)
        {
            var connectionID = World.GetExistingSystem<NetworkSystem>().Connector.ConnectionId;
            var hasRequestBuffer = World.EntityManager.HasComponent<AuthorityTransferRequest>(remotePlayerEntity);
            if (!hasRequestBuffer)
            {
                World.EntityManager.AddBuffer<AuthorityTransferRequest>(remotePlayerEntity);
            }

            var buffer = World.EntityManager.GetBuffer<AuthorityTransferRequest>(remotePlayerEntity);
            buffer.Add(new AuthorityTransferRequest
            {
                participant = connectionID
            });

            EntityManager.AddComponent<RemoteInputClient>(remotePlayerEntity);

            _inputEvents.Add(remotePlayerEntity, new List<InputEvent>(InputClientSystem.MAX_INPUT_EVENTS));
            _lastSentInputFrame.Add(remotePlayerEntity, simulationFrame);
        }

		protected override void OnUpdate()
		{
			var isSimulator = World.GetExistingSystem<NetworkSystem>().ConnectionType == ConnectionType.Simulator;
			if (!isSimulator)
			{
				return;
			}

			var simulationFrame = _coherenceSystemGroup.SimulationFrame;

			Entities.WithNone<LocalInputClient, RemoteInputClient>().WithAll<InputClient>().ForEach((Entity entity) =>
			{
				ClaimRemotePlayer(entity, simulationFrame);
			}).WithoutBurst().WithStructuralChanges().Run();

			Entities.ForEach((Entity entity, ref DynamicBuffer<InputClientCommand> commandBuffer) =>
			{
				if (commandBuffer.IsEmpty)
				{
					return;
				}

				byte[] inputBytes = null;

				foreach (var request in commandBuffer)
				{
					inputBytes = Convert.FromBase64String(request.inputs.ToString());
					break;
				}

				commandBuffer.Clear();

				ProcessInputs(entity, inputBytes);

				PostInputEvents(entity, simulationFrame);

			}).WithoutBurst().WithStructuralChanges().Run();

			//These are probably very slow.
			ClearOldInputEvents();
		}
	}	
}
// ------------------ end of InputServerSystem.cs -----------------
#endregion
