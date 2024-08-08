using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTargetState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
    }

    public void OnExecute(Bot bot)
    {     
        if(bot.Target!=null && bot.BrickCollected < bot.Target.BrickCollected)
        {
            bot.ChangeState(new CollectBrickState());
        }
        bot.FindTarget();
    }

    public void OnExit(Bot bot)
    {

    }
}