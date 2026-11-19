using UnityEngine;

[CreateAssetMenu(fileName = "Level_01", menuName = "Assignment11/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelNumber = 1;
    public string levelTitle = "Level 1";
    public int scrollsToComplete = 3;
    public float timeLimit = 90f;
    public float enemySpeedMultiplier = 1f;
    public Vector3 spawnPosition;
}