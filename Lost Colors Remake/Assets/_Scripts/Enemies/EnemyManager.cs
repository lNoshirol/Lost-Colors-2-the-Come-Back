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
    }

    private void Start()
    {
        Debug.Log(FindCloserEnemy());
    }

    public void AddEnemiesToListAndDic(GameObject enemy, bool isColorized)
    {
        AddEnemiesToWorldDic(enemy);
        AddCurrentEnemiesInRoom(enemy, isColorized);
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

    public void AddCurrentEnemiesInRoom(GameObject currentEnemies, bool isColorized)
    {
        if (isColorized) return;
        CurrentEnemyList.Add(currentEnemies);
    }

    public void UpdateEnemyWorldDic()
    {

    }

    public void ArmorLost(string glyphName)
    {
        FindCloserEnemy().GetComponent<EnemyArmor>().RemoveGlyph(glyphName);
    }

    public GameObject FindCloserEnemy()
    {
        GameObject closerEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach(GameObject enemy in CurrentEnemyList)
        {
            float actualDist = Vector2.Distance(enemy.transform.position, PlayerMain.Instance.transform.position);
            if(actualDist < minDistance)
            {
                closerEnemy = enemy;
                minDistance = actualDist;
            }
        }
        return closerEnemy;
    }
}