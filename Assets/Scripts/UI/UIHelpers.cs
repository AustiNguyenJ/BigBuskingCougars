using UnityEngine;

public static class UIHelpers
{
    public static void ShowGroup(this CanvasGroup group, bool isVisible, bool ignoreParent = false)
    {
        if (Validate.AnyNull(group)) return;
        
        group.alpha = isVisible ? 1f : 0f;
        group.interactable = isVisible;
        group.blocksRaycasts = isVisible;

        group.ignoreParentGroups = ignoreParent;
    }

    public static bool GroupIsActive(this CanvasGroup group)
    {
        return (group.alpha == 1f && group.interactable && group.blocksRaycasts);
    }
}