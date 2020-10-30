using UnityEngine;

public class MonoSpawner : MonoBehaviour
{
    public Transform prefab;
    public Transform delete;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _ = Instantiate(prefab);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Destroy(delete.gameObject);
        }

    }
}