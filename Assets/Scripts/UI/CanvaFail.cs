using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaFail : UICanva
{
    public void ButtonMainMenu()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    public void ButtonReplay()
    {
        LevelManager.Ins.LoadCurrentLevel();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }
}
