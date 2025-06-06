using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageAfterDraw : MonoBehaviour
{
    public static ApplyDamageAfterDraw Instance;
    
    [SerializeField] private List<DamageStructModel> _damageToEnemy = new ();
    [SerializeField] private List<string> _enemyArmorToTej = new();

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

    public void AddEnemyGlyphToTej(string glyphName)
    {
        _enemyArmorToTej.Add(glyphName);
    }

    public void ApplyDamage()
    {
        Debug.Log($"tabasse tout le monde {_damageToEnemy.Count}");
        
        _tabassedEnemy.Clear();

        foreach(DamageStructModel model in _damageToEnemy)
        {
            model.targetedEnemy.EnemyLoseHP(model.damage);
            _tabassedEnemy.Add(model.targetedEnemy);
        }

        _damageToEnemy.Clear();
    }

    public void TejArmor()
    {
        Debug.Log("Tej armor");

        foreach(string glyphName in _enemyArmorToTej)
        {
            EnemyManager.Instance.ArmorLost(glyphName);
        }

        _enemyArmorToTej.Clear();
    }
}