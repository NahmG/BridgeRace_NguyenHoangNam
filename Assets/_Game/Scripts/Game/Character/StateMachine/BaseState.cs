using System;

public enum STATE
{
    NONE,
    IDLE,
    MOVE,
    IN_AIR
}

[Serializable]
public abstract class BaseState
{
    public event Action<STATE> _OnStateChanged;
    public abstract STATE Id { get; }
    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
    protected void ChangeState(STATE newState)
    {
        _OnStateChanged?.Invoke(newState);
    }
}

