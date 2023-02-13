using UnityEngine;
using System;
using System.IO;

public static class DataManager
{
    // File name within the target directory
    public static string fileName = "quiz_data.json";
    
    public static void SaveData(QuizData quiz)
    {    
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            File.WriteAllText(fullPath, JsonUtility.ToJson(quiz));
        }
        catch (Exception e)
        {
            Debug.LogError($"Saving to {fullPath} unsuccessful with exception {e}");
        }
    }

    public static QuizData LoadData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        QuizData quiz = new QuizData();
        try
        {
            string jsonData = File.ReadAllText(fullPath);
            JsonUtility.FromJsonOverwrite(jsonData, quiz);
        }
        catch (Exception e)
        {
            Debug.LogError($"Loading from {fullPath} unsuccessful with exception {e}");
        }
        return quiz;
    }
}
