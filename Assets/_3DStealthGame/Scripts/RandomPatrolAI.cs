using UnityEngine;
using System.Collections;

public class RandomPatrolAI : BaseAI
{
    public float wanderRadius = 5f;
    public float wanderTimer = 2f;

    private Vector3 currentTarget;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WanderRoutine());
    }

    void FixedUpdate()
    {
        // Move towards the current target
        MoveTowards(currentTarget);
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            // Pick a random point within radius on the XZ plane
            Vector2 rand = Random.insideUnitCircle * wanderRadius;
            currentTarget = rb.position + new Vector3(rand.x, 0, rand.y);

            // Optional: keep target within scene bounds
            currentTarget.y = rb.position.y;

            // Wait for wanderTimer seconds before picking a new target
            yield return new WaitForSeconds(wanderTimer);
        }
    }
}