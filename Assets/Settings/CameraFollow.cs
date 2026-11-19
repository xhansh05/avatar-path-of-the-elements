using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    
    [Header("Смещение камеры (сзади и выше)")]
    public Vector3 offset = new Vector3(0f, 2.5f, -4.5f); 
    public float smoothSpeed = 10f; 

    void LateUpdate()
    {
        if (player == null) return;

        
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);
        
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}