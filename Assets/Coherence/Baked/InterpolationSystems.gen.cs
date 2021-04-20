


#region InterpolationSystems
// -----------------------------------
//  InterpolationSystems.cs
// -----------------------------------
			
namespace Coherence.Generated
{
	using Coherence.Replication.Client.Unity.Ecs;
	using System;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Mathematics;
	using Unity.Transforms;
	using UnityEngine;

	[UpdateInGroup(typeof(PresentationSystemGroup))]
	[AlwaysUpdateSystem]
	public class InterpolationSystem_Translation : InterpolationSystem
	{
		protected override void OnDestroy()
		{
			Interpolation.DestroySetups();
		}

		protected override void OnUpdate()
		{
			Interpolation.CreateSetups();

			var disabled = disableInterpolation;
			var localFrame = World.GetExistingSystem<FrameCountSystem>().SimulationFrame;
			var deltaTime = Time.DeltaTime;
			var setups = Interpolation.GetSetups();

			Entities.WithNone<Simulated>().ForEach((ref Translation translation, ref DynamicBuffer<Sample_Translation_value> buffer, ref InterpolationComponent_Translation_value interpolation) =>
			{
				if (disabled) // Interpolation can be disabled in the ConsolePanel for debug purposes
				{
					if (buffer.Length > 0)
					{
						translation.Value = buffer[buffer.Length - 1].value;
					}
					return;
				}

				InterpolationSetup setup = setups[(int)interpolation.setupID];

				// Calculate current lag-compensated frame
				var frame = localFrame - (ulong)(60 * setup.manualLatency);

				if (setup.autoLatencyFactor > 0f)
				{
					frame = localFrame - (ulong)(60 * interpolation.latency);

					// Prevents entity from moving backwards in time if latency increases quickly
					if (frame < interpolation.previousFrame)
					{
						frame = interpolation.previousFrame;
					}
				}

				// Remember which frame we are at
				interpolation.previousFrame = frame;

				// Can't interpolate with less than 2 samples
				if (buffer.Length < 1)
				{
					return;
				}

				var frames = new NativeArray<ulong>(buffer.Length, Allocator.Temp);
				var values = new NativeArray<float3>(buffer.Length, Allocator.Temp);

				for (int i = 0; i < buffer.Length; i++)
				{
					frames[i] = buffer[i].simulationFrame;
					values[i] = buffer[i].value;
				}

				// TODO: Use float time instead of ulong frame for better interpolation precision
				var targetPosition = setup.curveType == InterpolationCurveType.Linear ?
					GetLinearPoint(values, frames, frame, setup) :
					GetCatmullRomPoint(values, frames, frame, setup);

				// Smoothly move from current position to target (interpolated) position. Elasticity is seconds to reach target.
				if (setup.elasticity > 0)
				{
					translation.Value = SmoothDamp(translation.Value, targetPosition, ref interpolation.velocity, setup.elasticity, 10000, deltaTime);
				} else 
				{
					translation.Value = targetPosition;
				}

				frames.Dispose();
				values.Dispose();
			}).ScheduleParallel();

			Dependency.Complete();
		}

