using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;

    public float enemyCurrentHealth;

    [SerializeField] EnemiesMain EnemyMain;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerMain.Instance.Health.PlayerLoseHP(1);
        }
    }
    public void EnemyHealthChange(float healthChangeAmount)
    {
        Debug.Log(EnemyMain.isColorized);
        if (EnemyMain.isColorized)
        {
            return;
        }

        if (EnemyMain.Armor.activeGlyphs.Count == 0) {
            enemyCurrentHealth -= healthChangeAmount;
        }
        
        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            EnemyIsDead();
        }
        else if (enemyCurrentHealth >= 100)
        {
            enemyCurrentHealth = 100;
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
