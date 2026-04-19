using System;
using Events.UI;
using UnityEngine;

[Serializable]
public abstract class MenuState
{
    public abstract void OnEnter();
    public abstract void OnExit();
}

[Serializable]
public class OnMainMenu : MenuState
{
    [SerializeField] CanvasGroup mainMenuGroup;
    
    public override void OnEnter()
    {
        mainMenuGroup.alpha = 1;
        mainMenuGroup.blocksRaycasts = true;
        mainMenuGroup.interactable = true;
        GlobalEventAsset.Instance.StartListening<OnLocationSelected>(OnLocationSelected);
    }

    void OnLocationSelected(OnLocationSelected data)
    {
        GlobalEventAsset.Instance.TriggerEvent(new RequestSceneLoadEvent { sceneGroupToLoad = data.locationSceneGroupAsset});
    }

    public override void OnExit()
    {
        mainMenuGroup.alpha = 0;
        mainMenuGroup.blocksRaycasts = false;
        mainMenuGroup.interactable = false;
        GlobalEventAsset.Instance.StopListening<OnLocationSelected>(OnLocationSelected);
    }
}

[Serializable]
public class LocationSelectionMenu : MenuState
{
    [SerializeField] CanvasGroup locationSelectionGroup;

    public override void OnEnter()
    {
        locationSelectionGroup.alpha = 1;
        locationSelectionGroup.blocksRaycasts = true;
        locationSelectionGroup.interactable = true;
    }

    public override void OnExit()
    {
        locationSelectionGroup.alpha = 0;
        locationSelectionGroup.blocksRaycasts = false;
        locationSelectionGroup.interactable = false;
    }
}