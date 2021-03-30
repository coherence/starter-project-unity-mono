using System.Collections;
using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    static Cube previousCube;

    CoherenceSync sync;
    public GameObject friend;
    Text text;

    public string s;
    public int i;

    void Start()
    {
        sync = GetComponent<CoherenceSync>();
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        var who = (friend == null) ? "no one" : friend.name;
        text.text = $"I'm friends with {who}";
    }

    private void OnGUI()
    {
        //GUI.Label()
    }

    private void OnMouseDown()
    {
        //Debug.Log($"User clicked {name} (simulated = {sync.isSimulated})");

        if (sync.isSimulated)
        {
            if (previousCube == null)
            {
                previousCube = this;
            }
            else
            {
                Debug.Log($"Set friend of {previousCube} to {name}");
                previousCube.friend = this.gameObject;
                previousCube = null;
            }
        }
    }
}
