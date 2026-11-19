using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelData[] levels;
    [SerializeField] private Transform playerTransform;

    private LevelData currentLevelData;
    private int collectedInLevel = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        int indexToLoad = 0;
        if (SaveSystem.Instance != null && levels != null && levels.Length > 0)
        {
            indexToLoad = Mathf.Clamp(SaveSystem.Instance.Data.selectedLevel - 1, 0, levels.Length - 1);
        }
        LoadLevelData(indexToLoad);
    }

    public void LoadLevelData(int index)
    {
        if (levels == null || levels.Length == 0) return;

        index = Mathf.Clamp(index, 0, levels.Length - 1);
        currentLevelData = levels[index];
        collectedInLevel = 0;

        if (SaveSystem.Instance != null)
        {
            SaveSystem.Instance.Data.selectedLevel = currentLevelData.levelNumber;
            SaveSystem.Instance.Save();
        }

        if (playerTransform != null && currentLevelData.spawnPosition != Vector3.zero)
        {
            CharacterController cc = playerTransform.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            playerTransform.position = currentLevelData.spawnPosition;
            if (cc != null) cc.enabled = true;
        }
    }

    public void RegisterScrollCollected(int amount)
    {
        collectedInLevel += amount;

        if (currentLevelData != null && collectedInLevel >= currentLevelData.scrollsToComplete)
        {
            if (SaveSystem.Instance != null)
            {
                SaveSystem.Instance.UnlockLevel(currentLevelData.levelNumber + 1);
            }
        }
    }
}