		// Add a new sample to the end of the sample buffer, first removing the oldest sample if the buffer is full
		public static void AppendvalueBuffer(Entity entity, Translation newTranslation, World world, ulong simulationFrame)
		{
			if (!world.EntityManager.HasComponent<InterpolationComponent_Translation_value>(entity))
			{
				Debug.LogWarning("Entity is missing InterpolationComponent_Translation_value: " + entity);
				return;
			}

			var interpolation = world.EntityManager.GetComponentData<InterpolationComponent_Translation_value>(entity);
			var setup = Interpolation.GetSetup(interpolation.setupID);
			var buffer = world.EntityManager.GetBuffer<Sample_Translation_value>(entity);

			// Throttle sample rate by dropping samples on the receiving side. This lowers update frequency for easier interpolation debugging.
			if (throttleSampleRate > 0)
			{
				if (buffer.Length > 0 && simulationFrame < buffer[buffer.Length - 1].simulationFrame + throttleSampleRate)
				{
					return;
				}
			}

			// Remove oldest sample if buffer is full
			if (buffer.Length >= buffer.Capacity)
			{
				buffer.RemoveAt(0);
			}

			var maxDistance = setup.maxDistance;
			if (buffer.Length > 0 && maxDistance >= 0f)
			{
				// Reset sample buffer if interpolation distance limit is exceeded
				var newestSampleInBuffer = buffer[buffer.Length - 1];
				var diff = math.distancesq(newestSampleInBuffer.value, newTranslation.Value);
				if (diff > maxDistance * maxDistance)
				{
					world.EntityManager.SetComponentData(entity, newTranslation);
					buffer.Clear();
				}
			}

			// Create a new sample of the field.
			var sample = new Sample_Translation_value
			{
				simulationFrame = simulationFrame,
				value = newTranslation.Value,
			};

			// Append buffer
			_ = buffer.Add(sample);

			// Update intentional latency to median time between packets.
			// We wait until there are at least 3 samples in the buffer, because the first two samples may be very far apart which would skew the initial latency.
			// E.g., if an entity remains static for a long time (only its initial position is in the buffer) and then starts moving (a new sample appears)
			// then the latency would spike before the third sample appears.
			if (buffer.Length > 2)
			{
				// Median is used instead of average to filter out extreme delta times for components that are updated at irregular intervals.
				var frames = new NativeArray<ulong>(buffer.Length, Allocator.Temp);
				for (int i = 0; i < buffer.Length; i++)
				{
					frames[i] = buffer[i].simulationFrame;
				}

				var medianDelta = CalculateMedianDelta(frames);
				interpolation.latency = setup.autoLatencyFactor * medianDelta / 60f;
				world.EntityManager.SetComponentData(entity, interpolation);
				frames.Dispose();
			}
		}
	}

	[InternalBufferCapacity(5)]
	public struct Sample_Translation_value : IBufferElementData
	{
		public ulong simulationFrame;
		public float3 value;
	}

	public struct InterpolationComponent_Translation_value : IComponentData
	{
		public InterpolationSetupID setupID;
		public float latency;
		public ulong previousFrame;
		public float3 velocity;
	}

	[UpdateInGroup(typeof(PresentationSystemGroup))]
	[AlwaysUpdateSystem]
	public class InterpolationSystem_Rotation : InterpolationSystem
	{
		protected override void OnDestroy()
		{
			Interpolation.DestroySetups();
		}

		protected override void OnUpdate()
		{
			Interpolation.CreateSetups();

			var disabled = disableInterpolation;
			var localFrame = World.GetExistingSystem<FrameCountSystem>().SimulationFrame;
			var deltaTime = Time.DeltaTime;
			var setups = Interpolation.GetSetups();

			Entities.WithNone<Simulated>().ForEach((ref Rotation rotation, ref DynamicBuffer<Sample_Rotation_value> buffer, ref InterpolationComponent_Rotation_value interpolation) =>
			{
				if (disabled) // Interpolation can be disabled in the ConsolePanel for debug purposes
				{
					if (buffer.Length > 0)
					{
						rotation.Value = buffer[buffer.Length - 1].value;
					}
					return;
				}

				InterpolationSetup setup = setups[(int)interpolation.setupID];

				// Calculate current lag-compensated frame
				var frame = localFrame - (ulong)(60 * setup.manualLatency);

				if (setup.autoLatencyFactor > 0f)
				{
					frame = localFrame - (ulong)(60 * interpolation.latency);

					// Prevents entity from moving backwards in time if latency increases quickly
					if (frame < interpolation.previousFrame)
					{
						frame = interpolation.previousFrame;
					}
				}

				// Remember which frame we are at
				interpolation.previousFrame = frame;

				// Can't interpolate with less than 2 samples
				if (buffer.Length < 1)
				{
					return;
				}

				var frames = new NativeArray<ulong>(buffer.Length, Allocator.Temp);
				var values = new NativeArray<quaternion>(buffer.Length, Allocator.Temp);

				for (int i = 0; i < buffer.Length; i++)
				{
					frames[i] = buffer[i].simulationFrame;
					values[i] = buffer[i].value;
				}

				// TODO: Use float time instead of ulong frame for better interpolation precision
				quaternion targetRotation = GetQuaternion(values, frames, frame, setup);

				// Smoothly move from current rotated to target (interpolated) rotated. Elasticity is seconds to reach target.
				rotation.Value = Quaternion.Slerp(rotation.Value, targetRotation, setup.elasticity);

				frames.Dispose();
				values.Dispose();
			}).ScheduleParallel();

			Dependency.Complete();
		}

