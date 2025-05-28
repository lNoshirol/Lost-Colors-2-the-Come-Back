using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmor : MonoBehaviour
{
    //public GameObject glyphPrefab;
    public List<GameObject> activeGlyphs = new List<GameObject>();
    [SerializeField] EnemyMain EnemiesMain;
    int enemyArmorCount;

    private void Start()
    {
        enemyArmorCount = EnemiesMain.Stats.maxArmor;

    }

    public GameObject SearchArmorInPool(string glyphName)
    {
        foreach (KeyValuePair<string, Pool> glyph in EnemyManager.Instance.glyphPrefabPool)
        {
            if (glyph.Key.Contains(glyphName))
            {
                return glyph.Value.GetObject();
            }
        }
        return null;
    }

    public void RePackInPool(GameObject glyph)
    {
        foreach(GameObject parent in EnemyManager.Instance.GlyphPoolList)
        {
            string cleanGlyphName = glyph.name.Replace("(Clone)", "");

            if (parent.name.Contains(cleanGlyphName))
            {
                glyph.transform.parent = parent.transform;
                glyph.transform.position = parent.transform.position;
                glyph.SetActive(false);
            }
        }
    }


    public void AddGlyph()
    {
        while (enemyArmorCount > 0)
        {
            int index = UnityEngine.Random.Range(0, EnemiesMain.Stats.armorList.Count);
            GameObject newGlyph = SearchArmorInPool(EnemiesMain.Stats.armorList[index].name);
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
        RePackInPool(last);
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
}