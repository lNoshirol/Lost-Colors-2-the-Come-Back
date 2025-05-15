using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesMain : MonoBehaviour
{
    [Header("ScriptableDATA")]
    public EnemyDATA enemyData;
    public bool isColorized;

    [Header("Enemy Brain Needs")]

    public bool playerInSightRange;
    public bool playerInAttackRange;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public GameObject projectile;

    [Header("Enemy State")]
    public EnemiesState EnemiesCurrentState;

    [Header("Enemy State Global")]
    public EIdle EIdleState;
    public EPatrolState EPatrolState;
    

    [Header("Enemy State BW")]
    public EChaseState EChaseState;
    public EAttackState EAttackState;

    [Header("Enemy State Colorized")]
    public EFleeState EFleeState;


    [Header("Enemy Components")]
    [SerializeField] SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    public GameObject enemyMesh;
    public EnemyHealth Health { get; private set; }
    public EnemyUI UI { get; private set; }

    public EnemyStats Stats { get; private set; }

    public Rigidbody rb { get; private set; }
    public Transform player { get; private set; }
    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }
    

    //Range





    //Delay for updates
    private float nextSightCheckTime = 0f;
    private float nextAttackCheckTime = 0f;
    private float checkInterval = 0.2f;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();

        Health = GetComponent<EnemyHealth>();
        UI = GetComponent<EnemyUI>();
    }
    private void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject);
        SetupStats();

        EPatrolState.Setup(this);
        EChaseState.Setup(this);
        EAttackState.Setup(this);
        EIdleState.Setup(this);
        EFleeState.Setup(this);
        EnemiesCurrentState = EIdleState;
        EnemiesCurrentState?.OnEnter();

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        EnemiesCurrentState?.Do();
    }

    private void FixedUpdate()
    {
        EnemiesCurrentState?.FixedDo();
    }

    public void SwitchState(EnemiesState newState)
    {
        EnemiesCurrentState?.OnExit();
        EnemiesCurrentState = newState;
        EnemiesCurrentState?.OnEnter();
    }

    public bool CheckPlayerInSightRange()
    {
        if (Time.time >= nextSightCheckTime)
        {
            nextSightCheckTime = Time.time + checkInterval;
            playerInSightRange = Physics2D.OverlapCircle(transform.position, Stats.sightRange, whatIsPlayer);
        }
        return playerInSightRange;
    }

    public bool CheckPlayerInAttackRange()
    {
        if (Time.time >= nextAttackCheckTime)
        {
            nextAttackCheckTime = Time.time + checkInterval;
            playerInAttackRange = Physics2D.OverlapCircle(transform.position, Stats.attackRange, whatIsPlayer);
        }
        return playerInAttackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Stats.attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Stats.sightRange);
    }

    [System.Serializable]
    public class EnemyStats
    {
        //All the stat from the scriptable 
        [Header("Globale Stats")]
        public bool isColorized;
        public int powerLevel;
        public float maxHp;
        public List<string> armorList;
        public int maxArmor;
        public float sightRange;
        public float attackRange;
        public float idleWaitTime;
        public float patrolSpeedMultiplier;
        public float chaseSpeedMultiplier;
        public float attackAmount;
        public float attackCooldown;
        public float speed;
        public float maxSpeed;
        public List<string> skillNameList;
    }

    void SetupStats()
    {
        Stats = new EnemyStats
        {
            powerLevel = enemyData.enemyPowerLevel,
            maxHp = enemyData.enemyMaxHP,
            armorList = new List<string>(enemyData.enemyArmorList),
            maxArmor = enemyData.enemyMaxArmor,
            sightRange = enemyData.enemySightRange,
            attackRange = enemyData.enemyAttackRange,
            idleWaitTime = enemyData.enemyIdleWaitTime,
            patrolSpeedMultiplier = enemyData.patrolSpeedMultiplier,
            chaseSpeedMultiplier = enemyData.chaseSpeedMultiplier,
            attackAmount = enemyData.enemyAttackAmount,
            attackCooldown = enemyData.enemyAttackCooldown,
            speed = enemyData.enemySpeed,
            maxSpeed = enemyData.enemyMaxSpeed,
            skillNameList = new List<string>(enemyData.skillNameList)
        };

    }

    public EnemiesState GetChaseOrFleeState()
    {
        return isColorized ? EFleeState : EChaseState;

    }
}
