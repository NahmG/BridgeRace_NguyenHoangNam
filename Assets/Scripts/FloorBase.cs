using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class FloorBase : MonoBehaviour
{
    public MiniPool Pool { get; set; }
    public List<Vector3> Pos { get; set; }

    public Bridge[] bridges;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {

    }

    public virtual void PreLoad(MiniPool pool, int amount, Transform parent)
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Brick/");

        for (int i = 0; i < gameUnits.Length; i++)
        {
            pool.PreLoad(gameUnits[i], amount, parent);
        }
    }

    public virtual void Spawn(Color color)
    {
        
    }

}


