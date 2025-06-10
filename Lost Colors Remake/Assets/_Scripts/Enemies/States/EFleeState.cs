using System.Collections;
using System.Collections.Generic;
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
        EnemiesMain.Animation.GetDirectionXAnimAgent();
        if (!fleePointSet)
        {
            FleeFromPlayer(fleeDistance);
        }

        else if (fleePointSet)
        {
            SetEnemyDestination(fleePoint);
            WaitAndFleeAgain(2f);
        }

        //if (FleeDestinationReach())
        //{
        //    Debug.Log("Destination atteinte");
        //    fleePointSet = false;
        //}
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
        return Vector3.Distance(transform.position, fleePoint) < 0.5f;
    }

    IEnumerator WaitAndFleeAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fleePointSet = false;
    }
}
