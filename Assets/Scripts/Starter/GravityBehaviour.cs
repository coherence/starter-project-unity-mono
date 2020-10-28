using UnityEngine;

public class GravityBehaviour : MonoBehaviour
{
    public CharacterController characterController;
    public float gravity;

    private void Reset()
    {
        characterController = GetComponent<CharacterController>();
        gravity = -9.81f;
    }

    private void Update()
    {
        Vector3 move = Vector3.up * gravity;

        if (characterController.isGrounded && move.y < 0f)
        {
            move.y = 0f;
        }

        _ = characterController.Move(move * Time.smoothDeltaTime);
    }
}
