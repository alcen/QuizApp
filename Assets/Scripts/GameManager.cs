using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Persistent game data")]
    [SerializeField] private GameData data = new GameData(new List<Quiz>{});

    [Header("Temporary data for current session")]
    [SerializeField] private int latestQuizIndex = -1;
    // Index of the quiz being edited currently
    [SerializeField] private int currentlyEditingQuizIndex = 1;
    // Current score of the quiz being previewed
    [SerializeField] private int currentlyPreviewingQuizScore = -1;
    // Max score of the current quiz being previewed
    [SerializeField] private int currentlyPreviewingQuizMaxScore = -1;

    // Callback that runs when a change in game data occurs
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

    public int GetLatestQuizIndex() => latestQuizIndex;
    public void SetLatestQuizIndex(int index)
    {
        latestQuizIndex = index;
    }

    public int GetCurrentlyEditingQuizIndex() => currentlyEditingQuizIndex;
    public void SetCurrentlyEditingQuizIndex(int index)
    {
        currentlyEditingQuizIndex = index;
    }

    public int GetCurrentlyPreviewingQuizScore() => currentlyPreviewingQuizScore;
    public void SetCurrentlyPreviewingQuizScore(int score)
    {
        currentlyPreviewingQuizScore = score;
    }
    public void IncrementCurrentlyPreviewingQuizScore()
    {
        currentlyPreviewingQuizScore++;
    }

    public int GetCurrentlyPreviewingQuizMaxScore() => currentlyPreviewingQuizMaxScore;
    public void SetCurrentlyPreviewingQuizMaxScore(int score)
    {
        currentlyPreviewingQuizMaxScore = score;
    }

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
        // Add quiz to the end of the list
        int indexToInsertAt = data.quizzes.Count;
        data.quizzes.Insert(indexToInsertAt, newQuiz);
        SetLatestQuizIndex(indexToInsertAt);
        // Save data and notify observers
        SaveData();
        NotifyAll();
    }

    public void AddQuestion(int quizIndex, Question qn)
    {
        data.quizzes[quizIndex].questions.Add(qn);
    }

    // Adds a question to the current quiz that is being edited
    public void AddQuestionToCurrentQuiz(Question qn)
    {
        AddQuestion(currentlyEditingQuizIndex, qn);
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
