using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game data")]
    private GameData data = new GameData(new List<Quiz>{});

    public delegate void DataChangedCallback(GameData data);

    private List<DataChangedCallback> callbacks = new List<DataChangedCallback>{};

    public void SaveData()
    {
        DataManager.SaveData(data);
    }

    public void LoadData()
    {
        data = DataManager.LoadData();
        NotifyAll();
    }

    // Returns a copy of the current game data
    public GameData GetGameData() => data;

    // Adds an observer that will be notified when the game data changes
    public void AddObserver(DataChangedCallback dcc)
    {
        callbacks.Add(dcc);
    }

    // Notifies all observers that the game data has changed
    public void NotifyAll()
    {
        foreach (DataChangedCallback dcc in callbacks)
        {
            dcc.Invoke(data);
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
        NotifyAll();
    }
}
