using TMPro;
using UnityEngine;

public class QuestionEntryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI optionText;
    [SerializeField] private int indexInTable = -1;

    public void RefreshUi(int index, Question qs)
    {
        indexInTable = index;
        numberText.text = Utils.FormatTableNumber(indexInTable);
        questionText.text = qs.question;
        optionText.text = (qs.answer + 1).ToString();
    }
}
