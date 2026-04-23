using System;
using Events.UI;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public abstract class WristMenuState : IState
{
    protected WristMenuController controller;
    
    public void Initialize(WristMenuController controller) => this.controller = controller;
    
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}

[Serializable]
public class ViewingDefaultMenu : WristMenuState
{
    [SerializeField] CanvasGroup defaultMenuGroup;

    public override void Enter()
    {
        defaultMenuGroup.ShowGroup(true);
    }

    public override void Exit()
    {
        defaultMenuGroup.ShowGroup(false);
    }

}

[Serializable]
public class LocationsMenu : WristMenuState
{
    [SerializeField] CanvasGroup locationsMenuGroup;

    public override void Enter()
    {
        locationsMenuGroup.ShowGroup(true);
    }

    public override void Exit()
    {
        locationsMenuGroup.ShowGroup(false);
    }
    
}

public class WristMenuController : MonoBehaviour
{
    [BoxGroup("References")]
    public Transform playerCamera;
    [BoxGroup("Settings")]
    public float activationAngle = 45f;
    [BoxGroup("States")]
    [SerializeReference] public WristMenuState viewingDefaultMenu;

    [BoxGroup("States")] 
    [SerializeReference] public WristMenuState locationsMenu;
    StateMachine<WristMenuState> stateMachine = new StateMachine<WristMenuState>();
    public WristMenuState previousState { get; private set; }
    
    void Awake()
    {
        viewingDefaultMenu.Initialize(this);
        locationsMenu.Initialize(this);
    }

    void Update()
    {
        CheckMenuVisibility();
    }

    void CheckMenuVisibility()
    {
        Vector3 cameraForward = playerCamera.forward;
        Vector3 menuUp = transform.forward;

        float angle = Vector3.Angle(cameraForward, menuUp);

        // if player is looking at watch and it is off, turn it on
        if (stateMachine.currentState == null)
        {
            if (angle < activationAngle)
            {
                ChangeState(viewingDefaultMenu);
            }
        }
        // if player is not looking at watch and it is on, turn it off
        else
        {
            if (angle > activationAngle)
            {
                stateMachine.ForceExit();
            }
        }
    }

    public void ChangeToPreviousState() => ChangeState(previousState);
    public void ChangeToLocationMenu() => ChangeState(locationsMenu);

    void ChangeState(WristMenuState newState)
    {
        if (Validate.AnyNull(newState)) return;
        
        if (stateMachine.currentState != null)
            previousState = stateMachine.currentState as WristMenuState;
        stateMachine.ChangeState(newState);
    }
}