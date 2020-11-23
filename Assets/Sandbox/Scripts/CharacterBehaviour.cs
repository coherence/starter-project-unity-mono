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
                debugPanel.AddLog($"[S] {gameObject.name} [{stringToSend} ... <numbers>]");

                //Debug.Log(debugPanel.log.text + "== text");
            }

            // This creates a full command with a scrambled order of its arguments
            coherenceSyncOther.SendCommand(coherenceSync, "CharacterBehaviour.LogToPanel",
                                           stringToSend, 1.0f, 10, 20, 30, 2.0f, 40, 3.0f, 4.0f);
        }
    }

    public void LogToPanel(string message, float f0, int i0, int i1, int i2, float f1, int i3, float f2, float f3)
    {
        if (debugPanel)
        {
            debugPanel.AddLog($"[R] [{message}, {f0}, {i0}, {i1}, {i2}, {f1}, {i3}, {f2}, {f3}]");
        }
    }

    public void Hit()
    {
        debugPanel.AddLog($"{name} was hit!");
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
