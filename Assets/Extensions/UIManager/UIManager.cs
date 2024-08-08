using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UICanva> canvaActives = new Dictionary<System.Type, UICanva>();
    Dictionary<System.Type, UICanva> canvaPrefabs = new Dictionary<System.Type, UICanva>();
    [SerializeField] Transform canvaParent;

    private void Awake()
    {
        UICanva[] prefabs = Resources.LoadAll<UICanva>("UI/");
        for (int i = 0; i < prefabs.Length; i++)
        {
            canvaPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
        }
    }

    public T Open<T>() where T : UICanva
    {
        T canva = GetUI<T>();

        canva.SetUp();
        canva.Open();

        return canva;
    }

    public void Close<T>(float time) where T : UICanva
    {
        if (IsOpened<T>())
        {
            canvaActives[typeof(T)].Close(time);
        }
    }

    public void CloseDirectly<T>() where T : UICanva
    {
        if (IsOpened<T>())
        {
            canvaActives[typeof(T)].CloseDirectly();
        }
    }

    public bool IsLoaded<T>() where T : UICanva
    {
        return canvaActives.ContainsKey(typeof(T)) && canvaActives[typeof(T)] != null;
    }

    public bool IsOpened<T>() where T : UICanva
    {
        return IsLoaded<T>() && canvaActives[typeof(T)].gameObject.activeSelf;
    }

    public T GetUI<T>() where T : UICanva
    {
        if (!IsLoaded<T>())
        {
            T prefab = GetUIPrefab<T>();
            T canva = Instantiate(prefab, canvaParent);
            canvaActives[typeof(T)] = canva;
        }

        return canvaActives[typeof(T)] as T;
    }

    public T GetUIPrefab<T>() where T : UICanva
    {
        return canvaPrefabs[typeof(T)] as T;
    }

    public void CloseAll()
    {
        foreach ( var item in canvaActives)
        {
            if(item.Value != null && item.Value.gameObject.activeSelf)
            {
                item.Value.Close(0);
            }
        }
    }

}
