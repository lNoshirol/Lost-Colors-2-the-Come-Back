using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
/*    [Range(0f, 100f)]*/
    public float currentPaintAmont = 100f;
    public float basePaintAmont = 100;
    private float _amountAtStart;

    [Header("NO UPDATE IN RUNTIME")]
    [SerializeField]
    DictItem itemType;
    [SerializeField] private DictSpell LootableSpells;



    public Dictionary<ItemTypeEnum, bool> ItemDatabase;
    public Dictionary<string, bool> SpellDataBase;

    private void Awake()
    {
        basePaintAmont = PlayerMain.Instance.toileInfo.paintAmount;
        currentPaintAmont = basePaintAmont;
    }

    void Start()
    {
        ItemDatabase = itemType.ToDictionary();
        SpellDataBase = LootableSpells.ToDictionary();
    }

    public void AddItemToInventory(ItemTypeEnum type)
    {
        ItemDatabase[type] = true;
        Debug.Log("Item loot : " + type);
    }

    public bool CheckBrushInDatabase()
    {
        bool brushIsLooted = false;
        if (ItemDatabase[ItemTypeEnum.Paintbrush] == true)
        {
            brushIsLooted = true;
        }
        return brushIsLooted;
    }

    public void EditPaintAmount()
    {
        currentPaintAmont = Mathf.Clamp(_amountAtStart - ExternalDrawFunctions.GetDrawLenght(DrawForDollarP.instance.GetDrawPoints()), 0f, 100f);
    }

    public void SetStartAmount()
    {
        _amountAtStart = currentPaintAmont;
    }

    public void ResetCurrentPaintAmount()
    {
        currentPaintAmont = basePaintAmont;
        ToileMain.Instance.ToileUI.UpdateToilePaintAmount();
    }
}

[Serializable]
public class DictItem
{
    [SerializeField]
    DictItemType[] thisDictItems;

    public Dictionary<ItemTypeEnum, bool> ToDictionary()
    {
        Dictionary<ItemTypeEnum, bool> itemDatabase = new Dictionary<ItemTypeEnum, bool>();
        foreach (var item in thisDictItems)
        {
            itemDatabase.Add(item.type, item.isLooted);
        }
        return itemDatabase;

    }
}

[Serializable]
public class DictItemType
{
    [SerializeField]
    public ItemTypeEnum type;
    [SerializeField]
    public bool isLooted;
}

[Serializable]
public class DictSpell
{
    [SerializeField] private DictSpellItem[] spells;
    public Dictionary<string, bool> ToDictionary()
    {
        Dictionary<string, bool> spellDatabase = new Dictionary<string, bool>();
        foreach (var spell in spells)
        {
            spellDatabase.Add(spell.spellName, spell.isLooted);
        }
        return spellDatabase;

    }

}

[Serializable]
public class DictSpellItem
{
    public string spellName;
    public bool isLooted;
}