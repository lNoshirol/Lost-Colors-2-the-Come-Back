using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float detectionRange = 8f;

    public NavMeshAgent agent;
    private IAState currentState;
    private int currentPatrolIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState(this));
    }

    void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(IAState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool NearPlayer()
    {
        return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public bool FarFromPlayer()
    {
        return Vector3.Distance(transform.position, player.position) >= detectionRange;
    }

    public void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        agent.SetDestination(targetPoint.position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}
