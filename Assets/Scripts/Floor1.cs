using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Floor1 : FloorBase
{
    [SerializeField] int amount;
    [SerializeField] Transform parent;

    private void Awake()
    {
        Pool = new MiniPool();
        Pos = new List<Vector3>();
        PreLoad(Pool, amount, parent);
    }

    public override void OnInit()
    {
        base.OnInit();

        for (float i = -4.5f; i < 5.5f; i++)
        {
            for (float j = -4; j < 4; j++)
            {
                Pos.Add(new Vector3(i, 0, j));
            }
        }

        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            GameUnit unit;
            int rnd = Random.Range(0, Pool.Inactives.Count);
            if (rnd >= Pool.Inactives.Count)
            {
                return;
            }
            unit = Pool.Inactives[rnd];

            Pool.Spawn(unit, Pos[i], Quaternion.identity);
        }
    }
}
