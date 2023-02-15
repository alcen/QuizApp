using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Question
{
    public string question;
    public List<string> options;
    public int answer;

    public Question(string question, List<string> options, int answer)
    {
        this.question = question;
        this.options = options;
        this.answer = answer;
    }
}
