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
    
    public override void OnEnter()
    {
        mainMenuGroup.alpha = 1;
        mainMenuGroup.blocksRaycasts = true;
        mainMenuGroup.interactable = true;
        GlobalEventAsset.Instance.StartListening<OnPlayButtonSelected>(OnPlayButtonSelected);
    }
    
    public void OnPlayButtonSelected()
    {
        manager.ChangeToLocationSelection();
    }

    public override void OnExit()
    {
        GlobalEventAsset.Instance.StopListening<OnPlayButtonSelected>(OnPlayButtonSelected);

        if (mainMenuGroup == null) return;
        
        mainMenuGroup.alpha = 0;
        mainMenuGroup.blocksRaycasts = false;
        mainMenuGroup.interactable = false;
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
        GlobalEventAsset.Instance.StartListening<OnBackButtonSelected>(OnBackButtonSelected);
    }
    
    public override void OnExit()
    {
        locationSelectionGroup.alpha = 0;
        locationSelectionGroup.blocksRaycasts = false;
        locationSelectionGroup.interactable = false;
        GlobalEventAsset.Instance.StopListening<OnLocationSelected>(OnLocationSelected);
        GlobalEventAsset.Instance.StopListening<OnBackButtonSelected>(OnBackButtonSelected);
    }
    

    void OnLocationSelected(OnLocationSelected data)
    {
        GlobalEventAsset.Instance.TriggerEvent(new RequestSceneLoadEvent { sceneGroupToLoad = data.locationSceneGroupAsset });
    }

    void OnBackButtonSelected()
    {
        manager.ChangeToPreviousState();
    }

}