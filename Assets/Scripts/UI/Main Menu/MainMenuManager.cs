using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class MainMenuManager : MonoBehaviour
{
    [SerializeReference] MenuState OnMainMenu;
    [SerializeReference] MenuState LocationSelection;

    MenuState prevMenuState;
    MenuState currentMenuState;
    
    
    [ReadOnly, ShowInInspector]
    string currentStateName => currentMenuState != null ? currentMenuState.GetType().ToString() : "Null";

    void Awake()
    {
        // provide each state a reference to the manager for changing states
        OnMainMenu.SetManager(this);
        LocationSelection.SetManager(this);
    }

    void Start()
    {
        ChangeToOnMainMenu();   
    }

    public void ChangeToOnMainMenu() => ChangeState(OnMainMenu);
    public void ChangeToLocationSelection() => ChangeState(LocationSelection);
    public void ChangeToPreviousState() => ChangeState(prevMenuState);
    
    
    void ChangeState(MenuState newState)
    {
        if (Validate.AnyNull(newState)) return;
        
        if (currentMenuState != null)
        {
            currentMenuState.OnExit();
            prevMenuState = currentMenuState;
        }
        currentMenuState = newState;
        currentMenuState.OnEnter();
    }

    void OnDestroy()
    {
        if (currentMenuState != null)
            currentMenuState.OnExit();
        currentMenuState = null;
    }
}