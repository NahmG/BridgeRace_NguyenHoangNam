using Core;

public class PlayerIdleState : IdleState
{
    public PlayerIdleState(CoreSystem core) : base(core)
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }
}

public class PlayerMoveState : MoveState
{
    public PlayerMoveState(CoreSystem core) : base(core)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Core.SENSOR.IsGoUpBridge)
        {

        }
    }

    public override void Exit()
    {
    }
}

public class PlayerInAirState : InAirState
{
    public PlayerInAirState(CoreSystem core) : base(core)
    {
    }

    public override STATE Id => STATE.IN_AIR;

    public override void Exit()
    {
    }
}

