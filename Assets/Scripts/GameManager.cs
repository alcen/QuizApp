using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game data")]
    public GameData data;

    private List<Observer> observers = new List<Observer>{};

    public void SaveData()
    {
        DataManager.SaveData(data);
    }

    public void LoadData()
    {
        data = DataManager.LoadData();
    }

    // Adds an observer that will be notified when the game data changes
    public void AddObserver(Observer obs)
    {
        observers.Add(obs);
    }

    // Notifies all observers that the game data has changed
    public void NotifyAll()
    {
        foreach (Observer obs in observers)
        {
            obs.update(data);
        }
    }

    public void AddQuiz(Quiz newQuiz)
    {
        data.quizzes.Add(newQuiz);
        SaveData();
        NotifyAll();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        LoadData();   
    }
}
