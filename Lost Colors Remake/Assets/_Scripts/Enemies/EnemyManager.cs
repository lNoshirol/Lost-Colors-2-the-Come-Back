using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonCreatorPersistant<EnemyManager>
{
    public List<GameObject> CurrentEnemyList = new();
    public Dictionary<GameObject, bool> WorldEnemyDic = new();
    [SerializeField] private List<GameObject> glyphPrefabList = new List<GameObject>();
    public Dictionary<string, Pool> glyphPrefabPool = new();
    public List<GameObject> GlyphPoolList = new();
    

    private void Start()
    {
        foreach (GameObject glyph in glyphPrefabList)
        {
            GameObject parent = new(glyph.name + " List");
            parent.transform.parent = this.transform;
            Pool newPool = new(glyph, 10, parent.transform);
            glyphPrefabPool.Add(glyph.name, newPool);
            GlyphPoolList.Add(parent);
        }
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
        if (FindCloserEnemy() != null)
        {

            var enemy = FindCloserEnemy();
            if (enemy.TryGetComponent<EnemyMain>(out var enemiesMain))
            {
                enemiesMain.Armor.RemoveGlyph(glyphName);
            }
            else if (enemy.TryGetComponent<CrystalMain>(out var crystalMain))
            {
                crystalMain.Armor.RemoveGlyph(glyphName);
            }
        }
        else
        {
            Debug.Log("No enemy on screen with armor");
        }
        
    }

    public GameObject FindCloserEnemy()
    {
        GameObject closerEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach(GameObject enemy in CurrentEnemyList)
        {
            float actualDist = Vector2.Distance(enemy.transform.position, PlayerMain.Instance.transform.position);
            if (actualDist < minDistance)
            {
                if (enemy.TryGetComponent<EnemyMain>(out var eMain))
                {
                    if (eMain.Armor.activeGlyphs.Count > 0 &&
                        eMain.spriteRenderer.TryGetComponent<Renderer>(out var renderer) &&
                        renderer.isVisible)
                    {
                        closerEnemy = enemy;
                        minDistance = actualDist;
                    }
                }
                else if (enemy.TryGetComponent<CrystalMain>(out var cMain))
                {
                    if (cMain.Armor.activeGlyphs.Count > 0 &&
                        cMain.spriteRenderer.TryGetComponent<Renderer>(out var renderer) &&
                        renderer.isVisible)
                    {
                        closerEnemy = enemy;
                        minDistance = actualDist;
                    }
                }
            }
        }
        return closerEnemy;
    }
}