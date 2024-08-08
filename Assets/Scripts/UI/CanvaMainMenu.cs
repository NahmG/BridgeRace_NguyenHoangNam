using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaMainMenu : UICanva
{
    public void PlayButton()
    {
        Close(0);
        
        LevelManager.Ins.LoadCurrentLevel();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }

    public void SettingButton()
    {
        UIManager.Ins.Open<CanvaSetting>().SetState(this);
        GameManager.Ins.ChangeState(GameState.Setting);
    }
}
