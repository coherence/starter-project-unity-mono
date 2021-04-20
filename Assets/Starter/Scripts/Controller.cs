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

    public GameObject otherPlayer;
    public Transform otherPlayerTransform;
    public CoherenceSync otherPlayerSync;

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
        //otherPlayer = null;
        //otherPlayerTransform = null;
        //otherPlayerSync = null;
    }

    public void Foo(GameObject g)
    {
        Debug.Log($"Got command Foo, g = {g}");
    }

    public void Boo()
    {
        Debug.Log($"Got command Boo");
    }

    private void Update()
    {
        if(coherenceSync.isSimulated)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                otherPlayer = null;
                otherPlayerTransform = null;
                otherPlayerSync = null;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                otherPlayer = this.gameObject;
                otherPlayerTransform = this.transform;
                otherPlayerSync = this.coherenceSync;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach(var ctrl in FindObjectsOfType<Controller>())
                {
                    if(ctrl.gameObject != this.gameObject)
                    {
                        otherPlayer = ctrl.gameObject;
                        otherPlayerTransform = ctrl.transform;
                        otherPlayerSync = ctrl.coherenceSync;
                        break;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach (var ctrl in FindObjectsOfType<Controller>())
                {
                    if (ctrl.gameObject != this.gameObject)
                    {
                        Debug.Log("Sending command Foo");
                        ctrl.coherenceSync.SendCommand(coherenceSync, "Controller.Foo", ctrl.gameObject);
                        break;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                foreach (var ctrl in FindObjectsOfType<Controller>())
                {
                    if (ctrl.gameObject != this.gameObject)
                    {
                        Debug.Log("Sending command Boo");
                        ctrl.coherenceSync.SendCommand(coherenceSync, "Controller.Boo");
                        break;
                    }
                }
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

    public void OnSpawn(CoherenceSync sync)
    {
        name = name + " (networked)";
    }
}
