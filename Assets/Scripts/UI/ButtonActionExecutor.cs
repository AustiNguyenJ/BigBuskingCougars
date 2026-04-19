using System;
using Events.UI;
using Oculus.Interaction.Input;
using UnityEngine;

public abstract class ButtonAction
{
    public abstract void Execute();
}

public interface IButtonEvent
{
    void Invoke();
}

[Serializable]
public class SwitchSettingTabButtonEvent : IButtonEvent
{
    [SerializeField] SettingTabType type;
    public void Invoke()
    {
        GlobalEventAsset.Instance.TriggerEvent(new SwitchSettingTabEvent { typeToSwitchTo = type });
    }
}

[Serializable]
public class InvokeEvent : ButtonAction
{
    [SerializeReference] public IButtonEvent buttonEvent;
    
    public override void Execute()
    {
        buttonEvent.Invoke();
    }
}
    
public class ButtonActionExecutor : MonoBehaviour
{
    [SerializeReference] ButtonAction action;

    public void Execute()
    {
        action.Execute();
    }
}
