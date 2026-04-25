using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CrateRowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text crateNameText;
    [SerializeField] private Button actionButton;
    [SerializeField] private TMP_Text detailsText;
    
    
    
    
    public void Setup(string crateName, string buttonLabel, string details, UnityEngine.Events.UnityAction onClick)
    {
        crateNameText.text = crateName;
        detailsText.text = details;
        actionButton.GetComponentInChildren<TMP_Text>().text = buttonLabel;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(onClick);
    }

    public void ChangeDetails(string details, Color color)
    {
        detailsText.text = details;
        detailsText.color = color;
    }
}