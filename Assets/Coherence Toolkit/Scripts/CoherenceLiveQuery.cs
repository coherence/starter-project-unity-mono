namespace Coherence.MonoBridge
{
    using Coherence.Replication.Client.Unity.Ecs;
    using Unity.Entities;
    using UnityEngine;
    using Network = Network;

#if COHERENCE_TOOLKIT
    using Coherence.Generated.FirstProject; // TODO: Use a better namespace name
#endif

    public class CoherenceLiveQuery : MonoBehaviour
    {
#if COHERENCE_TOOLKIT
        public float radius = 50f;
        public Color gizmoColor = Color.yellow;

        private void Awake()
        {
            Network.OnConnected += OnConnected;
        }

        private void OnConnected()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // Create live query
            Entity liveQuery = entityManager.CreateEntity(
                typeof(Simulated),
                typeof(WorldPositionQuery),
                typeof(SessionBased));

            entityManager.SetComponentData(liveQuery, new WorldPositionQuery()
            {
                radius = radius
            });
        }

        private void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
#endif
    }
}
