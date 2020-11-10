using UnityEngine;

public class ColorizeBehaviour : MonoBehaviour
{
    public Material material;
    public Renderer[] renderers;
    public int bleh;

    private void Start()
    {
        foreach (Renderer r in renderers)
        {
            r.material = material;
        }
    }
}
