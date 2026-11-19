using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Assignment11/Enemy Stats")]
public class EnemyStatsData : ScriptableObject
{
    public float moveSpeed = 3.5f;
    public int maxHealth = 30;
    public int damage = 1;
}