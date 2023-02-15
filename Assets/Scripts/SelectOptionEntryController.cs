using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectOptionEntryController : MonoBehaviour
{
    [SerializeField] private Button optionButton;
    [SerializeField] private Image optionButtonImage;
    [SerializeField] private Color selectedColour;
    [SerializeField] private Color notSelectedColour;
    [SerializeField] private TextMeshProUGUI optionText;

    public delegate void OnClickCallback(bool isButtonOn, int indexOfOption);

    [SerializeField] private bool isActive = false;
    private int index = -1;

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetOptionText(string option)
    {
        optionText.text = option;
    }

    public void RefreshUi()
    {
        optionButtonImage.color = isActive ? selectedColour : notSelectedColour;
    }
    
    public void Select()
    {
        isActive = true;
        RefreshUi();
    }

    public void Deselect()
    {
        isActive = false;
        RefreshUi();
    }

    public void Toggle()
    {
        isActive = !isActive;
        RefreshUi();
    }

    public void AddOnClickCallback(OnClickCallback callback)
    {
		optionButton.onClick.AddListener(delegate {
            callback.Invoke(isActive, index);
        });
    }

    void Start()
    {
        Deselect();
    }
}
