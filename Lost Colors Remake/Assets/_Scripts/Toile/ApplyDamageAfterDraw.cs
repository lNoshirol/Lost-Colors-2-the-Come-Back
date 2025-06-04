using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageAfterDraw : MonoBehaviour
{
    public static ApplyDamageAfterDraw Instance;
    
    private Dictionary<EnemyHealth, float> _ennemyAndDamage = new Dictionary<EnemyHealth, float>();

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

    public void AddEnnemyDamage(EnemyHealth enemy, float damage)
    {
        _ennemyAndDamage.Add(enemy, damage);
    }

    public void ApplyDamage()
    {
        Debug.Log("tabasse tout le monde");

        foreach (var var in _ennemyAndDamage)
        {
            var.Key.EnemyLoseHP(var.Value);
        }
    }
}
