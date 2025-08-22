using UnityEngine;

#region BASE STATE

using Core;

public abstract class BaseLogicState : BaseState
{
    protected CoreSystem Core;
    protected CharacterStats Stats;
    public BaseLogicState(CoreSystem core)
    {
        Core = core;
    }
}

public abstract class GroundedState : BaseLogicState
{
    protected GroundedState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (!Core.SEN.IsGrounded)
        {
            ChangeState(STATE.IN_AIR);
        }
    }
}

public abstract class IdleState : GroundedState
{
    protected IdleState(CoreSystem core) : base(core)
    {
    }

    public override STATE Id => STATE.IDLE;

    public override void Enter()
    {
        base.Enter();
        Core.DIS.ChangeAnim(CONSTANTS.IDLE_ANIM_NAME);
        Core.MOVE.SetVelocity(Vector3.zero);
    }

    public override void Update()
    {
        base.Update();
        if (Core.NAV.MoveDirection.sqrMagnitude > .01f)
        {
            ChangeState(STATE.MOVE);
        }
    }
}

public abstract class MoveState : GroundedState
{
    protected MoveState(CoreSystem core) : base(core)
    {
    }

    public override STATE Id => STATE.MOVE;

    public override void Enter()
    {
        base.Enter();
        Core.DIS.ChangeAnim(CONSTANTS.RUN_ANIM_NAME);
    }

    public override void Update()
    {
        if (Core.NAV.MoveDirection.sqrMagnitude < .01f)
        {
            ChangeState(STATE.IDLE);
        }
    }

    public override void FixedUpdate()
    {
        if (Core.NAV.MoveDirection.sqrMagnitude < .01f)
            return;

        Vector3 move = Core.NAV.MoveDirection;
        move.y = 0;

        Core.DIS.SetSkinRotation(Quaternion.LookRotation(move), true);
        Core.MOVE.SetVelocity(move * Core.Stats.Speed.Value);
    }
}

public abstract class InAirState : BaseLogicState
{
    protected InAirState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (Core.SEN.IsGrounded)
        {
            if (Core.NAV.MoveDirection.sqrMagnitude > 0.01f)
                ChangeState(STATE.MOVE);
            else
                ChangeState(STATE.IDLE);
        }
    }

    public override void FixedUpdate()
    {
        Core.MOVE.ApplyGravity(1);
    }
}
#endregion

