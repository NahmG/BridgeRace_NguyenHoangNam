using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameState _state;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public static bool IsState(GameState state) => _state == state;

    public void ChangeState(GameState newState)
    {
        _state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.GamePlay:
                GamePlay();
                break;
            case GameState.Setting:
                Setting();
                break;
            case GameState.Victory:
                Victory();
                break;
            case GameState.Fail:
                Fail();
                break;
        }
    }

    private void MainMenu()
    {
        Time.timeScale = 0f;
        UIManager.Ins.CloseAll();
        UIManager.Ins.Open<CanvaMainMenu>();
    }

    private void GamePlay()
    {
        Time.timeScale = 1f;
        UIManager.Ins.CloseAll();
        UIManager.Ins.Open<CanvaGameplay>();
    }

    private void Setting()
    {
        Time.timeScale = 0f;
    }

    private void Victory()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.Open<CanvaVictory>();
    }

    private void Fail()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.Open<CanvaFail>();
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    Setting,
    Victory,
    Fail
}
