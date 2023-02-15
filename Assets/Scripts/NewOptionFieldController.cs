using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewOptionFieldController : MonoBehaviour
{
    [SerializeField] private string optionNumPrefix = "Option ";
    [SerializeField] private TextMeshProUGUI optionNumText;
    [SerializeField] private Toggle radioButton;
    [SerializeField] private TMP_InputField inputField;

    public delegate void OnValueChangedCallback(bool isButtonOn, int indexOfOption);

    private int index = -1;

    public void SetIndex(int index)
    {
        this.index = index;
        optionNumText.text = optionNumPrefix + (index + 1).ToString();
    }
    
    public Toggle GetRadioButton()
    {
        return radioButton;
    }

    public TMP_InputField GetInputField()
    {
        return inputField;
    }

    // Adds an action to be invoked when the value of this button changes
    public void AddOnValueChangedCallback(OnValueChangedCallback callback)
    {
        radioButton.onValueChanged.AddListener(delegate {
            callback.Invoke(radioButton.isOn, index);
        });
    }

    void Start()
    {
        radioButton.isOn = false;
    }
}
