﻿using System;
using System.Collections;
using System.Collections.Generic;
using Coherence.MonoBridge;
using Coherence.Samples;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed = 3f;
    private ShowNameAndState showNameAndState;

    void Awake()
    {
        showNameAndState = GetComponent<ShowNameAndState>();
    }
    
    public void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        const float threshold = 0.001f;

        float dx = UnityEngine.Input.GetAxis("Horizontal");
        float dy = UnityEngine.Input.GetAxis("Vertical");

        if (Mathf.Abs(dx) > threshold)
        {
            transform.position += Vector3.right * (movementSpeed * dx * Time.deltaTime);
        }

        if (Mathf.Abs(dy) > threshold)
        {
            transform.position += Vector3.forward * (movementSpeed * dy * Time.deltaTime);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
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
