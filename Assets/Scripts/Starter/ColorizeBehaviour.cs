using UnityEngine;

public class ColorizeBehaviour : MonoBehaviour
{
    public Material material;
    public new Renderer renderer;

    private void Start()
    {
        renderer.material = material;
    }
}
