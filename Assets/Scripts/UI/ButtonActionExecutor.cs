using System;
using Events.UI;
using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class ExitAppButtonAction : ButtonAction
{
    public override void Execute()
    {
#if UNITY_EDITOR
        // Stops playmode in the Unity Editor
        EditorApplication.isPlaying = false;
#else
            // Closes the application in a standalone build
            Application.Quit();
#endif
    }
}

[Serializable]
public abstract class ButtonAction
{
    public abstract void Execute();
}
    
public class ButtonActionExecutor : MonoBehaviour
{
    [SerializeReference, SerializeField] ButtonAction action;

    public void Execute()
    {
        if (Validate.AnyNull(action)) return;
        action.Execute();
    }
}
