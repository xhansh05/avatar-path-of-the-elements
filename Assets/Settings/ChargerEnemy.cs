using UnityEngine;

public class ChargerEnemy : BaseEnemy
{
    [Header("Charger Settings")]
    public float chargeSpeed = 6f;
    public float normalSpeed = 2f;

    protected override void ChaseBehavior(float distanceToPlayer)
    {
       
        agent.speed = chargeSpeed;
        agent.SetDestination(player.position);
    }

    protected override void PatrolBehavior()
    {
        agent.speed = normalSpeed;
    }
}