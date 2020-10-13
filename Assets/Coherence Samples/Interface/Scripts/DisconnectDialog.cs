namespace Coherence.Samples
{
    using Unity.Entities;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using NetworkSystem = Replication.Client.Unity.Ecs.NetworkSystem;

    public class DisconnectDialog : Dialog
    {
        public Button disconnectButton;
        public UnityEvent onConnect;
        public UnityEvent onDisconnect;

        private NetworkSystem networkSystem;

        protected override void Awake()
        {
            base.Awake();

            disconnectButton.onClick.AddListener(OnDisconnectClicked);

            Network.OnConnected += OnConnected;
            Network.OnDisconnected += OnDisconnected;

            Hide();

            networkSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<NetworkSystem>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Network.OnConnected -= OnConnected;
            Network.OnDisconnected -= OnDisconnected;
        }

        private void OnDisconnectClicked()
        {
            networkSystem.Disconnect();
        }

        private void OnConnected()
        {
            onConnect.Invoke();
        }

        private void OnDisconnected()
        {
            onDisconnect.Invoke();
        }
    }
}