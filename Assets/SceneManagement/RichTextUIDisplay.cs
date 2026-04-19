using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RichTextDisplay : MonoBehaviour
{
    [HideLabel]
    public RichTextContent content = new RichTextContent();

    TextMeshProUGUI tmpText;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay(params object[] args)
    {
        if (tmpText == null) 
            tmpText = GetComponent<TextMeshProUGUI>();

        if (tmpText != null)
        {
            tmpText.text = content.GetText(args);
        }
    }

    void OnValidate()
    {
        if (tmpText == null) 
            tmpText = GetComponent<TextMeshProUGUI>();

        // Live updates the canvas when the designer tweaks rules or types in the inspector
        if (tmpText != null && content != null)
        {
            tmpText.text = content.GetText(); 
        }
    }
}