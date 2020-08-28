using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Coherence.Replication.Client.Unity.Ecs;
using Coherence.Replication.Client.Connection;
using Coherence.Generated.FirstProject;
using Coherence.Sdk.Unity;

[AlwaysUpdateSystem]
class JoinSystem : SystemBase
{
    CoherenceRuntimeSystem coherenceRuntime;
    Entity localUserAuthority;

    protected override void OnStartRunning()
    {
        coherenceRuntime = World.GetOrCreateSystem<CoherenceRuntimeSystem>();
        coherenceRuntime.Connect("127.0.0.1:12345", ConnectionType.Client);
    }

    protected override void OnUpdate()
    {
        // When we are connected we try to get hold of our LocalUser entity,
        // which allows us to create a WorldPositionQuery and Player.
        if (coherenceRuntime.IsConnected && localUserAuthority.Equals(Entity.Null))
        {
            var localUserQuery = EntityManager.CreateEntityQuery(typeof(LocalUser));

            if (localUserQuery.CalculateEntityCount() > 0)
            {
                localUserAuthority = localUserQuery.GetSingletonEntity();
                CreateWorldPositionQuery(localUserAuthority);
                CreateLocalPlayer(localUserAuthority);
            }
        }

        // Detect remotely simulated Player entities and instantiate a proper Prefab for them.
        Entities.WithNone<RenderMesh>().ForEach((Entity networkEntity, in Player player) =>
        {
            var otherPlayerPrefabEntity = PrefabHolder.Get().otherPlayerPrefabEntity;
            var newPlayerEntity = World.EntityManager.Instantiate(otherPlayerPrefabEntity);
            CoherenceUtil.ReplaceEntity(EntityManager, networkEntity, newPlayerEntity);
        }).WithStructuralChanges().WithoutBurst().Run();
    }

    void CreateWorldPositionQuery(Entity authority)
    {
        var worldQueryEntity = EntityManager.CreateEntity();

        EntityManager.AddComponentData(worldQueryEntity, new CoherenceSimulateComponent
        {
            Authority = authority
        });

        EntityManager.AddComponentData(worldQueryEntity, new WorldPositionQuery
        {
            position = new float3(0, 0, 0),
        });
    }

    private Entity CreateLocalPlayer(Entity authority)
    {
        var playerPrefabEntity = PrefabHolder.Get().playerPrefabEntity;
        var newPlayerEntity = World.EntityManager.Instantiate(playerPrefabEntity);

        // The empty 'Player' component acts as a tag to query for.
        EntityManager.AddComponentData(newPlayerEntity, new Player()
        {

        });

        // This component makes us responsible for the simulation of the Entity.
        EntityManager.AddComponentData(newPlayerEntity, new CoherenceSimulateComponent
        {
            Authority = authority,
        });

        // This component makes the Entity disappear if we log out or disconnect.
        EntityManager.AddComponentData(newPlayerEntity, new CoherenceSessionComponent
        {

        });

        // This component makes our keyboard input affect the Entity.
        EntityManager.AddComponentData(newPlayerEntity, new Input
        {
            Value = new float2()
        });

        // Set a random starting position.
        var range = 4.0f;
        EntityManager.AddComponentData(newPlayerEntity, new Translation
        {
            Value = new float3(UnityEngine.Random.Range(-range, range),
                               0.25f,
                               UnityEngine.Random.Range(-range, range))
        });

        return newPlayerEntity;
    }
}
