using System;
using System.Linq;
using Events.UI;
using UnityEngine;

[Serializable]
public abstract class MenuState
{

    protected MainMenuManager manager;

    public void SetManager(MainMenuManager newManager)
    {
        this.manager = newManager;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
}

[Serializable]
public class OnMainMenu : MenuState
{
    [SerializeField] CanvasGroup mainMenuGroup;
    [SerializeReference] MenuState locationSelectionState;
    
    public override void OnEnter()
    {
        mainMenuGroup.alpha = 1;
        mainMenuGroup.blocksRaycasts = true;
        mainMenuGroup.interactable = true;
        GlobalEventAsset.Instance.StartListening<OnPlayButtonSelected>(OnPlayButtonSelected);
    }
    
    public void OnPlayButtonSelected()
    {
        manager.ChangeState(locationSelectionState);
    }

    public override void OnExit()
    {
        Debug.Log($"OnExit called. CanvasGroup ref: {mainMenuGroup}", mainMenuGroup);
        
        if (mainMenuGroup == null)
        {
            Debug.LogWarning("CanvasGroup is NULL or destroyed!");
            return;
        }

        mainMenuGroup.alpha = 0;
        mainMenuGroup.blocksRaycasts = false;
        mainMenuGroup.interactable = false;
        GlobalEventAsset.Instance.StopListening<OnPlayButtonSelected>(OnPlayButtonSelected);
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
        GlobalEventAsset.Instance.StartListening<OnLocationSelected>(OnLocationSelected);

    }

    void OnLocationSelected(OnLocationSelected data)
    {
        GlobalEventAsset.Instance.TriggerEvent(new RequestSceneLoadEvent { sceneGroupToLoad = data.locationSceneGroupAsset });
    }

    public override void OnExit()
    {
        locationSelectionGroup.alpha = 0;
        locationSelectionGroup.blocksRaycasts = false;
        locationSelectionGroup.interactable = false;
        GlobalEventAsset.Instance.StopListening<OnLocationSelected>(OnLocationSelected);

    }
}