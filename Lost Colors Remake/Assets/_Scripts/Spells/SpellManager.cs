using System.Collections.Generic;
using System.Threading.Tasks;

public class SpellManager : AsyncSingletonPersistent<SpellManager>
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

    protected override async Task OnInitializeAsync()
    {
        await Bootstrap.Instance.WaitUntilInitializedAsync();
    }


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