using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] Character[] characterPrefabs;
    [SerializeField] List<Character> activeCharacters = new List<Character>();

    public Level CurrentLevel {  get; private set; }
    public int levelIndex {  get; private set; }

    private void Awake()
    {
        characterPrefabs = Resources.LoadAll<Character>("Character/");
        foreach (Character character in characterPrefabs)
        {
            activeCharacters.Add(Instantiate(character));
        }
    }

    private void Start()
    {
        levelIndex = 0;
    }

    public void OnInit()
    {
        foreach (Character c in activeCharacters)
        {
            c.OnInit();
        }
    }

    public void OnDespawn()
    {
        if (activeCharacters.Count > 0)
        {
            foreach (Character c in activeCharacters)
            {
                c.OnDespawn();
            }
        }
    }

    public void OnLoadLevel(int level)
    {
        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel.gameObject);
        }

        CurrentLevel = Instantiate(levels[level]);

        OnDespawn();
        Invoke(nameof(OnInit), .1f);
    }

    public void LoadNextLevel()
    {
        if (levelIndex < levels.Length-2)
        {
            levelIndex++;
            OnLoadLevel(levelIndex);  
        }
        else { CurrentLevel = null; }

    }
    public void LoadCurrentLevel()
    {
        OnLoadLevel(levelIndex);
    }
}
