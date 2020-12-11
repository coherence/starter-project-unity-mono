namespace Coherence.MonoBridge
{
    using Unity.Entities;
    using UnityEngine;
    using System;
    using Network = Network;

    public class CoherenceLiveQuery : MonoBehaviour
    {
        public static Action<EntityManager, float> CreateLiveQuery;

        public float radius = 50f;
        public Color gizmoColor = Color.yellow;

        private void Awake()
        {
            Network.OnConnected += OnConnected;
        }

        private void OnConnected()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            CreateLiveQuery(entityManager, radius);
        }

        private void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
