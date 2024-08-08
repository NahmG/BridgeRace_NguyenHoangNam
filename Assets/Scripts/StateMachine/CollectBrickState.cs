using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBrickState : IState
{

    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
    }

    public void OnExecute(Bot bot)
    {
        bot.FindBrick();
        int rnd = Random.Range(12, 18);

        if(bot.BrickCollected >= rnd)
        {
            bot.ChangeState(new BuildBridgeState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
