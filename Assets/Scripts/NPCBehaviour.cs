using System;
using System.Collections;
using System.Collections.Generic;
using Coherence.MonoBridge;
using Coherence.Samples;
using UnityEngine;
using UnityEngine.UIElements;
using Network = Coherence.Network;
using Random = System.Random;

public class NPCBehaviour : MonoBehaviour
{
    private Vector3 targetPos;

    private Random random;
    
    public float movementSpeed = 3f;
    
    private ShowNameAndState showNameAndState;

    public void Awake()
    {
        random = new Random((int)(Time.time * 1000f));
        showNameAndState = GetComponent<ShowNameAndState>();
        StartCoroutine(AICoroutine());
    }

    IEnumerator AICoroutine()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(1+ (float)random.NextDouble()*2f);

            const float amplitude = 5f;
            float dx = amplitude * (float)(random.NextDouble() - 0.5f);
            float dz = amplitude * (float)(random.NextDouble() - 0.5f);

            targetPos = new Vector3(dx, 0, dz);
            
            CycleState();
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!isActiveAndEnabled) return;
        
        Debug.Log("I've collided with " + other.gameObject);
        var coherenceSync = other.gameObject.GetComponent<CoherenceSync>();
        if (coherenceSync != null)
        {
            coherenceSync.SendNetworkCommand("Collided with", gameObject.name + " " + ConnectDialog.GetPlayerName());
        }
    }

    public void NetworkCommand(object args)
    {
        CoherenceSync.NetworkCommandArgs nargs = (CoherenceSync.NetworkCommandArgs) args;
        Debug.Log(gameObject.name + " received command: " + args.ToString());
    }
    
    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * movementSpeed);
    }

    private void CycleState()
    {
        var values = Enum.GetValues(typeof(ShowNameAndState.StateType));
        var maxValue = (int) values.GetValue(values.Length - 1);
        int cst = (int) showNameAndState.State;
        cst += 1;
        if (cst > maxValue) cst = 0;
        showNameAndState.State = cst;
    }
}
