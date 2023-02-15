using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    // File name within the target directory
    public static string fileName = "quiz_data.json";
    // Target directory to be used
    public static string dataPath = Application.persistentDataPath;
    
    public static void SaveData(GameData data)
    {    
        string fullPath = Path.Combine(dataPath, fileName);
        try
        {
            File.WriteAllText(fullPath, JsonUtility.ToJson(data));
        }
        catch (Exception e)
        {
            Debug.LogError($"Saving to {fullPath} unsuccessful with exception {e}");
        }
    }

    public static GameData LoadData()
    {
        string fullPath = Path.Combine(dataPath, fileName);
        if (File.Exists(fullPath))
        {
            GameData data = new GameData(new List<Quiz>{});
            try
            {
                string jsonData = File.ReadAllText(fullPath);
                JsonUtility.FromJsonOverwrite(jsonData, data);
            }
            catch (Exception e)
            {
                Debug.LogError($"Loading from {fullPath} unsuccessful with exception {e}");
            }
            return data;
        }
        else
        {
            // Could not find game data, possibly at first launch
            // Create a new empty GameData object
            return new GameData(new List<Quiz>{});
        }
    }
}