		// Add a new sample to the end of the sample buffer, first removing the oldest sample if the buffer is full
		public static void AppendvalueBuffer(Entity entity, Rotation newRotation, World world, ulong simulationFrame)
		{
			if (!world.EntityManager.HasComponent<InterpolationComponent_Rotation_value>(entity))
			{
				Debug.LogWarning("Entity is missing InterpolationComponent_Rotation_value: " + entity);
				return;
			}

			var interpolation = world.EntityManager.GetComponentData<InterpolationComponent_Rotation_value>(entity);
			var setup = Interpolation.GetSetup(interpolation.setupID);
			var buffer = world.EntityManager.GetBuffer<Sample_Rotation_value>(entity);

			// Throttle sample rate by dropping samples on the receiving side. This lowers update frequency for easier interpolation debugging.
			if (throttleSampleRate > 0)
			{
				if (buffer.Length > 0 && simulationFrame < buffer[buffer.Length - 1].simulationFrame + throttleSampleRate)
				{
					return;
				}
			}

			// Remove oldest sample if buffer is full
			if (buffer.Length >= buffer.Capacity)
			{
				buffer.RemoveAt(0);
			}

			var maxDistance = setup.maxDistance;
			if (buffer.Length > 0 && maxDistance >= 0f)
			{
				// TODO: test the maxDistance of the rotation and snap rotation if too far.
			}

			// Create a new sample of the field.
			var sample = new Sample_Rotation_value
			{
				simulationFrame = simulationFrame,
				value = newRotation.Value,
			};

			// Append buffer
			_ = buffer.Add(sample);

			// Update intentional latency to median time between packets.
			// We wait until there are at least 3 samples in the buffer, because the first two samples may be very far apart which would skew the initial latency.
			// E.g., if an entity remains static for a long time (only its initial position is in the buffer) and then starts moving (a new sample appears)
			// then the latency would spike before the third sample appears.
			if (buffer.Length > 2)
			{
				// Median is used instead of average to filter out extreme delta times for components that are updated at irregular intervals.
				var frames = new NativeArray<ulong>(buffer.Length, Allocator.Temp);
				for (int i = 0; i < buffer.Length; i++)
				{
					frames[i] = buffer[i].simulationFrame;
				}

				var medianDelta = CalculateMedianDelta(frames);
				interpolation.latency = setup.autoLatencyFactor * medianDelta / 60f;
				world.EntityManager.SetComponentData(entity, interpolation);
				frames.Dispose();
			}
		}
	}

	[InternalBufferCapacity(5)]
	public struct Sample_Rotation_value : IBufferElementData
	{
		public ulong simulationFrame;
		public quaternion value;
	}

	public struct InterpolationComponent_Rotation_value : IComponentData
	{
		public InterpolationSetupID setupID;
		public float latency;
		public ulong previousFrame;
		public quaternion velocity;
	}
}


// ------------------ end of InterpolationSystems.cs -----------------
#endregion
