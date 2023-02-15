using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<Quiz> quizzes;

    public GameData(List<Quiz> quizzes)
    {
        this.quizzes = quizzes;
    }
}
