


#region ImplSync
// -----------------------------------
//  ImplSync.cs
// -----------------------------------
			
namespace Coherence.MonoBridge
{
	using Coherence.Replication.Client.Unity.Ecs;
	using static Coherence.MonoBridge.CoherenceSync;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Transforms;
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using global::Coherence.Generated.FirstProject;
	using System.Reflection;

	public class CoherenceSyncImpl
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			 CoherenceSync.CreateECSRepresentation = CreateECSRepresentation;
			 CoherenceSync.CreateBuiltinComponents = CreateBuiltinComponents;
			 CoherenceSync.ReceiveGenericNetworkCommands = ReceiveGenericNetworkCommands;
			 CoherenceSync.SendCommandImpl = SendCommandImpl;
			 CoherenceSync.EcsEntityExistsImpl = EcsEntityExistsImpl;
			 CoherenceSync.GetGenericScaleType = GetGenericScaleType;
		}

		private static void CreateECSRepresentation(CoherenceSync self)
		{
			self.entity = self.entityManager.CreateEntity();
			self.entityManager.AddComponent<GenericPrefabReference>(self.entity);

			if (self.usingReflection)
			{
				self.InitializeComponents();
			}

			self.entityManager.SetComponentData(self.entity, new GenericPrefabReference
			{
				prefab = new FixedString64(self.remoteVersionPrefabName)
			});
		}

		private static void CreateBuiltinComponents(EntityManager entityManager, Entity entity, Transform transform)
		{
			entityManager.AddComponentData<Translation>(entity, new Translation { Value = transform.position });
			entityManager.AddComponentData<Rotation>(entity, new Rotation { Value = transform.rotation });
			entityManager.AddComponentData<GenericScale>(entity, new GenericScale { Value = transform.localScale });

			entityManager.AddComponent<SessionBased>(entity);
			entityManager.AddComponent<Simulated>(entity);
			entityManager.AddComponent<GenericCommand>(entity);
		}

		private static GenericCommandRequest GenericCommandRequestFromObjects(string commandName, object[] args)
		{
			var (paramInt, paramFloat, paramString) = TypeHelpers.ExtractTypedArraysFromObjects(args);

			var genericCommandRequest = new GenericCommandRequest
			{
				name = String.IsNullOrEmpty(commandName) ? "" : commandName,

				paramInt1 = paramInt[0],
				paramInt2 = paramInt[1],
				paramInt3 = paramInt[2],
				paramInt4 = paramInt[3],

				paramFloat1 = paramFloat[0],
				paramFloat2 = paramFloat[1],
				paramFloat3 = paramFloat[2],
				paramFloat4 = paramFloat[3],

				paramString = String.IsNullOrEmpty(paramString[0]) ? "" : paramString[0],
			};

			return genericCommandRequest;
		}

		private static void SendCommandImpl(CoherenceSync self, CoherenceSync sender,
											string commandName, params object[] args)
		{
			if (self.entityManager.HasComponent<GenericCommandRequest>(self.entity))
			{
				// Remote Entity
				if(self.usingReflection)
				{
					var commandRequest = GenericCommandRequestFromObjects(commandName, args);
					var cmdRequestBuffer = self.entityManager.GetBuffer<GenericCommandRequest>(self.entity);
					_ = cmdRequestBuffer.Add(commandRequest);
				}
				else
				{
					self.commandRequestDelegates[commandName](args);
				}
			}
			else {
				// Local Entity
				var command = GenericNetworkCommandArgs.FromObjects(commandName, args);
				self.ProcessGenericNetworkCommand(commandName, command);
			}
		}

		private static void ReceiveGenericNetworkCommands(CoherenceSync self)
		{
			if (self.entity == Entity.Null) return;

			var buffer = self.entityManager.GetBuffer<GenericCommand>(self.entity);

			foreach (var genericCommand in buffer)
			{
				var commandName = genericCommand.name.ToString();
				self.ProcessGenericNetworkCommand(commandName, FromGenericCommand(genericCommand));
			}

			buffer.Clear();
		}

		private static GenericNetworkCommandArgs FromGenericCommand(GenericCommand command)
		{
			return new GenericNetworkCommandArgs
			{
				Name = command.name.ToString(),

				ParamInt1 = command.paramInt1,
				ParamInt2 = command.paramInt2,
				ParamInt3 = command.paramInt3,
				ParamInt4 = command.paramInt4,

				ParamFloat1 = command.paramFloat1,
				ParamFloat2 = command.paramFloat2,
				ParamFloat3 = command.paramFloat3,
				ParamFloat4 = command.paramFloat4,

				ParamString = command.paramString.ToString(),
			};
		}

		private static bool EcsEntityExistsImpl(EntityManager entityManager, Entity entity)
		{
			return entity != Entity.Null && entityManager.HasComponent<GenericPrefabReference>(entity);
		}

		private static Type GetGenericScaleType() {
			return typeof(GenericScale);
		}
	}
}

// ------------------ end of ImplSync.cs -----------------
#endregion
