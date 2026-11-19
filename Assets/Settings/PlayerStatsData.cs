using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Assignment11/Player Stats")]
public class PlayerStatsData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 15f;
    public float acceleration = 10f;
    public int maxHealth = 3;
}