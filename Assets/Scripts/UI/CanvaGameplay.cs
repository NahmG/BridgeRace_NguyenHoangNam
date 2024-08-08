using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvaGameplay : UICanva
{
    [SerializeField] TextMeshProUGUI levelText;


    public override void SetUp()
    {
        base.SetUp();
        SetLevel(LevelManager.Ins.levelIndex);
    }

    public void SetLevel(int index)
    {
        levelText.text = "LEVEL " + (index + 1).ToString();
    }

    public void SettingButton()
    {
        UIManager.Ins.Open<CanvaSetting>().SetState(this);
        GameManager.Ins.ChangeState(GameState.Setting);
    }
}
