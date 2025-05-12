using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Canvas enemyCanvas;
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private EnemiesMain enemiesMain;
    [SerializeField] private Image enemyGlyphe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        if (enemiesMain.Health.enemyArmorAmount > 0)
        {
            healthbarSprite.transform.parent.gameObject.SetActive(false);
            enemyGlyphe.gameObject.SetActive(true);
        }
        else
        {
            healthbarSprite.transform.parent.gameObject.SetActive(true);
            enemyGlyphe.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        enemyCanvas.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    public void UpdateEnemyHealthUI()
    {
        healthbarSprite.fillAmount = (float)enemiesMain.Health.enemyCurrentHealth / enemiesMain.Health.enemyMaxHealth;
    }

    public void SwitchGlyphToHealth()
    {
        healthbarSprite.transform.parent.gameObject.SetActive(true);
        enemyGlyphe.gameObject.SetActive(false);
    }
}
