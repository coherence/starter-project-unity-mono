using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSpawner : MonoBehaviour
{
    public Transform prefab;

    public Transform delete;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(prefab);
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            Destroy(delete.gameObject);
        }
            
    }
}
