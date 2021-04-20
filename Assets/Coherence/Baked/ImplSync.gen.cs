


#region ImplSync
// -----------------------------------
//  ImplSync.cs
// -----------------------------------
			
namespace Coherence.Toolkit
{
	using Coherence.Replication.Client.Unity.Ecs;
	using static Coherence.Toolkit.CoherenceSync;
	using Unity.Collections;
	using Unity.Entities;
	using Unity.Transforms;
	using UnityEngine;
	using System;
	using System.Collections.Generic;
	using global::Coherence.Generated;
	using global::Coherence.Generated.Internal;
	using System.Reflection;

	public class CoherenceSyncImpl
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void OnRuntimeMethodLoad()
		{
			CoherenceSync.CreateECSRepresentation = CreateECSRepresentation;
			CoherenceSync.CreateBuiltinComponents = CreateBuiltinComponents;
			CoherenceSync.ReceiveGenericNetworkCommands = ReceiveGenericNetworkCommands;
			CoherenceSync.ReceiveAuthorityRequests = ReceiveAuthorityRequests;
			CoherenceSync.RequestAuthorityTransfer = RequestAuthorityTransfer;
			CoherenceSync.SendCommandImpl = SendCommandImpl;
			CoherenceSync.EcsEntityExistsImpl = EcsEntityExistsImpl;
			CoherenceSync.GetGenericScaleType = GetGenericScaleType;
			CoherenceSync.IsAuthorityRequestRejected = IsAuthorityRequestRejected;
			CoherenceSync.GetPersistenceUuid = GetPersistenceUuid;
		}

		private static void CreateECSRepresentation(CoherenceSync self)
		{
			self.LinkedEntity = self.entityManager.CreateEntity();
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

		private static void CreateBuiltinComponents(CoherenceSync coherenceSync)
		{
			var entityManager = coherenceSync.entityManager;
			var entity = coherenceSync.LinkedEntity;
			var transform = coherenceSync.transform;
			var authorityTransferType = coherenceSync.authorityTransferType;

			entityManager.AddComponentData<Translation>(entity, new Translation { Value = transform.position });
			entityManager.AddComponentData<Rotation>(entity, new Rotation { Value = transform.rotation });
			entityManager.AddComponentData<GenericScale>(entity, new GenericScale { Value = transform.localScale });

			entityManager.AddComponent<Simulated>(entity);
			entityManager.AddComponent<GenericCommand>(entity);

			if (coherenceSync.lifetimeType != LifetimeType.SessionBased)
			{
				entityManager.AddComponentData(entity, new Persistence()
				{
					uuid = coherenceSync.persistenceUUID,
					expiry = coherenceSync.GetPersistenceExpiryString()
				});
			}

			switch (authorityTransferType)
			{
				case AuthorityTransferType.Request:
				case AuthorityTransferType.Stealing:
					entityManager.AddComponent<AuthorityTransfer>(entity);
					break;
				case AuthorityTransferType.NotTransferable:
					break;
			}
		}

		private static GenericCommandRequest GenericCommandRequestFromObjects(string commandName, object[] args)
		{
			var (paramInt, paramFloat, paramBool, paramString, paramEntity) = TypeArrays.ExtractTypedArraysFromObjects(args);

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

				paramBool1 = paramBool[0],
				paramBool2 = paramBool[1],
				paramBool3 = paramBool[2],
				paramBool4 = paramBool[3],

                paramEntity1 = CoherenceMonoBridge.UnknownObjectToEntityId(paramEntity[0]),
                paramEntity2 = CoherenceMonoBridge.UnknownObjectToEntityId(paramEntity[1]),
                paramEntity3 = CoherenceMonoBridge.UnknownObjectToEntityId(paramEntity[2]),
                paramEntity4 = CoherenceMonoBridge.UnknownObjectToEntityId(paramEntity[3]),

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

		private static string GetPersistenceUuid(CoherenceSync self)
		{
			if (self.entityManager.HasComponent<Persistence>(self.entity))
			{
				var ret = self.entityManager.GetComponentData<Persistence>(self.entity);
				return ret.uuid.ToString();
			}

			return null;
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

		private static bool IsAuthorityRequestRejected(CoherenceSync self)
		{
			if (self.entityManager.HasComponent<TransferAction>(self.entity))
			{
				var buffer = self.entityManager.GetBuffer<TransferAction>(self.entity);
				if (buffer.Length == 0)
				{
					return false;
				}

				var ret = buffer[0];
				return !ret.accepted;
			}

			return false;
		}

		private static void ReceiveAuthorityRequests(CoherenceSync self)
		{
			if (self.entity == Entity.Null) return;

			if (!self.entityManager.HasComponent<AuthorityTransfer>(self.entity)) return;

			var buffer = self.entityManager.GetBuffer<AuthorityTransfer>(self.entity);

			bool transfer = !buffer.IsEmpty;
			TransferAction action = default;

			foreach (var req in buffer)
			{
				bool allowed = false;
				switch (self.authorityTransferType)
				{
					case AuthorityTransferType.NotTransferable:
						break;
					case AuthorityTransferType.Request:
						allowed = self.AllowAuthorityTransfer();
						break;
					case AuthorityTransferType.Stealing:
						allowed = true;
						break;
				}

				if (self.isOrphaned) allowed = true; // auto-allow orphaned entities

				if (self.simulationType == SimulationType.SimulationServerWithClientInput) allowed = true; // have to support it for client input

				action = new TransferAction
				{
					participant = req.participant,
					accepted = allowed
				};

				if (allowed)
				{
					break; // Only the first request is given the authority OK
				}
			}

			buffer.Clear();

			if (transfer)
			{
				if (!self.entityManager.HasComponent<TransferAction>(self.entity))
				{
					self.entityManager.AddBuffer<TransferAction>(self.entity);
				}

				var authorityBuffer = self.entityManager.GetBuffer<TransferAction>(self.entity);
				authorityBuffer.Add(action);
			}
		}

		private static void RequestAuthorityTransfer(CoherenceSync self)
		{
			var connectionId =
			World.DefaultGameObjectInjectionWorld.GetExistingSystem<NetworkSystem>().Connector.ConnectionId;
			var buffer = self.entityManager.GetBuffer<AuthorityTransferRequest>(self.entity);
			buffer.Add(new AuthorityTransferRequest {participant = connectionId});
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

				ParamBool1 = command.paramBool1,
				ParamBool2 = command.paramBool2,
				ParamBool3 = command.paramBool3,
				ParamBool4 = command.paramBool4,

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
