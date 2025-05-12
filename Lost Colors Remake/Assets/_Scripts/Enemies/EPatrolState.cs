using UnityEngine;
using UnityEngine.AI;

public class EPatrolState : EnemiesState
{
    [SerializeField] 
    float patrolSpeedMultiplier;
    [SerializeField]
    float walkPointRange;
    [SerializeField]
    Vector3 walkPoint;
    [SerializeField]
    bool walkPointSet = false;

    public override void OnEnter()
    {
        //EnemiesMain.mat.color = Color.green;
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
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, EnemiesMain.whatIsGround))
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(walkPoint, out hit, 1f, NavMesh.AllAreas))
            {
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
        bool destinationReach = false;
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            return destinationReach = true;
        }
        return destinationReach;
    }

}
