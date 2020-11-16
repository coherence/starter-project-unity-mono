using UnityEngine;
using Coherence.MonoBridge;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float timeSpent;
    private CoherenceSync sender;

    public void SetCoherenceSender(CoherenceSync sender)
    {
        this.sender = sender;
    }

    private void Reset()
    {
        speed = 4f;
        lifetime = 5f;
    }

    private void Update()
    {
        if (timeSpent < lifetime)
        {
            timeSpent += Time.deltaTime;

            if (timeSpent >= lifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (sender == null)
        {
            return;
        }

        CoherenceSync target = c.GetComponent<CoherenceSync>();
        if (target == sender)
        {
            return;
        }

        if (target)
        {
            target.SendNetworkCommand(sender, "Hit");
        }

        Destroy(gameObject);
    }
}
