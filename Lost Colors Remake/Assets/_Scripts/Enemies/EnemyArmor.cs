using System.Collections.Generic;
using UnityEngine;

public class EnemyArmor : MonoBehaviour
{
    public GameObject glyphPrefab;
    public List<GameObject> activeGlyphs = new List<GameObject>();
    [SerializeField] EnemiesMain EnemiesMain;
    int enemyArmorCount;

    private void Start()
    {
        enemyArmorCount = EnemiesMain.Stats.maxArmor;

    }

    public void AddGlyph()
    {
        while (enemyArmorCount > 0)
        {
            int index = Random.Range(0, EnemiesMain.Stats.armorList.Count);
            GameObject newGlyph = Instantiate(EnemiesMain.Stats.armorList[index], transform);
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
        Destroy(last);
        UpdateGlyphPositions();
        EnemiesMain.UI.SwitchHealtBar(true);

    }

    void UpdateGlyphPositions()
    {
        float spacing = 0.5f;
        float totalWidth = (activeGlyphs.Count - 1) * spacing;
        for (int i = 0; i < activeGlyphs.Count; i++)
        {
            float x = i * spacing - totalWidth / 2f;
            activeGlyphs[i].transform.localPosition = new Vector3(x, 0, 0);
        }
    }
}