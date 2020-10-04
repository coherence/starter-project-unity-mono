using Coherence.Generated.FirstProject;
using Coherence.Replication.Client.Unity.Ecs;
using Unity.Entities;
using UnityEngine;
using Coherence.Generated.Internal.Schema;

namespace Coherence.MonoBridge
{
    public class CoherenceLiveQuery : MonoBehaviour
    {
        public float radius = 50f;
        public Color gizmoColor = Color.yellow;
        
        public void Awake()
        {
            Network.OnConnected += OnConnected;
        }

        private void OnConnected()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            // Create live query
            Entity liveQuery = entityManager.CreateEntity(
                typeof(CoherenceSimulateComponent),
                typeof(WorldPositionQuery),
                typeof(CoherenceSessionComponent));

            entityManager.SetComponentData(liveQuery, new WorldPositionQuery()
            {
                radius = this.radius
            });
        }
        
        private void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}