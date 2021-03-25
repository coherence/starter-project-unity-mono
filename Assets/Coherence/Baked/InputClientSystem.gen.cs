


#region InputClientSystem
// -----------------------------------
//  InputClientSystem.cs
// -----------------------------------
			
namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Collections;
	using UnityEngine;
	using Coherence.Replication.Client.Unity.Ecs;
	using System.Collections.Generic;
	using System.Linq;
	using System;

	//Empty tag to mark which ClientInput entities are local to this client
	public struct LocalInputClient : IComponentData { }

	public class InputClientSystem : SystemBase
	{
		public const int MAX_INPUT_EVENTS = 32;
		public const int MAX_INPUT_WINDOW = 1;
		public const int MAX_INPUT_MESSAGE_SIZE = 64; //octets

		private Dictionary<Entity, List<InputEvent>> _inputEvents = new Dictionary<Entity, List<InputEvent>>();
		private Dictionary<Entity, bool> _inputDirty = new Dictionary<Entity, bool>();

		private CoherenceSystemGroup _coherenceSystemGroup;

		protected override void OnCreate()
		{
			base.OnCreate();

			_coherenceSystemGroup = World.GetExistingSystem<CoherenceSystemGroup>();
		}

		public void OnKeyInput(Entity entity, KeyCode keyCode, bool state)
		{
			var inputEvent = new InputEvent()
			{
				type = InputType.Button,
				simulationFrame = _coherenceSystemGroup.SimulationFrame,
				ID = (short)keyCode,
			};

			inputEvent.SetButtonState(state);

			OnInput(entity, inputEvent);
		}

		public void OnStickInput(Entity entity, short stickID, float2 value)
		{
			var inputEvent = new InputEvent()
			{
				type = InputType.StickVector,
				simulationFrame = _coherenceSystemGroup.SimulationFrame,
				ID = stickID,
				value = Basal.Vector2f.FromFloats(value.x, value.y),
			};

			OnInput(entity, inputEvent);
		}

		public void OnMouseInput(Entity entity, float2 value)
		{
			var inputEvent = new InputEvent()
			{
				type = InputType.MouseVector,
				simulationFrame = _coherenceSystemGroup.SimulationFrame,
				ID = 0,
				value = Basal.Vector2f.FromFloats(value.x, value.y),
			};

			OnInput(entity, inputEvent);
		}

		private void OnInput(Entity entity, InputEvent inputEvent)
		{
			List<InputEvent> inputEvents;

			if (!_inputEvents.ContainsKey(entity))
			{
				_inputEvents.Add(entity, new List<InputEvent>(MAX_INPUT_EVENTS));
			}

			inputEvents = _inputEvents[entity];

			for (int i = inputEvents.Count - 1; i >= 0; i--)
			{
				var evt = inputEvents[i];
				if (evt.simulationFrame < inputEvent.simulationFrame)
				{
					break;
				}
				else if (evt.type == inputEvent.type && evt.ID == inputEvent.ID)
				{
					inputEvents[i] = inputEvent;
					return;
				}
			}

			inputEvents.Add(inputEvent);

			_inputDirty[entity] = true;
		}

		private List<InputEvent> GetInputEventsForEntity(Entity entity)
		{
			if (!_inputEvents.ContainsKey(entity))
			{
				return new List<InputEvent>();
			}

			var simulationFrame = _coherenceSystemGroup.SimulationFrame;
			var inputEvents = _inputEvents[entity];
			var events = new List<InputEvent>(inputEvents.Count);
			foreach (var e in inputEvents)
			{
				if (simulationFrame - e.simulationFrame <= MAX_INPUT_WINDOW)
				{
					events.Add(e);
				}
			}

			return events;
		}

		private void ClearOldInputEvents()
		{
			var eventListsToRemove = _inputEvents.Where(kv => !EntityManager.Exists(kv.Key) || !EntityManager.HasComponent<LocalInputClient>(kv.Key)).ToArray();
			foreach (var kv in eventListsToRemove)
			{
				_inputEvents.Remove(kv.Key);
			}

			var simulationFrame = _coherenceSystemGroup.SimulationFrame;
			foreach (var kv in _inputEvents)
			{
				var events = kv.Value;
				var eventsToRemove = events.Where(e => simulationFrame - e.simulationFrame > MAX_INPUT_WINDOW).ToArray();
				foreach (var e in eventsToRemove)
				{
					events.Remove(e);
				}
			}
		}

		protected override void OnUpdate()
		{
			var isSimulator = World.GetExistingSystem<NetworkSystem>().ConnectionType == ConnectionType.Simulator;
			if (isSimulator)
			{
				return;
			}

			//Automatically transfer all input clients to the requesting server upon request.
			Entities.WithAll<InputClient>().ForEach((Entity entity, ref DynamicBuffer<AuthorityTransfer> requestBuffer) =>
			{
				if (requestBuffer.IsEmpty)
				{
					return;
				}

				int participant = 0;

				foreach (var request in requestBuffer)
				{
					participant = request.participant;
					break;
				}

				requestBuffer.Clear();

				// Give ownership to requesting participant
				var transferAction = new TransferAction
				{
					participant = participant,
					accepted = true
				};
				EntityManager.AddComponentData(entity, transferAction);

			}).WithoutBurst().WithStructuralChanges().Run();

			//Should consider only sending the input update message in the next simulation frame so you can be sure to have captured all the
			//inputs in any given frame.  This means the inputs will always be a frame behind but you won't accidentally send two messages in one
			//simulationFrame
				Entities.WithAll<LocalInputClient>().ForEach((Entity entity) =>
			{
				if (_inputDirty.ContainsKey(entity) && _inputDirty[entity])
				{
					var inputEvents = GetInputEventsForEntity(entity);

					var writer = new Brook.OctetWriter(MAX_INPUT_MESSAGE_SIZE);
					var stream = new Brook.OutBitStream(writer);

					for (int i = 0; i < inputEvents.Count; i++)
					{
						var inputEvent = inputEvents[i];

						var rollback = stream.Tell;

						if (stream.RemainingBitCount <= InputEvent.BITS_FOR_TYPE)
						{
							//no more room for inputs.
							if (i < inputEvents.Count - 1)
							{
								Debug.LogWarning($"Too many inputs, {inputEvents.Count - i} skipped");
							}
							break;
						}

						//Debug.LogWarning($"Event! Entity: {entity} key: {key.keyCode} value: {key.value} frame: {key.simulationFrame} ");

						var ok = inputEvent.Serialize(ref stream);

						if (!ok)
						{
							Debug.LogWarning($"Too many inputs, {inputEvents.Count - i} skipped");
							stream.Rewind(rollback);
							break;

						}

						if (i == inputEvents.Count - 1)
						{
							try
							{
								//indicate this is the last entry.
								stream.WriteBits((uint)InputType.TotalTypes, InputEvent.BITS_FOR_TYPE);
							}
							catch
							{
								Debug.LogWarning($"Too many inputs, {inputEvents.Count - i} skipped");
								stream.Rewind(rollback);
								break;
							}
						}
					}

					stream.Flush();

					_inputDirty[entity] = false;

					var hasRequestBuffer = EntityManager.HasComponent<InputClientCommandRequest>(entity);
					if (!hasRequestBuffer)
					{
						EntityManager.AddBuffer<InputClientCommandRequest>(entity);
					}

					var buffer = EntityManager.GetBuffer<InputClientCommandRequest>(entity);
					buffer.Add(new InputClientCommandRequest
					{
						inputs = new FixedString64(Convert.ToBase64String(writer.Octets))
					});
				}
			}).WithStructuralChanges().WithoutBurst().Run();

			//These are probably very slow.
			ClearOldInputEvents();
		}
	}
}
// ------------------ end of InputClientSystem.cs -----------------
#endregion
