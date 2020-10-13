namespace Coherence.Samples
{
    using UnityEngine.Events;
    using UnityEngine.UI;
    using ConnectionType = Replication.Client.Unity.Ecs.ConnectionType;

    public class ConnectDialog : Dialog
    {
        public InputField nameInput;
        public InputField serverInput;
        public Toggle clientToggle;
        public Toggle simulationToggle;
        public Button connectButton;

        public UnityEvent onAttemptConnection;
        public UnityEvent onInvalidConnect;
        public UnityEvent onConnect;
        public UnityEvent onDisconnect;

        public string PlayerName => nameInput.text;

        protected override void Awake()
        {
            base.Awake();

            nameInput.text = System.Environment.UserName;
            connectButton.onClick.AddListener(OnConnectClicked);
            nameInput.onValueChanged.AddListener(OnValueChanged);
            serverInput.onValueChanged.AddListener(OnValueChanged);
            serverInput.text = Server.IP;

            Network.OnConnected += OnConnected;
            Network.OnDisconnected += OnDisconnected;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Network.OnConnected -= OnConnected;
            Network.OnDisconnected -= OnDisconnected;
        }

        private void OnConnected()
        {
            onConnect.Invoke();
        }

        private void OnDisconnected()
        {
            onDisconnect.Invoke();
        }

        private void OnConnectClicked()
        {
            if (Network.Connect(serverInput.text, simulationToggle.isOn ? ConnectionType.Simulator : ConnectionType.Client))
            {
                canvasGroup.interactable = false;
                onAttemptConnection.Invoke();
            }
            else
            {
                onInvalidConnect.Invoke();
            }
        }

        private void OnValueChanged(string text)
        {
            connectButton.interactable = !string.IsNullOrEmpty(nameInput.text) && !string.IsNullOrEmpty(serverInput.text);
        }
    }
}