using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy", order = 1)]
public class EnemyDATA : ScriptableObject
{
    public string enemyID;	         // 
    public string enemyName;         // 

    public float enemyAttack;
    public float enemyAttackCooldown;

    public float enemyMaxHP;
    public float enemyHP;

    public string enemyArmorId;
    public float enemyArmor;

    public float enemySpeed;
    public float enemyMaxSpeed;

    public float enemySightRange;
    public float enemyAttackRange;

    public List<string> skillName;
}
