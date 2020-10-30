using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    public float rotationSpeed = 200f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}