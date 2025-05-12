using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    // spell name --> spell IDs
    public Dictionary<string, string> SpellsIDs { get; private set; } = new()
    {
        { "FireBall", "FireBall;Circle;E50037" },
        { "SimpleDash", "SimpleDash;Spirale;0430A2" }
    };

    // V2 : que des strings du coup le dico est utile, et on parse les clés avec des fonctions
    public Dictionary<string, SkillParentClass> Spells { get; private set; } = new() {
        { "FireBall;Circle;E50037", new FireBall() },
        { "SimpleDash;Spirale;0430A2", new SimpleDash() }
    };

    // Singleton
    #region Singleton
    private static SpellManager _instance;

    public static SpellManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Spell Manager");
                _instance = go.AddComponent<SpellManager>();
                Debug.Log("<color=#8b59f0>SpellManager</color> instance <color=#58ed7d>created</color>");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("<color=#8b59f0>SpellManager</color> instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    #region SpellDetectionMethods

    /// <summary>
    /// Takes spell name, gives spell ID.
    /// </summary>
    /// <param name="spellName"></param>
    /// <returns></returns>
    public string SpellNameToSpellID(string spellName)
    {
        print(SpellsIDs[spellName]);
        return SpellsIDs[spellName];
    }

    /// <summary>
    /// Takes spell ID, gives spell name.
    /// </summary>
    /// <param name="spellID"></param>
    /// <returns></returns>
    public string ToSpell(string spellID)
    {
        return spellID.Split(';')[0];
    }

    /// <summary>
    /// Takes spell ID, gives spell shape.
    /// </summary>
    /// <param name="spellID"></param>
    /// <returns></returns>
    public string ToShape(string spellID)
    {
        return spellID.Split(';')[1];
    }

    /// <summary>
    /// Takes spell ID, gives spell color.
    /// </summary>
    /// <param name="spellID"></param>
    /// <returns></returns>
    public string ToColor(string spellID)
    {
        return spellID.Split(';')[2];
    }
    #endregion 

    public void PROTOGiveSimpleDash()
    {
        PlayerMain.Instance.Inventory.SpellDataBase["SimpleDash"] = true;
    }

    public SkillParentClass GetSpell(string spellName)
    {
        SkillParentClass spell = (PlayerMain.Instance.Inventory.SpellDataBase["SimpleDash"]) ? Spells[SpellsIDs[spellName]] : null;
        return spell;
    }
}