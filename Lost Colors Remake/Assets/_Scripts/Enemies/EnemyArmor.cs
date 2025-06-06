using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyArmor : MonoBehaviour
{
    //public GameObject glyphPrefab;
    public List<GameObject> activeGlyphs = new List<GameObject>();
    [SerializeField] EnemyMain EnemiesMain;
    public int enemyArmorCount;
    bool enemyArmorCountSetup;

    private void SetupArmorCount()
    {
        if (!enemyArmorCountSetup)
        {
            enemyArmorCount = EnemiesMain.Stats.maxArmor;
            enemyArmorCountSetup = true;
        }
    }

    public void AddGlyph()
    {
        SetupArmorCount();
        while (enemyArmorCount > 0)
        {
            int index = UnityEngine.Random.Range(0, EnemiesMain.Stats.armorList.Count);
            GameObject newGlyph = EnemyManager.Instance.SearchInPool(EnemiesMain.Stats.armorList[index].name, EnemyManager.Instance.glyphPool);
            newGlyph.transform.parent = transform;
            activeGlyphs.Add(newGlyph);
            UpdateGlyphPositions();
            enemyArmorCount--;
        }
    }

    public void RemoveGlyph(string glyphName)
    {
        if (activeGlyphs.Count == 0) return;

        GameObject last = activeGlyphs[activeGlyphs.Count - 1];
        Debug.Log(last.name);
        if (!last.name.Contains(glyphName))
        {
            return;
        }
        activeGlyphs.RemoveAt(activeGlyphs.Count - 1);
        EnemyManager.Instance.RePackInPool(last, EnemyManager.Instance.glyphPool);
        UpdateGlyphPositions();
        if (activeGlyphs.Count == 0) EnemiesMain.UI.SwitchHealtBar(true);
    }

    void UpdateGlyphPositions()
    {
        float spacing = 1f;
        float totalWidth = (activeGlyphs.Count - 1) * spacing;
        for (int i = 0; i < activeGlyphs.Count; i++)
        {
            float x = i * spacing - totalWidth / 2f;
            activeGlyphs[i].transform.localPosition = new Vector3(x, 0, 0);
        }
    }

    public bool IsGlyphInArmorList(string glyphName)
    {
        foreach (GameObject glyph in activeGlyphs)
        {
            if (glyph.name.Contains(glyphName))
            {
                return true;
            }
        }
        return false;
    }
}