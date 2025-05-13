using UnityEngine;
using UnityEngine.AI;

public class EPatrolState : EnemiesState
{
    [SerializeField]
    private float patrolSpeedMultiplier;

    [SerializeField]
    private float walkPointRange;

    [SerializeField]
    private Vector3 walkPoint;

    [SerializeField]
    private bool walkPointSet = false;

    public override void OnEnter()
    {
        
        SearchWalkPoint();
    }

    public override void Do()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            SetEnemyDestination(walkPoint);
        }

        if (EnemiesMain.CheckPlayerInSightRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
            return;
        }

        if (DestinationReach() && !EnemiesMain.CheckPlayerInSightRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EIdleState);
        }
    }

    public override void FixedDo()
    {
        
    }

    public override void OnExit()
    {
        walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, 0f);

        
        RaycastHit2D groundHit = Physics2D.Raycast(walkPoint, Vector2.down, 0.1f, EnemiesMain.whatIsGround);

        if (groundHit.collider != null)
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(walkPoint, out navHit, 0.5f, NavMesh.AllAreas))
            {
                walkPoint = navHit.position;
                walkPointSet = true;
            }
        }
    }

    private void SetEnemyDestination(Vector3 destination)
    {
        EnemiesMain.agent.SetDestination(destination);
    }

    private bool DestinationReach()
    {
        return Vector3.Distance(transform.position, walkPoint) < 0.5f;
    }

}
