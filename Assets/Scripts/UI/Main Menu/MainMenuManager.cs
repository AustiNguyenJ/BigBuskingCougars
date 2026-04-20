using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class MainMenuManager : MonoBehaviour
{
    [Tooltip("First State is Default State which should be OnMainMenu")]
    [SerializeReference] 
    public List<MenuState> menuStates = new List<MenuState>();
    MenuState currentMenuState;
    
    
    [ReadOnly, ShowInInspector]
    string currentStateName => currentMenuState != null ? currentMenuState.GetType().ToString() : "Null";

    void Start()
    {
        foreach (var state in menuStates)
        {
            state.SetManager(this);
        }
        
        ChangeState(menuStates[0]);
    }
    
    public void ChangeState(MenuState newState)
    {
        if (Validate.AnyNull(newState)) return;
        
        if (currentMenuState != null)
            currentMenuState.OnExit();
        currentMenuState = newState;
        currentMenuState.OnEnter();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}