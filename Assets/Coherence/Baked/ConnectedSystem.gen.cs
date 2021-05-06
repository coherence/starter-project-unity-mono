


#region ConnectedSystem
// -----------------------------------
//  ConnectedSystem.cs
// -----------------------------------
			

namespace Coherence.Generated.Internal
{
	using global::Coherence.Generated;
	using Coherence.Replication.Client.Unity.Ecs;
	using Unity.Entities;
	using Unity.Transforms;

	public class ConnectedSystem : SystemBase
	{
		private EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;

		protected override void OnCreate()
		{
			base.OnCreate();
			// Find the ECB system once and store it for later usage
			m_EndSimulationEcbSystem = World
				.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			var ecbSync = m_EndSimulationEcbSystem.CreateCommandBuffer();
			var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
			BufferFromEntity<ChildBufferElement> buffersOfAllEntities
				= GetBufferFromEntity<ChildBufferElement>(false);

			var mapper = World.GetExistingSystem<SyncSendSystem>().Sender.Mapper;

			Entities
				.WithNone<ConnectedInitialized>()
				.ForEach((Entity entity, in ConnectedEntity connected) =>
				{
					var val = connected.value;
					var parent = mapper.ToUnityEntity(val);
					if (HasComponent<Mapped>(parent))
					{ // checksb for parent existence
						DynamicBuffer<ChildBufferElement> bufferOfOneEntity;
						if (!buffersOfAllEntities.HasComponent(parent))
						{
							bufferOfOneEntity = ecbSync.AddBuffer<ChildBufferElement>(parent);
						}
						else
						{
							bufferOfOneEntity = buffersOfAllEntities[parent];
						}

						var index = bufferOfOneEntity.Add(new ChildBufferElement { Value = entity });
						ecbSync.AddComponent(entity, new ConnectedInitialized { Index = index });
					}
				})
				.WithoutBurst()
				.Run();

			m_EndSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
		}
	}
}

// ------------------ end of ConnectedSystem.cs -----------------
#endregion
