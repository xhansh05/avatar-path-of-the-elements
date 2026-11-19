using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsData stats;

    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float mobileMoveX;
    private float mobileMoveZ;
    private Vector3 currentMoveDir = Vector3.zero;
    private bool jumpRequested = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (stats == null) return;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float targetX = Input.GetAxis("Horizontal") != 0 ? Input.GetAxis("Horizontal") : mobileMoveX;
        float targetZ = Input.GetAxis("Vertical") != 0 ? Input.GetAxis("Vertical") : mobileMoveZ;

        Vector3 targetDirection = new Vector3(targetX, 0, targetZ);
        currentMoveDir = Vector3.Lerp(currentMoveDir, targetDirection, stats.acceleration * Time.deltaTime);

        controller.Move(currentMoveDir * stats.moveSpeed * Time.deltaTime);

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, stats.rotationSpeed * Time.deltaTime);
        }

        bool isMoving = currentMoveDir.magnitude > 0.1f;
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }

        if ((Input.GetButtonDown("Jump") || jumpRequested) && groundedPlayer)
        {
            Jump();
            jumpRequested = false;
        }
        else if (groundedPlayer)
        {
            jumpRequested = false;
        }

        playerVelocity.y += stats.gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void SetMobileInput(float x, float z)
    {
        mobileMoveX = x;
        mobileMoveZ = z;
    }

    public void TriggerJump()
    {
        jumpRequested = true;
    }

    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(stats.jumpHeight * -3.0f * stats.gravity);
        if (animator != null)
        {
            animator.SetTrigger("JumpTrigger");
        }
    }
}