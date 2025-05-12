using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Scriptable")]
    public EnemyDATA enemyData;
    [Header("Globale Stats")]
    public bool isAlive;

    [SerializeField] 

    void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject);
    }


}
