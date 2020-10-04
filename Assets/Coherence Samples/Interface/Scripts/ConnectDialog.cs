namespace Coherence.Samples
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using ConnectionType = Replication.Client.Unity.Ecs.ConnectionType;

    public class ConnectDialog : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public InputField nameInput;
        public InputField serverInput;
        public Toggle clientToggle;
        public Toggle simulationToggle;
        public Button connectButton;

        public UnityEvent onAttemptConnection;
        public UnityEvent onConnect;
        public UnityEvent onDisconnect;

        public string PlayerName => nameInput.text;

        private void Awake()
        {
            nameInput.text = System.Environment.UserName;
            connectButton.onClick.AddListener(OnConnectClicked);
            nameInput.onEndEdit.AddListener(OnInputEnd);
            serverInput.onEndEdit.AddListener(OnInputEnd);

            Coherence.Network.OnConnected += OnConnected;
            Coherence.Network.OnDisconnected += OnDisconnected;
        }

        private void OnDestroy()
        {
            Coherence.Network.OnConnected -= OnConnected;
            Coherence.Network.OnDisconnected -= OnDisconnected;
        }

        private void OnConnectClicked()
        {
            canvasGroup.interactable = false;
            Coherence.Network.Connect(serverInput.text, simulationToggle.isOn ? ConnectionType.Simulator : ConnectionType.Client);
            onAttemptConnection.Invoke();
        }

        private void OnConnected()
        {
            canvasGroup.interactable = true;
            onConnect.Invoke();
        }

        private void OnDisconnected()
        {
            canvasGroup.interactable = true;
            onDisconnect.Invoke();
        }

        private void OnInputEnd(string text)
        {
            connectButton.interactable = !string.IsNullOrEmpty(nameInput.text) && !string.IsNullOrEmpty(serverInput.text);
        }
    }
}

