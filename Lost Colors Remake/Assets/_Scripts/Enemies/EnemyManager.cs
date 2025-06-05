using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonCreatorPersistent<EnemyManager>
{
    [Header("Enemies List")]
    public List<GameObject> CurrentEnemyList = new();
    public Dictionary<GameObject, bool> WorldEnemyDic = new();

    [Header("Glyph Pool")]
    [SerializeField] private List<GameObject> glyphPrefabList = new List<GameObject>();
    public Dictionary<string, Pool> glyphPool = new();

    [Header("VFX Pool")]
    [SerializeField] private List<GameObject> vfxPrefabList = new List<GameObject>();
    public Dictionary<string, Pool> vfxPool = new();


    protected override void Awake()
    {
        base.Awake();
        CreateEnemyPool(glyphPrefabList, glyphPool);
        CreateEnemyPool(vfxPrefabList, vfxPool);

    }

    private void CreateEnemyPool(List<GameObject> prefabList, Dictionary<string, Pool> PrefabPool)
    {
        foreach (GameObject glyph in prefabList)
        {
            GameObject parent = new(glyph.name + " List");
            parent.transform.parent = this.transform;
            Pool newPool = new(glyph, 10, parent.transform);
            PrefabPool.Add(glyph.name, newPool);
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

    public GameObject SearchInPool(string searchName, Dictionary<string, Pool> pool)
    {
        foreach (KeyValuePair<string, Pool> item in pool)
        {
            if (item.Key.Contains(searchName))
            {
                return item.Value.GetObject();
            }
        }
        return null;
    }

    public void RePackInPool(GameObject item, Dictionary<string, Pool> pool)
    {
        string cleanName = item.name.Replace("(Clone)", "");

        foreach (KeyValuePair<string, Pool> entry in pool)
        {
            if (entry.Key.Contains(cleanName))
            {
                entry.Value.Stock(item);
                return;
            }
        }

        Debug.LogWarning($"RePackInPool: Aucun pool trouvé pour l'objet {item.name}");
    }
}