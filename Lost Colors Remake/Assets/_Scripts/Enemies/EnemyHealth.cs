using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] EnemyMain EnemyMain;
    bool enemyHealthSetup = false;
    public float enemyCurrentHealth;

    private void SetupHealthCount()
    {
        if (!enemyHealthSetup)
        {
            enemyCurrentHealth = EnemyMain.Stats.maxHp;
            enemyHealthSetup = true;
        }
    }
    public void EnemyLoseHP(float healthLoose)
    {
        SetupHealthCount();
        if (EnemyMain.isColorized)
        {
            return;
        }
        if (EnemyMain.Armor.activeGlyphs.Count == 0)
        {
            enemyCurrentHealth -= healthLoose;
            JuiceManager.Instance.PlayerHit(0.25f);
            EnemyMain.UI.UpdateEnemyHealthUI();
        }

        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            EnemyIsDead();
        }
    }

    private void EnemyIsDead()
    {
        Debug.Log("Enemy dead");
        PlayerMain.Instance.Health.PlayerGainHP(0.5f);
        EnemyMain.ColorSwitch();
        EnemyMain.UI.SwitchHealtBar(false);
    }
    // NEW METHOD IN ENEMYARMOR
    //public void ArmorLost()
    //{
    //    if (enemyArmorAmount == 0)
    //    {
    //        enemyArmorAmount = 0;
    //        Debug.Log("JE PERD PAS DE LARMURE");
    //    }
    //    else
    //    {
    //        Debug.Log("JE PERD DE L'ARMURE");
    //        enemyArmorAmount--;
    //        if(enemyArmorAmount == 0)
    //        {
    //            EnemyMain.UI.SwitchGlyphToHealth();
    //        }
    //    }

    //}
}
