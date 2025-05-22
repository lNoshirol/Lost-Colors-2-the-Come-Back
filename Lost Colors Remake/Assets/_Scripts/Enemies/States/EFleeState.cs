using UnityEngine;
using UnityEngine.AI;

public class EFleeState : EnemiesState
{
    public float fleeDistance;
    private Vector2 fleePoint;
    [SerializeField]
    private bool fleePointSet = false;

    public override void OnEnter()
    {
        FleeFromPlayer(fleeDistance);
    }
    public override void Do()
    {
        EnemiesMain.UpdateSpriteDirectionRB();
        if (!fleePointSet)
        {
            FleeFromPlayer(fleeDistance);
        }

        if (fleePointSet)
        {
            SetEnemyDestination(fleePoint);
        }

        if (FleeDestinationReach())
        {
            Debug.Log("Destination atteinte");
            FleeFromPlayer(fleeDistance);
        }
    }

    public override void FixedDo()
    {
    }
    public override void OnExit()
    {
        EnemiesMain.agent.SetDestination(transform.position);
    }

    public void FleeFromPlayer(float fleeDistance)
    {
        Vector3 directionAwayFromPlayer = (transform.position - EnemiesMain.player.position).normalized;
        Vector3 rawFleePoint = transform.position + directionAwayFromPlayer * fleeDistance;

        RaycastHit2D groundHit = Physics2D.Raycast(rawFleePoint, Vector2.down, 0.1f, EnemiesMain.whatIsGround);

        if (groundHit.collider != null)
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(rawFleePoint, out navHit, 2f, NavMesh.AllAreas))
            {
                fleePoint = navHit.position;
                fleePointSet = true;
            }
        }
    }

    private void SetEnemyDestination(Vector2 destination)
    {
        EnemiesMain.agent.SetDestination(destination);
    }

    private bool FleeDestinationReach()
    {
        return Vector2.Distance(transform.position, fleePoint) < 2f;
    }

}
