using TMPro;
using UnityEngine;

public class QuizDetailsPageController : MonoBehaviour
{
    // Title field of loaded quiz
    [SerializeField] private TextMeshProUGUI quizTitleText;
    // Container to attach questions to
    [SerializeField] private GameObject questionTableContent;
    // Prefab of a question entry
    [SerializeField] private GameObject questionEntryPrefab;
    // Preview quiz button
    [SerializeField] private GameObject previewQuizButton;

    // Refreshes the UI with the new quiz details
    private void RefreshUi(Quiz quiz)
    {
        foreach (Transform child in questionTableContent.transform)
        {
            Destroy(child.gameObject);
        }

        previewQuizButton.SetActive(quiz.questions.Count != 0);
        quizTitleText.text = quiz.name;

        for (int i = 0; i < quiz.questions.Count; i++)
        {
            Question currentQuestion = quiz.questions[i];
            GameObject newEntry = Instantiate(questionEntryPrefab,
                                              new Vector3(0, 0, 0),
                                              Quaternion.identity,
                                              questionTableContent.transform) as GameObject;
            QuestionEntryController entryController = newEntry.GetComponent<QuestionEntryController>();
            entryController.RefreshUi(i, currentQuestion);
        }
    }

    // Loads a quiz with the specified index
    public void LoadQuiz(int index)
    {
        RefreshUi(GameManager.instance.GetGameData().quizzes[index]);
    }

    // Loads the latest quiz added to the game 
    public void LoadLatestQuiz()
    {
        int latestQuiz = GameManager.instance.GetLatestQuizIndex();
        LoadQuiz(latestQuiz);
        GameManager.instance.SetCurrentlyEditingQuizIndex(latestQuiz);
    }
    
    // Loads the quiz that is currently being edited
    public void LoadCurrentlyEditingQuiz()
    {
        LoadQuiz(GameManager.instance.GetCurrentlyEditingQuizIndex());
    }
}
