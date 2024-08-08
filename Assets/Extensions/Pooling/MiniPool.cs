using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPool 
{
    private List<GameUnit> inactives = new List<GameUnit>(); 
    private List<GameUnit> actives = new List<GameUnit>();

    public List<GameUnit> Inactives {  get { return inactives; } }
    public List<GameUnit> Active { get { return actives; } }

    public void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError(prefab + " is null!");
            return;
        }
        for (int i = 0; i < amount; i++)
        {
            DeSpawn(GameObject.Instantiate(prefab, parent));
        }
    }

    public void Spawn(GameUnit unit ,Vector3 pos, Quaternion rot)
    {
        inactives.Remove(unit);

        unit.TF.SetLocalPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);
    }


    public void DeSpawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Add(unit);
            unit.gameObject.SetActive(false);
        }
    }

    public void Collect()
    {
        while (actives.Count > 0)
        {
            DeSpawn(actives[0]);
        }
    }

    public void Release()
    {
        Collect();

        for (int i = 0; i < inactives.Count; i++)
        {
            GameObject.Destroy(inactives[0].gameObject);
        }
        inactives.Clear();
    }
}

public enum BColor
{
    red, green, blue, yellow
}
