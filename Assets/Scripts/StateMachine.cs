public interface IState
{
    void Update() { }
    void FixedUpdate() { }
    void LateUpdate() { }
    void Enter() { }
    void Exit() { }

}

public class StateMachine<T> where T : IState
{
    public IState currentState { get; private set; }
    
    public StateMachine(IState startingState = null)
    {
        if (startingState == null) return;
        currentState = startingState;
        currentState.Enter();
    }
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        if (currentState != null)
            currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void RunLateUpdateLogic()
    {
        currentState?.LateUpdate();
    }

    
    public void ForceExit()
    {
        currentState?.Exit();
        currentState = null;
    }
}