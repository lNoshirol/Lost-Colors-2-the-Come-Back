using PDollarGestureRecognizer;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct DamageStructModel
{
    public EnemyHealth targetedEnemy;
    public float damage;
    public DrawData drawData;

    public DamageStructModel(EnemyHealth _targetedEnemy, float _damage, DrawData _drawData)
    {
        targetedEnemy = _targetedEnemy;
        damage = _damage;
        drawData = _drawData;
    }
}
