using System;
using Events.UI;

public interface IMainMenuEvent { }

[Serializable]
public class InvokePlayButtonSelectedEvent : InvokeMainMenuEvent<OnPlayButtonSelected> {}
[Serializable]
public class InvokeLocationSelectedEvent : InvokeMainMenuEvent<OnLocationSelected> {}

[Serializable]
public class InvokeBackButtonSelectedEvent : InvokeMainMenuEvent<OnBackButtonSelected> { }

[Serializable]
public class InvokeMainMenuEvent<T>: ButtonAction where T : struct, IMainMenuEvent
{
    public T eventStructRef;
    
    public override void Execute()
    {
        GlobalEventAsset.Instance.TriggerEvent(eventStructRef);
    }
}