using System;
using UnityEngine;

public class UiController : Singleton<UiController>
{
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject newQuizPage;
    [SerializeField] private GameObject quizDetailsPage;
    [SerializeField] private GameObject questionDetailsPage;
    [SerializeField] private GameObject quizPreviewPage;
    [SerializeField] private GameObject quizResultsPage;

    private GameObject[] pages = {};

    // Start is called before the first frame update
    void Start()
    {
        pages = new GameObject[]
        {
            mainPage,            // 0
            newQuizPage,         // 1
            quizDetailsPage,     // 2
            questionDetailsPage, // 3
            quizPreviewPage,     // 4
            quizResultsPage      // 5
        };
        // At the beginning, all pages are active for initialisation
        // UiController is set to be the last script to execute,
        // and it will make the application start on MainPage
        SwitchToMainPage();
    }

    public GameObject GetPageGameObject(Page p)
    {
        return pages[(int) p];
    }

    public void SwitchToPage(Page p)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == (int) p);
        }
    }
    
    public void SwitchToMainPage() => SwitchToPage(Page.MAIN);
    public void SwitchToNewQuizPage() => SwitchToPage(Page.NEW_QUIZ);
    public void SwitchToQuizDetailsPage() => SwitchToPage(Page.QUIZ_DETAILS);
    public void SwitchToQuestionDetailsPage() => SwitchToPage(Page.QUESTION_DETAILS);
    public void SwitchToQuizPreviewPage() => SwitchToPage(Page.QUIZ_PREVIEW);
    public void SwitchToQuizResultsPage() => SwitchToPage(Page.QUIZ_RESULTS);

    public void EditQuiz(int index)
    {
        quizDetailsPage.GetComponent<QuizDetailsPageController>().LoadQuiz(index);
        GameManager.instance.SetCurrentlyEditingQuizIndex(index);
        SwitchToQuizDetailsPage();
    }
}
