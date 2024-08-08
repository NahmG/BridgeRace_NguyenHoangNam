using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaVictory : UICanva
{
    public void ButtonMainMenu()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    public void ButtonNextLevel()
    {
        LevelManager.Ins.LoadNextLevel();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }
}
