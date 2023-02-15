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

    public override string ToString()
    {
        string optionsList = "[";
        for (int i = 0; i < options.Count; i++)
        {
            optionsList += options[i];
            if (i < options.Count - 1)
            {
                optionsList += ", ";
            }
        }
        optionsList += "]";
        return "{\"" + this.question + "\", " + optionsList + ", Answer: " + answer.ToString() + "}";
    }
}
