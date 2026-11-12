using UnityEngine;

public class PatrollerEnemy : BaseEnemy
{
    [Header("Patrol Settings")]
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float patrolSpeed = 2f;

    protected override void Start()
    {
        base.Start();
        agent.speed = patrolSpeed;
        if (waypoints != null && waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    protected override void PatrolBehavior()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}