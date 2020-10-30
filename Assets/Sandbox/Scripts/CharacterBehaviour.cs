using Coherence.MonoBridge;
using System;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float movementSpeed = 3f;
    private ShowNameAndState showNameAndState;
    private DebugPanel debugPanel;

    protected virtual void Awake()
    {
        showNameAndState = GetComponent<ShowNameAndState>();
        DebugPanel dpo = FindObjectOfType<DebugPanel>();

        if (dpo)
        {
            debugPanel = dpo;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        CoherenceSync coherenceSyncOther = other.gameObject.GetComponent<CoherenceSync>();
        CoherenceSync coherenceSync = gameObject.GetComponent<CoherenceSync>();
        if (coherenceSync != null)
        {
            string stringToSend = string.Empty;

            if (debugPanel)
            {
                stringToSend = debugPanel.stringToSend.text;
                debugPanel.AddLog($"[S] {gameObject.name} [{stringToSend}]");

                //Debug.Log(debugPanel.log.text + "== text");
            }

            coherenceSyncOther.SendNetworkCommand(coherenceSync, stringToSend, 8, 7, 6, 5, 100, 100, 100, 100,
                    null);
        }
    }

    public void NetworkCommand(object args)
    {
        if (debugPanel)
        {
            debugPanel.AddLog($"[R] [{args}]");
        }
    }

    protected void CycleState()
    {
        Array values = Enum.GetValues(typeof(ShowNameAndState.StateType));
        int maxValue = (int)values.GetValue(values.Length - 1);
        int cst = showNameAndState.State;
        cst += 1;
        if (cst > maxValue)
        {
            cst = 0;
        }

        showNameAndState.State = cst;
    }
}