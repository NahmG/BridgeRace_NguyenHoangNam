using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaSetting : UICanva
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] UICanva currentCanvas;

    public void SetState(UICanva canvas)
    { 
        currentCanvas = canvas;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
                
        if(canvas is CanvaMainMenu)
        {
            buttons[2].SetActive(true);
        }
        else if(canvas is CanvaGameplay)
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
        }
    }

    public void ButtonClose()
    {
        UIManager.Ins.Close<CanvaSetting>(0);

        if (currentCanvas is CanvaMainMenu)
        {
            GameManager.Ins.ChangeState(GameState.MainMenu);
        }
        if (currentCanvas is CanvaGameplay)
        {
            GameManager.Ins.ChangeState(GameState.GamePlay);
        }
    }

    public void ButtonMainMenu()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
