using System;
using Events.UI;

public interface IUiEvent { }

#region MainMenuEvents
[Serializable]
public class InvokePlayButtonSelectedEvent : InvokeUIEvent<OnPlayButtonSelected> {}
[Serializable]
public class InvokeLocationSelectedEvent : InvokeUIEvent<OnLocationSelected> {}
#endregion


[Serializable]
public class InvokeBackButtonSelectedEvent : InvokeUIEvent<OnBackButtonSelected> { }

[Serializable]
public class InvokeWristMenuViewLocationsEvent : InvokeUIEvent<WristMenuViewLocationsEvent> { }

[Serializable]
public class InvokeUIEvent<T>: ButtonAction where T : struct, IUiEvent
{
    public T eventStructRef;
    
    public override void Execute()
    {
        GlobalEventAsset.Instance.TriggerEvent(eventStructRef);
    }
}