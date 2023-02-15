using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizPreviewPageController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionNumberText;
    [SerializeField] private TextMeshProUGUI questionNameText;
    // Container to attach option entries to
    [SerializeField] private GameObject optionEntriesContent;
    [SerializeField] private GameObject selectOptionEntryPrefab;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject submitButton;
    [SerializeField] private UnityEvent onSubmitCallback;

    // List of all the option entry controllers
    private List<SelectOptionEntryController> optionEntryControllers =
        new List<SelectOptionEntryController>{};

    // Default value for currently selected option
    private const int INVALID_INDEX = -1;
    // Index of the currently selected option
    [SerializeField] private int currentlySelectedOption = INVALID_INDEX;

    // Internal state variables
    private Quiz currentQuiz;
    private int currentQuestionIndex = -1;

    public void RefreshUi(Question qs)
    {
        int numberOfQuestions = currentQuiz.questions.Count;
        questionNumberText.text = Utils.FormatQuestionNumber(currentQuestionIndex, numberOfQuestions);
        questionNameText.text = qs.question;
        currentlySelectedOption = INVALID_INDEX;
        for (int i = 0; i < qs.options.Count && i < optionEntryControllers.Count; i++)
        {
            optionEntryControllers[i].SetOptionText(qs.options[i]);
            optionEntryControllers[i].Deselect();
        }
    }

    public void PreviewQuiz(Quiz q)
    {
        if (q.questions.Count != 0)
        {
            currentQuiz = q;
            currentQuestionIndex = -1;
            GameManager.instance.SetCurrentlyPreviewingQuizScore(0);
            nextButton.SetActive(true);
            submitButton.SetActive(false);
            AdvanceQuiz();
        }
        else
        {
            Debug.LogWarning("A quiz without questions cannot be previewed");
        }
    }

    public void PreviewQuizWithIndex(int index)
    {
        PreviewQuiz(GameManager.instance.GetGameData().quizzes[index]);
    }
    
    public void PreviewCurrentlyEditingQuiz()
    {
        PreviewQuizWithIndex(GameManager.instance.GetCurrentlyEditingQuizIndex());
    }

    public void AdvanceQuiz()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex == currentQuiz.questions.Count - 1)
        {
            // Change to submit button
            nextButton.SetActive(false);
            submitButton.SetActive(true);
        }
        else if (currentQuestionIndex != 0 &&
                 currentlySelectedOption == currentQuiz.questions[currentQuestionIndex].answer)
        {
            // Update the score
            GameManager.instance.IncrementCurrentlyPreviewingQuizScore();
        }
        RefreshUi(currentQuiz.questions[currentQuestionIndex]);
    }

    public void SubmitQuiz()
    {
        if (currentlySelectedOption == currentQuiz.questions[currentQuestionIndex].answer)
        {
            GameManager.instance.IncrementCurrentlyPreviewingQuizScore();
        }
        onSubmitCallback.Invoke();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in optionEntriesContent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Utils.NUMBER_OF_OPTIONS; i++)
        {
            GameObject optionEntry = Instantiate(selectOptionEntryPrefab,
                                                 new Vector3(0, 0, 0),
                                                 Quaternion.identity,
                                                 optionEntriesContent.transform) as GameObject;
            SelectOptionEntryController entryController =
                optionEntry.GetComponent<SelectOptionEntryController>();
            entryController.SetIndex(i);
            entryController.AddOnClickCallback((isButtonOn, index) => {
                if (isButtonOn)
                {
                    currentlySelectedOption = index;
                    for (int j = 0; j < optionEntryControllers.Count; j++)
                    {
                        if (j != index)
                        {
                            optionEntryControllers[j].Deselect();
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
            optionEntryControllers.Add(entryController);
        }
    }
}
