using UnityEngine;

public class ChaseState : IAState
{
    private EnemyIA enemy;

    public ChaseState(EnemyIA enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Entering Chase State");
    }

    public void Execute()
    {
        enemy.agent.SetDestination(enemy.player.position);

        if (enemy.FarFromPlayer())
        {
            enemy.ChangeState(new PatrolState(enemy));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Chase State");
    }
}

