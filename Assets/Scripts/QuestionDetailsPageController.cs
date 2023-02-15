using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestionDetailsPageController : MonoBehaviour
{
    [SerializeField] private TMP_InputField questionNameField;
    // Container to attach option fields to
    [SerializeField] private GameObject optionFieldsContent;
    [SerializeField] private GameObject newOptionFieldPrefab;
    [SerializeField] private UnityEvent onSaveSuccessCallback;

    // List of all the radio buttons
    private List<Toggle> radioButtons = new List<Toggle>{};
    // List of all the option fields in the same order as they are displayed
    private List<TMP_InputField> optionFields = new List<TMP_InputField>{};

    // Default value for currently selected option
    private const int INVALID_INDEX = -1;
    // Index of the currently selected option
    [SerializeField] private int currentlySelectedOption = INVALID_INDEX;

    public void SaveNewQuestion()
    {
        string questionName = questionNameField.text;
        if (String.IsNullOrWhiteSpace(questionName))
        {
            // Question name should not be empty
            Debug.LogWarning("Please enter a question name.");
        }
        else if (currentlySelectedOption == INVALID_INDEX)
        {
            // Need to select an answer
            Debug.LogWarning("Please select an answer.");
        }
        else
        {
            List<string> options = new List<string>{};
            foreach (TMP_InputField optFld in optionFields)
            {
                options.Add(optFld.text);
            }
            GameManager.instance.AddQuestionToCurrentQuiz(
                new Question(questionName, options, currentlySelectedOption));
            onSaveSuccessCallback.Invoke();
           
            // Clear text from fields
            questionNameField.text = String.Empty;
            foreach (TMP_InputField optFld in optionFields)
            {
                optFld.text = String.Empty;
            }
            // Clear selected radio buttons
            foreach (Toggle tog in radioButtons)
            {
                tog.isOn = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in optionFieldsContent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Utils.NUMBER_OF_OPTIONS; i++)
        {
            GameObject newOptionField = Instantiate(newOptionFieldPrefab,
                                                    new Vector3(0, 0, 0),
                                                    Quaternion.identity,
                                                    optionFieldsContent.transform) as GameObject;
            NewOptionFieldController fieldController =
                newOptionField.GetComponent<NewOptionFieldController>();
            fieldController.SetIndex(i);
            fieldController.AddOnValueChangedCallback((isButtonOn, index) => {
                if (isButtonOn)
                {
                    currentlySelectedOption = index;
                    for (int j = 0; j < radioButtons.Count; j++)
                    {
                        if (j != index)
                        {
                            radioButtons[j].isOn = false;
                        }
                    }
                }
                else
                {
                    if (currentlySelectedOption == index)
                    {
                        currentlySelectedOption = INVALID_INDEX;
                    }
                }
            });
            radioButtons.Add(fieldController.GetRadioButton());
            optionFields.Add(fieldController.GetInputField());
        }
    }
}
