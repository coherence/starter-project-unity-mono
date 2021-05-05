using UnityEngine;
using Coherence.Toolkit;

public class Controller : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;

    [Header("Controls")]
    public string xAxis;
    public string yAxis;
    public KeyCode jumpKey;
    public KeyCode shootKey;

    [Header("Movement")]
    public bool useTankControls;
    public float moveSpeed;
    public float rotationSpeed;

    [Header("Jump")]
    public bool canJump;
    [Range(0f, 1f)]
    public float airborneSpeedModifier;
    public float jumpHeight;

    [Header("Gun")]
    public bool useGun;
    public Transform gun;
    public Transform gunPivot;
    public Bullet bulletPrefab;

    private Vector3 velocity;
    private CoherenceSync coherenceSync;

    private void OnValidate()
    {
        if (gun)
        {
            gun.gameObject.SetActive(useGun);
        }
    }

    private void Reset()
    {
        controller = GetComponent<CharacterController>();
        moveSpeed = 2f;
        rotationSpeed = 90f;
        airborneSpeedModifier = 1f;
        jumpHeight = 1f;
        xAxis = "Horizontal";
        yAxis = "Vertical";
        jumpKey = KeyCode.Space;
        shootKey = KeyCode.J;
    }

    private void Awake()
    {
        coherenceSync = GetComponent<CoherenceSync>();
    }

    public void OnRemoteInstantiate(CoherenceSync sync)
    {
        name = name + "_NETWORKED";
    }

    public void Blaj(int frame)
    {
        Debug.Log($"Got command 'Blaj' with frame {frame}.");
    }

    private void Update()
    {
        var sender = coherenceSync;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var controller in FindObjectsOfType<Controller>())
            {
                var sync = controller.coherenceSync;
                Debug.Log($"Sending command 'Controller.Blaj' to '{sync.name}'");
                sync.SendCommand(sender, "Controller.Blaj", Time.frameCount);
            }
        }

        if (useGun != gun.gameObject.activeSelf)
        {
            gun.gameObject.SetActive(useGun);
        }

        bool grounded = controller.isGrounded;
        if (grounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        float h = Input.GetAxis(xAxis);
        float v = Input.GetAxis(yAxis);

        Vector3 motion;

        if (useTankControls)
        {
            transform.localRotation *= Quaternion.AngleAxis(rotationSpeed * h * Time.smoothDeltaTime, Vector3.up);
            motion = v * transform.forward * moveSpeed;
        }
        else
        {
            Vector3 move = new Vector3(h, 0f, v);
            move = Camera.main.transform.TransformDirection(move);
            move.y = 0f;

            motion = move * moveSpeed;
        }

        motion *= grounded ? 1f : airborneSpeedModifier;

        if (canJump && grounded && Input.GetKeyDown(jumpKey))
        {
            // https://medium.com/@brazmogu/physics-for-game-dev-a-platformer-physics-cheatsheet-f34b09064558
            velocity -= Physics.gravity.normalized * Mathf.Sqrt(Mathf.Abs(2f * jumpHeight * Physics.gravity.magnitude));
        }

        velocity += Physics.gravity * Time.smoothDeltaTime;

        _ = controller.Move((velocity + motion) * Time.smoothDeltaTime);

        if (useGun && Input.GetKeyDown(shootKey))
        {
            Bullet bullet = Instantiate(bulletPrefab, gunPivot.position, gunPivot.rotation);
            bullet.SetCoherenceSender(coherenceSync);
        }
    }

    public void Hit()
    {
        velocity -= Physics.gravity.normalized * Mathf.Sqrt(Mathf.Abs(2f * 8f * Physics.gravity.magnitude));
    }
}
