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

    static int count = 0;

    void Awake()
    {
        sync = GetComponent<CoherenceSync>();
        text = GetComponentInChildren<Text>();
    }

    public void SpawnedOverNetwork(CoherenceSync sync)
    {
        Debug.Log($"Spawned over network {sync.name}");
        name = $"NetworkedCube{count++}";
        text.color = Color.blue;
        transform.position += new Vector3(3f, 0f, 0f);
    }

    void Update()
    {
        var who = (friend == null) ? "no one" : friend.name;
        text.text = $"I'm friends with {who}";

        if(Input.GetKeyDown(KeyCode.Alpha1) && name == "Cube1")
        {
            Click();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && name == "Cube2")
        {
            Click();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && name == "Cube3")
        {
            Click();
        }
    }

    private void OnGUI()
    {
        //GUI.Label()
    }

    private void Click()
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
