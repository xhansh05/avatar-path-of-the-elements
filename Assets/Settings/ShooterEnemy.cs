using UnityEngine;

public class ShooterEnemy : BaseEnemy
{
    [Header("Shooter Settings")]
    public float shootDistance = 6f;
    public float shooterSpeed = 3f;
    public float fireRate = 1.5f; 
    private float nextFireTime = 0f;

    [Header("Projectile (Optional)")]
    public GameObject bulletPrefab; 
    public Transform firePoint;     

    protected override void Start()
    {
        base.Start();
        attackDistance = shootDistance; 
    }

    protected override void ChaseBehavior(float distanceToPlayer)
    {
        agent.speed = shooterSpeed;
        
        
        if (distanceToPlayer > shootDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    protected override void AttackBehavior(float distanceToPlayer)
    {
        base.AttackBehavior(distanceToPlayer);

       
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Шутер произвел выстрел в сторону игрока!");

        
        /*
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        */
    }
}