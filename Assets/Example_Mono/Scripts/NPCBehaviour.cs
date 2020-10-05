using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class NPCBehaviour : MonoBehaviour
{
    public string PlayerName = "Bot";

    private Vector3 targetPos;

    private Random random;
    
    public enum StateType
    {
        Default = 0,
        Red,
        Yellow,
        Green,
        Blue
    }

    void OnGUI()
    {
        var p = Camera.main.WorldToScreenPoint(transform.position);
        var centeredStyle = new GUIStyle(GUI.skin.label);
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        centeredStyle.normal.textColor = Color.black;
        GUI.Label(new Rect(p.x - 100, Screen.height-p.y, 200, 50), PlayerName, centeredStyle);
    }

    public float movementSpeed = 3f;
    
    private StateType state = StateType.Default;

    public int State
    {
        get
        {
            return (int) state;
        }
        set
        {
            state = (StateType) value;
        }
    }

    private Material material;

    public void Awake()
    {
        random = new Random((int)(Time.time * 1000f));
        var renderer = GetComponent<Renderer>();
        material = renderer.material;
        RenderState();
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
    
    public void Update()
    {
        RenderState();
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * movementSpeed);
    }

    private void CycleState()
    {
        var values = Enum.GetValues(typeof(StateType));
        var maxValue = (int) values.GetValue(values.Length - 1);
        int cst = (int) state;
        cst += 1;
        if (cst > maxValue) cst = 0;
        state = (StateType) cst;
    }

    private void RenderState()
    {
        switch (state)
        {
            case StateType.Default:
                material.color = Color.grey;
                break;
            case StateType.Red:
                material.color = Color.cyan;
                break;
            case StateType.Yellow:
                material.color = Color.magenta;
                break;
            case StateType.Green:
                material.color = Color.green;
                break;
            case StateType.Blue:
                material.color = Color.blue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
