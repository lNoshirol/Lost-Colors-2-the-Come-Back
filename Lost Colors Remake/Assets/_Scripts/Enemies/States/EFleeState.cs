using UnityEngine;
using UnityEngine.AI;

public class EFleeState : EnemiesState
{
    public float fleeDistance;
    public override void OnEnter()
    {
        FleeFromPlayer(fleeDistance);
    }
    public override void Do()
    {
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
                EnemiesMain.agent.SetDestination(navHit.position);
                Debug.DrawLine(transform.position, navHit.position, Color.blue, 1f);
            }
        }
    }

}
