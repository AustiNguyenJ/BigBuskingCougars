using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI drumText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI leftButtonText;
    [SerializeField] private TextMeshProUGUI rightButtonText;

    public void Setup(
        string displayText,
        string leftLabel,
        UnityEngine.Events.UnityAction leftAction,
        string rightLabel,
        UnityEngine.Events.UnityAction rightAction)
    {
        drumText.text = displayText;

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButtonText.text = leftLabel;
        rightButtonText.text = rightLabel;

        leftButton.onClick.AddListener(leftAction);
        rightButton.onClick.AddListener(rightAction);
    }
}