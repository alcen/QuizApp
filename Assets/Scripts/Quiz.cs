using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quiz
{
    public string name;
    public List<Question> questions;
    public int score;

    public Quiz(string name, List<Question> questions, int score)
    {
        this.name = name;
        this.questions = questions;
        this.score = score;
    }
}
