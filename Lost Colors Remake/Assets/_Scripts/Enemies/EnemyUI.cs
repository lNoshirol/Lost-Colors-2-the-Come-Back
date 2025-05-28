using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Canvas enemyCanvas;
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private EnemyMain enemiesMain;
    //[SerializeField] private Image enemyGlyphe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        if (enemiesMain.Armor.activeGlyphs.Count > 0)
        {
            healthbarSprite.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            healthbarSprite.transform.parent.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        enemyCanvas.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    public void UpdateEnemyHealthUI()
    {
        healthbarSprite.fillAmount = (float)enemiesMain.Health.enemyCurrentHealth / enemiesMain.Stats.maxHp;
    }


    public void SwitchHealtBar(bool OffOrOn)
    {

        if(!OffOrOn)
        healthbarSprite.transform.parent.parent.gameObject.SetActive(false);
        else
        {
            healthbarSprite.transform.parent.parent.gameObject.SetActive(true);
        }
    }
}
