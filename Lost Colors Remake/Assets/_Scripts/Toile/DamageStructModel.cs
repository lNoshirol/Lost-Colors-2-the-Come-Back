using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct DamageStructModel
{
    public EnemyHealth targetedEnemy;
    public float damage;

    public DamageStructModel(EnemyHealth _targetedEnemy, float _damage)
    {
        targetedEnemy = _targetedEnemy;
        damage = _damage;
    }
}
