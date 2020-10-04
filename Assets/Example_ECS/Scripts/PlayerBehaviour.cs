﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public enum State
    {
        Default = 0,
        Red,
        Yellow,
        Green,
        Blue
    }

    public float movementSpeed = 3f;
    
    public State state = State.Default;

    private Material material;

    public void Awake()
    {
        var renderer = GetComponent<Renderer>();
        material = renderer.material;
        RenderState();
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

    private void CycleState()
    {
        var values = Enum.GetValues(typeof(State));
        var maxValue = (int) values.GetValue(values.Length - 1);
        int cst = (int) state;
        cst += 1;
        if (cst > maxValue) cst = 0;
        state = (State) cst;

        RenderState();
    }

    private void RenderState()
    {
        switch (state)
        {
            case State.Default:
                material.color = Color.white;
                break;
            case State.Red:
                material.color = Color.red;
                break;
            case State.Yellow:
                material.color = Color.yellow;
                break;
            case State.Green:
                material.color = Color.green;
                break;
            case State.Blue:
                material.color = Color.blue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}