using System.Security.Permissions;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{
    public void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        const float threshold = 0.001f;

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (Mathf.Abs(dx) > threshold)
        {
            transform.position += Vector3.right * (movementSpeed * dx * Time.deltaTime);
        }

        if (Mathf.Abs(dy) > threshold)
        {
            transform.position += Vector3.forward * (movementSpeed * dy * Time.deltaTime);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            CycleState();
        }
    }
}
