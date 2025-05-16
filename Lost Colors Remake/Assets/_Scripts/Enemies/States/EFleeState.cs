using UnityEngine;
using UnityEngine.AI;

public class EFleeState : EnemiesState
{
    public float fleeDistance;
    private Vector2 fleePoint;
    public override void OnEnter()
    {
        FleeFromPlayer(fleeDistance);
    }
    public override void Do()
    {
        if (!EnemiesMain.CheckPlayerInSightRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EPatrolState);
        }
        else if (FleeDestinationReach() && EnemiesMain.CheckPlayerInSightRange())
        {
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
                EnemiesMain.agent.SetDestination(fleePoint);
                Debug.DrawLine(transform.position, fleePoint, Color.blue, 1f);
            }
        }
    }

    private bool FleeDestinationReach()
    {
        return Vector2.Distance(transform.position, fleePoint) < 0.5f;
    }

}
