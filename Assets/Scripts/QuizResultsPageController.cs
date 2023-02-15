using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuizResultsPageController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quizScoreText;
    [SerializeField] private UnityEvent onLeaveResultsPageCallback;

    public void DisplayResults()
    {
        int quizScore = GameManager.instance.GetCurrentlyPreviewingQuizScore();
        int maxScore = GameManager.instance.GetCurrentlyPreviewingQuizMaxScore();
        quizScoreText.text = Utils.FormatQuizScore(quizScore, maxScore);
    }

    public void LeaveResultsPage()
    {
        GameManager.instance.SetCurrentlyPreviewingQuizScore(-1);
        GameManager.instance.SetCurrentlyPreviewingQuizMaxScore(-1);
        onLeaveResultsPageCallback.Invoke();
    }
}
