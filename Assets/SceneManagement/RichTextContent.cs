using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class KeywordStyle
{
    [Tooltip("The exact word or phrase to format in the text.")]
    [TableColumnWidth(150, Resizable = true)]
    [HideLabel]
    public string keyword;

    [TableColumnWidth(60, Resizable = false)]
    [HideLabel]
    public Color color = Color.white;

    [TableColumnWidth(50, Resizable = false)]
    [HideLabel]
    public bool isBold;

    [TableColumnWidth(70, Resizable = false)]
    [HideLabel]
    public bool useIcon;

    [ShowIf("useIcon")]
    [HideLabel]
    public Sprite iconSprite;

    public string ApplyStyle(string sourceText)
    {
        if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(sourceText))
            return sourceText;

        string hexColor = ColorUtility.ToHtmlStringRGB(color);
        string boldStart = isBold ? "<b>" : "";
        string boldEnd = isBold ? "</b>" : "";
        string iconTag = (useIcon && iconSprite != null) ? $"<sprite name=\"{iconSprite.name}\"> " : "";

        string formattedKeyword = $"{iconTag}<color=#{hexColor}>{boldStart}{keyword}{boldEnd}</color>";

        return sourceText.Replace(keyword, formattedKeyword);
    }
}

[Serializable]
public class RichTextContent
{
    [Title("Main Text")]
    [HideLabel]
    [TextArea(4, 10)]
    [Tooltip("Type naturally. Press Enter for new lines. Use {0}, {1} to inject dynamic code variables.")]
    public string plainText;

    [Title("Auto-Formatting Rules")]
    [Tooltip("Any word defined here will automatically be formatted in the text above.")]
    [TableList(ShowIndexLabels = false, AlwaysExpanded = true)]
    public List<KeywordStyle> stylingRules = new List<KeywordStyle>();

    public string GetText(params object[] args)
    {
        if (string.IsNullOrEmpty(plainText)) return "";

        string processedText = plainText;

        if (stylingRules != null)
        {
            foreach (KeywordStyle rule in stylingRules)
            {
                processedText = rule.ApplyStyle(processedText);
            }
        }

        if (args != null && args.Length > 0)
        {
            try 
            {
                processedText = string.Format(processedText, args);
            }
            catch (FormatException)
            {
                Debug.LogWarning("[RichTextContent] Format exception. Make sure your {0} tags match the arguments passed in code.");
            }
        }

        return processedText;
    }
}