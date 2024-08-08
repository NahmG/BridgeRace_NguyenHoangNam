using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor3 : FloorBase
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

        for (float i = -3.5f; i < 4.5f; i++)
        {
            for (float j = -3.5f; j < 2.5f; j++)
            {
                Pos.Add(new Vector3(i, 0, j));
            }
        }
    }

    public override void Spawn(Color color)
    {
        base.Spawn(color);

        if (Pool.Inactives.Count > 0)
        {
            foreach (GameUnit unit in Pool.Inactives.ToArray())
            {
                if (unit.Color.color == color)
                {
                    int rnd = Random.Range(0, Pos.Count);
                    if (rnd >= Pos.Count)
                    {
                        return;
                    }
                    Pool.Spawn(unit, Pos[rnd], Quaternion.identity);
                    Pos.Remove(Pos[rnd]);
                }
            }
        }
    }
}
