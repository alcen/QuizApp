using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NewQuizPageController : MonoBehaviour
{
    [SerializeField] private TMP_InputField quizNameField;
    [SerializeField] private UnityEvent onSaveSuccessCallback;

    public void SaveNewQuiz()
    {
        string quizName = quizNameField.text;
        if (String.IsNullOrWhiteSpace(quizName))
        {
            // Quiz name should not be empty
            Debug.LogWarning("Please enter a quiz name.");
        }
        else
        {
            GameManager.instance.AddQuiz(new Quiz(quizName, new List<Question>{}, 0));
            onSaveSuccessCallback.Invoke();
        }
    }
}
