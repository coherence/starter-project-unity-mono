using System.Collections;
using UnityEngine;
using Random = System.Random;

public class NPCBehaviour : CharacterBehaviour
{
    private Vector3 targetPos;

    private Random random;
    
    protected override void Awake()
    {
        base.Awake();
        random = new Random((int)(Time.time * 1000f));
        StartCoroutine(AICoroutine());
    }

    IEnumerator AICoroutine()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(1+ (float)random.NextDouble()*2f);

            const float amplitude = 5f;
            float dx = amplitude * (float)(random.NextDouble() - 0.5f);
            float dz = amplitude * (float)(random.NextDouble() - 0.5f);

            targetPos = new Vector3(dx, 0, dz);
            
            CycleState();
        }
    }
    
    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * movementSpeed);
    }
}
