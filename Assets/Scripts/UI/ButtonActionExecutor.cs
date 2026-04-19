using System;
using Events.UI;
using Oculus.Interaction.Input;
using UnityEngine;

public interface IMainMenuEvent { }

[Serializable]
public class InvokeMainMenuEvent<T>: ButtonAction where T : struct, IMainMenuEvent
{
    public T eventStructRef;
    
    public override void Execute()
    {
        GlobalEventAsset.Instance.TriggerEvent(eventStructRef);
    }
}

[Serializable]
public class InvokePlayButtonSelectedEvent : InvokeMainMenuEvent<OnPlayButtonSelected> {}
[Serializable]
public class InvokeLocationSelectedEvent : InvokeMainMenuEvent<OnLocationSelected> {}

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
        Debug.Log("Attempting to exectute on " + gameObject.name);
        if (Validate.AnyNull(action)) return;
        action.Execute();
    }
}
