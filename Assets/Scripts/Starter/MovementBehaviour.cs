using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;

    private void Reset()
    {
        characterController = GetComponent<CharacterController>();
        speed = 2f;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v);
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0f;

        _ = characterController.Move(move * speed * Time.smoothDeltaTime);
    }
}
