using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
       
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0f, moveZ).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            
            Vector3 targetPosition = transform.position + moveDir * 5f;
            agent.SetDestination(targetPosition);

           
            if (animator != null)
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }
}