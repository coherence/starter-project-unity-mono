using UnityEngine;

public class ColorizeBehaviour : MonoBehaviour
{
    public Material material;
    public Renderer[] renderers;

    private void Start()
    {
        foreach (Renderer r in renderers)
        {
            r.material = material;
        }
    }
}
