using System;
using UnityEngine;

public class ShowNameAndState : MonoBehaviour
{
    public string PlayerName = "Bot";

    public int TestInt = 0;

    private StateType state = StateType.Default;
    private Material material;

    public int State
    {
        get => (int)state;
        set => state = (StateType)value;
    }

    public void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        RenderState();
    }

    public void Update()
    {
        RenderState();
    }

    private void RenderState()
    {
        switch (state)
        {
            case StateType.Default:
                material.color = Color.white;
                break;
            case StateType.Red:
                material.color = Color.red;
                break;
            case StateType.Yellow:
                material.color = Color.yellow;
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

    public enum StateType
    {
        Default = 0,
        Red,
        Yellow,
        Green,
        Blue
    }

    private void OnGUI()
    {
        Vector3 p = Camera.main.WorldToScreenPoint(transform.position);
        GUIStyle centeredStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter
        };
        centeredStyle.normal.textColor = Color.black;
        GUI.Label(new Rect(p.x - 100, Screen.height - p.y, 200, 50), $"{PlayerName} {TestInt}", centeredStyle);
    }
}