using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy", order = 1)]
public class EnemyDATA : ScriptableObject
{
    public string enemyID;	         //    
    public int enemyPowerLevel;
    public float enemyMaxHP;

    public List<GameObject> armorSpriteListPrefab;
    public int enemyMaxArmor;

    public float enemySightRange;
    public float enemyAttackRange;

    public float enemyIdleWaitTime;

    public float patrolSpeedMultiplier;
    public float chaseSpeedMultiplier;

    public float enemyAttackAmount;
    public float enemyAttackCooldown;

    public float enemySpeed;
    public float enemyMaxSpeed;


    public List<string> skillNameList;
}
