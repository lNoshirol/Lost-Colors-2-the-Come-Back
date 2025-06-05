using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageAfterDraw : MonoBehaviour
{
    public static ApplyDamageAfterDraw Instance;
    
    [SerializeField] private List<DamageStructModel> _damageToEnemy = new ();

    public int damageToEnemyCount = 0;
    public List<EnemyHealth> _tabassedEnemy = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        damageToEnemyCount = _damageToEnemy.Count;
    }

    public void AddEnnemyDamage(EnemyHealth enemy, float damage)
    {
        _damageToEnemy.Add(new(enemy, damage));
    }

    public void ApplyDamage()
    {
        Debug.Log("tabasse tout le monde");
        
        _tabassedEnemy.Clear();

        foreach(DamageStructModel model in _damageToEnemy)
        {
            model.targetedEnemy.EnemyLoseHP(model.damage);
            _tabassedEnemy.Add(model.targetedEnemy);
        }

        _damageToEnemy.Clear();
    }
}