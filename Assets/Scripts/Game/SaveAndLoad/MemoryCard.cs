using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class MemoryCard {
    private static string baseName = "/data.txt";

    public int selectedLevel = 0;
    public List<LevelProgress> levels = new List<LevelProgress>();

    public static MemoryCard Load()
    {
        string path = Application.persistentDataPath + baseName;
        CreateFileIfDontExist(path, JsonUtility.ToJson(new MemoryCard()));
        string content = File.ReadAllText(path);
        return JsonUtility.FromJson<MemoryCard>(content);
    }
    public void Save()
    {
        string path = Application.persistentDataPath + baseName;
        string content = JsonUtility.ToJson(this);
        CreateFileIfDontExist(path, content);
        File.WriteAllText(path, content);
    }
    public static int GetSelectedLevel()
    {
        return Load().selectedLevel;
    }
    public static void SetSelectedLevel(int index)
    {
        var memoryCard = Load();
        memoryCard.selectedLevel = index;
        memoryCard.Save();
    }
    public static void CreateFileIfDontExist(string path, string content)
    {
        if (!File.Exists(path))
        {
            Debug.Log(path);
            File.WriteAllText(path, content);
        }
    }
}
[System.Serializable]
public class LevelProgress
{
    public int bigTreasures = 0;
    public int treasures = 0;
    public int steps = 100;
}
