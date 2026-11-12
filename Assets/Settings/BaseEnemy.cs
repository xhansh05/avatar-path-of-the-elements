using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    protected State currentState;

    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;

    [Header("Detection Settings")]
    public float viewDistance = 10f;
    public float attackDistance = 2f;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        currentState = State.Patrol;
    }

    protected virtual void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                PatrolBehavior();
                if (distanceToPlayer <= viewDistance)
                {
                    currentState = State.Chase;
                    animator.SetTrigger("ToChase");
                }
                break;

            case State.Chase:
                ChaseBehavior(distanceToPlayer);
                if (distanceToPlayer <= attackDistance)
                {
                    currentState = State.Attack;
                    animator.SetTrigger("ToAttack");
                }
                else if (distanceToPlayer > viewDistance * 1.5f)
                {
                    currentState = State.Patrol;
                    animator.SetTrigger("BackToPatrol");
                }
                break;

            case State.Attack:
                AttackBehavior(distanceToPlayer);
                if (distanceToPlayer > attackDistance)
                {
                    currentState = State.Chase;
                    animator.SetTrigger("ToChase");
                }
                break;
        }
    }

    protected virtual void PatrolBehavior() { }
    
    protected virtual void ChaseBehavior(float distanceToPlayer)
    {
        agent.SetDestination(player.position);
    }

    protected virtual void AttackBehavior(float distanceToPlayer)
    {
        agent.SetDestination(transform.position); 
        
        
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}