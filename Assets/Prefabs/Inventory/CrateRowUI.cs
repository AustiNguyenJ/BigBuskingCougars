using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrateRowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text crateNameText;
    [SerializeField] private Button actionButton;

    public void Setup(string crateName, string buttonLabel, UnityEngine.Events.UnityAction onClick)
    {
        crateNameText.text = crateName;
        actionButton.GetComponentInChildren<TMP_Text>().text = buttonLabel;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(onClick);
    }
}