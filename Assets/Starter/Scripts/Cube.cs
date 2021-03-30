using System.Collections;
using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;

public class Cube : MonoBehaviour
{
    static Cube previousCube;

    CoherenceSync sync;
    public GameObject friend;

    void Start()
    {
        sync = GetComponent<CoherenceSync>();
    }

    void Update()
    {
        
    }

    private void OnGUI()
    {
        //GUI.Label()
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log($"User clicked {name} (simulated = {sync.isSimulated})");

    //    if(sync.isSimulated)
    //    {
    //        if(previousCube == null)
    //        {
    //            previousCube = this;
    //        }
    //        else
    //        {
    //            friend = previousCube.sync;
    //            previousCube = null;
    //        }
    //    }        
    //}
}
