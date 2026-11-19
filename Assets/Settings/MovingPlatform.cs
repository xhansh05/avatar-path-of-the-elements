using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 moveAxis = new Vector3(1f, 0f, 0f);
    [SerializeField] private float moveDistance = 1.5f;
    [SerializeField] private float moveSpeed = 0.7f;

    private Vector3 startPosition;
    private Vector3 lastPosition;
    private CharacterController playerOnPlatform;

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        Vector3 newPosition = startPosition + moveAxis.normalized * offset;
        Vector3 deltaMovement = newPosition - lastPosition;

        transform.position = newPosition;

        if (playerOnPlatform != null)
        {
            playerOnPlatform.Move(deltaMovement);
        }

        lastPosition = newPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null)
        {
            playerOnPlatform = cc;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null && playerOnPlatform == cc)
        {
            playerOnPlatform = null;
        }
    }
}