using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState
{

    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        bot.ResetIndex();
    }

    public void OnExecute(Bot bot)
    {

        bot.GoOnBridge();
        if (bot.BrickCollected <= 0)
        {
            bot.ChangeState(new CollectBrickState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
