namespace Coherence.Samples
{
    using UnityEngine;
    using UnityEngine.UI;
    using NetworkSystem = Replication.Client.Unity.Ecs.NetworkSystem;
    using World = Unity.Entities.World;

    public class Watermark : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public Text infoLabel;

        private NetworkSystem networkSystem;

        private void Awake()
        {
            networkSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<NetworkSystem>();
        }

        private void Update()
        {
            infoLabel.text = $"SDK: {VersionInfo.sdk}\n{networkSystem.State}\n{networkSystem.Hostname}";
        }
    }
}