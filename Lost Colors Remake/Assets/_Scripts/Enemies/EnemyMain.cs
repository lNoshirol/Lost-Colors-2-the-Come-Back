using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D;
using static EnemyDATA;

public class EnemyMain : MonoBehaviour
{
    [Header("ScriptableDATA")]
    public EnemyDATA enemyData;
    public bool isColorized;
    public bool doingColorization;

    [Header("Enemy State Needs")]
    public bool playerInSightRange;
    public bool playerInAttackRange; 
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public bool isAttacking = false;
    public bool alreadyAttack;
    public bool canLookAt = true;

    [Header("ShaderNeeds")]
    public SpriteRenderer spriteRenderer;
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
    public NavMeshAgent agent;
    public GameObject enemyMesh;
    public EnemyHealth Health { get; protected set; }
    public EnemyUI UI { get; protected set; }

    public EnemyStats Stats { get; protected set; }

    public EnemyArmor Armor;

    public EnemyAnimation Animation;

    public Rigidbody2D rb { get; protected set; }
    public Transform player { get; private set; }
    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }





    //[Header("Enemy Sprite BW")]
    //public Sprite spriteRightBW;
    //public Sprite spriteLeftBW;

    //[Header("Enemy Sprite Color")]
    //public Sprite spriteRightColor;
    //public Sprite spriteLeftColor;

    //Delay for updates
    private float nextSightCheckTime = 0f;
    private float nextAttackCheckTime = 0f;
    private float checkInterval = 0.2f;

    //ATTACK STATE
    public SkillParentClass closeSkill;
    public SkillParentClass rangeSkill;

    public SkillContext contextRange;
    public SkillContext contextClose;

    public virtual void Start()
    {

        player = PlayerMain.Instance.PlayerGameObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        Health = GetComponent<EnemyHealth>();
        UI = GetComponent<EnemyUI>();

        Animation.GetGoodSocketVFXOrientation();

        SetupStats();

        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject, isColorized);

        SetupAndEnterState();

        SnapToNavMesh();

        DisplayGoodUI();
    }

    public virtual void Update()
    {
        EnemiesCurrentState?.Do();
    }

    private void FixedUpdate()
    {
        EnemiesCurrentState?.FixedDo();
    }

    private void SnapToNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public virtual void SetupAndEnterState()
    {
        EPatrolState.Setup(this);
        EChaseState.Setup(this);
        EAttackState.Setup(this);
        EIdleState.Setup(this);
        EFleeState.Setup(this);
        EnemiesCurrentState = EIdleState;
        EnemiesCurrentState?.OnEnter();
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

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    if (transform.position == null && Stats.attackRange == null) return;
    //    Gizmos.DrawWireSphere(transform.position, Stats.attackRange);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, Stats.sightRange);
    //}

    [System.Serializable]
    public class EnemyStats
    {
        //All the stat from the scriptable 
        [Header("Globale Stats")]
        public bool isColorized;
        public float maxHp;
        public List<GameObject> armorList;
        public int maxArmor;
        public float sightRange;
        public float attackRange;
        public float idleWaitTime;
        public float patrolSpeedMultiplier;
        public float chaseSpeedMultiplier;
        public float attackAmount;
        public float attackCooldown;
        public string animalType;
        public float speed;
        public float angularSpeed;
        public float acceleration;
    }

    protected void SetupStats()
    {
        Stats = new EnemyStats
        {
            maxHp = enemyData.enemyMaxHP,

            armorList = new List<GameObject>(enemyData.armorSpriteListPrefab),
            maxArmor = enemyData.enemyMaxArmor,

            sightRange = enemyData.enemySightRange,
            attackRange = enemyData.enemyAttackRange,

            idleWaitTime = enemyData.enemyIdleWaitTime,

            patrolSpeedMultiplier = enemyData.patrolSpeedMultiplier,
            chaseSpeedMultiplier = enemyData.chaseSpeedMultiplier,

            attackAmount = enemyData.enemyAttackAmount,
            attackCooldown = enemyData.enemyAttackCooldown,
            animalType = enemyData.animalType.ToString(),


            speed = enemyData.enemySpeed,
            angularSpeed = enemyData.enemyAngularSpeed,
            acceleration = enemyData.enemyAcceleration,

        };

        agent.speed = Stats.speed;
        agent.acceleration = Stats.acceleration;
        agent.angularSpeed = Stats.angularSpeed;
        AnimalContextCheck();
    }

    public EnemiesState GetChaseOrFleeState()
    {
        return isColorized ? EFleeState : EChaseState;
    }

    public virtual void ColorSwitch()
    {
        isColorized = true;
        gameObject.layer = 0;
        Animation.SwitchAnimatorToColor();
        EnemiesCurrentState = EIdleState;
        EnemiesCurrentState?.OnEnter();
    }

    //public void UpdateSpriteDirectionRB()
    //{
    //    Vector2 direction = agent.velocity;

    //    if (direction.x >= 0)
    //    {
    //        spriteRenderer.sprite = spriteRightBW;
    //        spriteRenderer.material.SetTexture("_ColoredTex", spriteRightColor.texture);
    //    }
    //    else
    //    {
    //        spriteRenderer.sprite = spriteLeftBW;
    //        spriteRenderer.material.SetTexture("_ColoredTex", spriteLeftColor.texture);
    //    }
    //}

    //public void UpdateSpriteDirectionPlayer()
    //{
    //    Vector2 toPlayer = player.position - transform.position;
    //    if (toPlayer.x >= 0)
    //    {
    //        spriteRenderer.sprite = spriteRightBW;
    //        spriteRenderer.material.SetTexture("_ColoredTex", spriteRightColor.texture);
    //    }
    //    else
    //    {
    //        spriteRenderer.sprite = spriteLeftBW;
    //        spriteRenderer.material.SetTexture("_ColoredTex", spriteLeftColor.texture);
    //    }
    //}


    public virtual void DisplayGoodUI()
    {
        if (isColorized)
        {
            UI.SwitchHealtBar(false);
        }
        else
        {
            if (Stats.maxArmor == 0)
            {
                return;
            }
            else
            {
                UI.SwitchHealtBar(false);
                Armor.AddGlyph();
            }
        }
    }

    public void AnimalContextCheck()
    {
        switch (Stats.animalType)
        {
            case "Deer":
                closeSkill = new DeerCloseSkill();
                rangeSkill = new DeerRangeSkill();
                contextClose = new(null, player, new Vector2(0,0), 10, 2, agent);
                contextRange = new(null, player, new Vector2(0, 0), 5, 3);
                break;
            case "Wolf":
                closeSkill = new WolfCloseSkill();
                rangeSkill = new WolfRangeSkill();
                contextClose = new(null, player, new Vector2(0,0), 10, 2, agent);
                contextRange = new(null, Animation.currentVfxSocket.transform, new Vector2(0,0), 1);
                break;
            default:
                Debug.LogError("Unknow animal type, please select one");
                break;
        }
    }
}