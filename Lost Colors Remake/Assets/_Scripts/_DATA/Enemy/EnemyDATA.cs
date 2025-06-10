using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy", order = 1)]
public class EnemyDATA : ScriptableObject
{
    [Header("Enemy Base")]
    public string enemyID;	         //    
    public float enemyMaxHP;

    [Header("Enemy AgentBaseStats")]
    public float enemySpeed;
    public float enemyAngularSpeed;
    public float enemyAcceleration;

    [Header("Enemy Armor")]
    public List<GameObject> armorSpriteListPrefab;
    public int enemyMaxArmor;

    [Header("Enemy Range")]
    public float enemySightRange;
    public float enemyAttackRange;

    [Header("Enemy WaitTime")]
    public float enemyIdleWaitTime;

    [Header("Enemy SpeedMultiplier")]
    public float patrolSpeedMultiplier;
    public float chaseSpeedMultiplier;

    [Header("Enemy Attack")]

    public float enemyAttackAmount;
    public float enemyAttackCooldown;
    public AnimalType animalType;


    public enum AnimalType
    {
        Deer,
        Wolf
    }
}
