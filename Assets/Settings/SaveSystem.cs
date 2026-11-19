using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int version = 1;
    public int bestScore = 0;
    public int selectedLevel = 1;
    public List<int> unlockedLevels = new List<int> { 1 };
    public float volume = 1f;
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    public SaveData Data { get; private set; }

    private string savePath;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Load();
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Data = CreateDefault();
            Save();
            return;
        }

        try
        {
            string json = File.ReadAllText(savePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                Data = CreateDefault();
                return;
            }

            SaveData loaded = JsonUtility.FromJson<SaveData>(json);
            if (loaded == null || loaded.unlockedLevels == null || loaded.unlockedLevels.Count == 0)
            {
                Data = CreateDefault();
                return;
            }

            Data = loaded;
        }
        catch
        {
            Data = CreateDefault();
        }
    }

    public void Save()
    {
        try
        {
            string json = JsonUtility.ToJson(Data, true);
            File.WriteAllText(savePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Save error: " + ex.Message);
        }
    }

    public void UnlockLevel(int levelNumber)
    {
        if (!Data.unlockedLevels.Contains(levelNumber))
        {
            Data.unlockedLevels.Add(levelNumber);
        }
        Save();
    }

    public void ResetProgress()
    {
        Data = CreateDefault();
        Save();
    }

    private SaveData CreateDefault()
    {
        return new SaveData
        {
            version = 1,
            bestScore = 0,
            selectedLevel = 1,
            unlockedLevels = new List<int> { 1 },
            volume = 1f
        };
    }

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            Save();
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }
}