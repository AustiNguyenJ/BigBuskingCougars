using System;

[Serializable]
public class TriggerSceneLoadEvent : ButtonAction
{
    public RequestSceneLoadEvent sceneLoadEvent;
    public override void Execute()
    {
        GlobalEventAsset.Instance.TriggerEvent(sceneLoadEvent);
    }
}