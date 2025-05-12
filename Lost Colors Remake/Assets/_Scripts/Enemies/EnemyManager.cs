using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> CurrentEnemyList = new();
    public Dictionary<GameObject, bool> WorldEnemyDic = new();

    public static EnemyManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddEnemiesToListAndDic(GameObject enemy)
    {
        AddEnemiesToWorldDic(enemy);
        AddCurrentEnemiesInRoom(enemy);
    }

    public void AddEnemiesToWorldDic(GameObject enemy)
    {
        foreach(var enemyDic in WorldEnemyDic)
        {
            if(!enemyDic.Key == enemy)
            {
                WorldEnemyDic.Add(enemy, true);
            }
        }
    }

    public void AddCurrentEnemiesInRoom(GameObject currentEnemies)
    {
        CurrentEnemyList.Add(currentEnemies);
    }

    public void UpdateEnemyWorldDic()
    {

    }
}