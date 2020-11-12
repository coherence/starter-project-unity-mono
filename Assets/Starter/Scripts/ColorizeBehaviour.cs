using UnityEngine;

public class ColorizeBehaviour : MonoBehaviour
{
    public Material material;
    public Renderer[] renderers;
    public int iii;
    public float fff;
    public bool bbb;
    public Vector3 target;
    public Quaternion whatever_works;
    public string name2;

    private void Start()
    {
        foreach (Renderer r in renderers)
        {
            r.material = material;
        }
    }
}
