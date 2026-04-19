using UnityEngine;

public class WaypointAI : BaseAI
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    protected override void Start()
    {
        base.Start();

        if (waypoints.Length == 0)
            Debug.LogWarning("WaypointAI: No waypoints assigned!");
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Vector3 target = waypoints[currentWaypointIndex].position;
        Vector3 toTarget = target - rb.position;

        // Finally this works
        if (toTarget.magnitude < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            target = waypoints[currentWaypointIndex].position;
            toTarget = target - rb.position;
        }

        MoveTowards(target);
    }
}