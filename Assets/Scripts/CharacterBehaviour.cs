using System;
using Coherence.MonoBridge;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float movementSpeed = 3f;
    private ShowNameAndState showNameAndState;
    private DebugPanel debugPanel;
    
    protected virtual void Awake()
    {
        showNameAndState = GetComponent<ShowNameAndState>();
        var dpo = FindObjectOfType<DebugPanel>();

        if (dpo)
        {
            debugPanel = dpo;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!isActiveAndEnabled) return;
        var coherenceSyncOther = other.gameObject.GetComponent<CoherenceSync>();
        var coherenceSync = gameObject.GetComponent<CoherenceSync>();
        if (coherenceSync != null)
        {
            var stringToSend = "";

            if (debugPanel)
            {
                stringToSend = debugPanel.stringToSend.text;
                debugPanel.log.text = $"[S] {gameObject.name} [{stringToSend}]\r\n" + debugPanel.log.text ;
                
                Debug.Log(debugPanel.log.text + "== text");
            }
            
            coherenceSyncOther.SendNetworkCommand(coherenceSync, stringToSend, 8, 7, 6, 5, 100, 100, 100, 100, null);
        }
    }
    
    public void NetworkCommand(object args)
    {
        CoherenceSync.NetworkCommandArgs nargs = (CoherenceSync.NetworkCommandArgs) args;
        if (debugPanel)
        {
            debugPanel.log.text = $"[R] [{args.ToString()}]\r\n" + debugPanel.log.text;
        }
    }
    
    protected void CycleState()
    {
        var values = Enum.GetValues(typeof(ShowNameAndState.StateType));
        var maxValue = (int) values.GetValue(values.Length - 1);
        int cst = (int) showNameAndState.State;
        cst += 1;
        if (cst > maxValue) cst = 0;
        showNameAndState.State = cst;
    }
}
