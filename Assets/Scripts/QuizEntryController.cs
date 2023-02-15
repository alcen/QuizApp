using TMPro;
using UnityEngine;

public class QuizEntryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI quizNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int indexInTable = -1;

    public void RefreshUi(int index, Quiz q)
    {
        indexInTable = index;
        numberText.text = Utils.FormatTableNumber(index);
        quizNameText.text = q.name;
        scoreText.text = q.score + "/" + q.questions.Count;
    }

    public void EditThisQuiz()
    {
        UiController.instance.EditQuiz(indexInTable);
    }
}
