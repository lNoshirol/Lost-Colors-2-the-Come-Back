using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesMain : MonoBehaviour
{
    [Header("Enemy State")]
    public EIdle EIdleState;
    public EPatrolState EPatrolState;
    public EChaseState EChaseState;
    public EAttackState EAttackState;
    public EnemiesState EnemiesCurrentState;


    [Header("Enemy Components")]
    public NavMeshAgent agent;
    public GameObject enemyMesh;
    public EnemyHealth Health { get; private set; }
    public EnemyUI UI { get; private set; }

    public EnemyStats Stats { get; private set; }

    public Rigidbody rb { get; private set; }
    public Transform player { get; private set; }
    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }
    [SerializeField] SpriteRenderer spriteRenderer;

    //Range

    [Header("Enemy Brain Needs")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    [Header("Enemy Brain Needs")]
    public GameObject projectile;
    public Material mat;

    //Delay for updates
    private float nextSightCheckTime = 0f;
    private float nextAttackCheckTime = 0f;
    private float checkInterval = 0.2f;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        mat = enemyMesh.GetComponent <Renderer>().material;

        Health = GetComponent<EnemyHealth>();
        UI = GetComponent<EnemyUI>();
        Stats = GetComponent<EnemyStats>();
    }
    private void Start()
    {
        EPatrolState.Setup(this);
        EChaseState.Setup(this);
        EAttackState.Setup(this);
        EIdleState.Setup(this);
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
        FlipSprite();


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
            playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, whatIsPlayer);
        }
        return playerInSightRange;
    }

    public bool CheckPlayerInAttackRange()
    {
        if (Time.time >= nextAttackCheckTime)
        {
            nextAttackCheckTime = Time.time + checkInterval;
            playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
        }
        return playerInAttackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void FlipSprite()
    {
        if(agent.velocity.normalized.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if(agent.velocity.normalized.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
