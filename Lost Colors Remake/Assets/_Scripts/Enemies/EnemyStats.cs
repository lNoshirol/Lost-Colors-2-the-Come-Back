using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Scriptable")]
    public EnemyDATA enemyData;
    [Header("Globale Stats")]
    public bool isAlive;

    [SerializeField] EnemiesMain EnemiesMain;

    void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject);
    }

    void Setup()
    {
    }


}
