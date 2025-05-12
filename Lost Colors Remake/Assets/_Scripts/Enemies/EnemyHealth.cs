using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyMaxHealth;

    public float enemyCurrentHealth;

    public string enemyArmorId;

    public float enemyArmorAmount;
    [SerializeField] EnemiesMain EnemyMain;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackArea"))
        {
            EnemyHealthChange(PlayerMain.Instance.Attack.attackDamageAmount);
            EnemyMain.UI.UpdateEnemyHealthUI();
        }
    }
    public void EnemyHealthChange(float healthChangeAmount)
    {

        if (enemyArmorAmount == 0) {
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
        gameObject.SetActive(false);
        PlayerMain.Instance.Health.PlayerHealthChange(-50);
    }

    public void ArmorLost()
    {
        if (enemyArmorAmount == 0)
        {
            enemyArmorAmount = 0;
            Debug.Log("JE PERD PAS DE LARMURE");
        }
        else
        {
            Debug.Log("JE PERD DE L'ARMURE");
            enemyArmorAmount--;
            if(enemyArmorAmount == 0)
            {
                EnemyMain.UI.SwitchGlyphToHealth();
            }
        }

    }
}
