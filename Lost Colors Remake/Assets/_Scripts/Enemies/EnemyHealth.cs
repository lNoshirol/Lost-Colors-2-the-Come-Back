using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] EnemyMain EnemyMain;
    bool enemyHealthSetup = false;
    public float enemyCurrentHealth;
    private float maxHealth;
    private Coroutine healthRecupCoro;

    private void SetupHealthCount()
    {
        if (!enemyHealthSetup)
        {
            enemyCurrentHealth = EnemyMain.Stats.maxHp;
            maxHealth = EnemyMain.Stats.maxHp;

            enemyHealthSetup = true;
        }
    }
    public void EnemyLoseHP(float healthLoose)
    {
        SetupHealthCount();
        if(healthRecupCoro != null)
        StopCoroutine(healthRecupCoro);
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
        healthRecupCoro = StartCoroutine(HealthRecup(3f, 1.5f));
        EnemyMain.UI.UpdateEnemyHealthUI();
    }

    private void EnemyIsDead()
    {
        Debug.Log("Enemy dead");
        PlayerMain.Instance.Health.PlayerGainHP(0.5f);
        EnemyMain.ColorSwitch();
        EnemyMain.UI.SwitchHealtBar(false);
    }

    IEnumerator HealthRecup(float firstRecupTime, float recupTimeBetween)
    {
        yield return new WaitForSeconds(firstRecupTime);
        while (enemyCurrentHealth < maxHealth)
        {
            enemyCurrentHealth += 1f;
            if (enemyCurrentHealth > maxHealth)
            {
                enemyCurrentHealth = maxHealth;
            }
            EnemyMain.UI.UpdateEnemyHealthUI();
            yield return new WaitForSeconds(recupTimeBetween);
        }
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
