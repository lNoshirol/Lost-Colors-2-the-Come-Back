using UnityEngine;

public class PatrolState : IAState
{
    private EnemyIA enemy;

    public PatrolState(EnemyIA enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("PatrolState.cs : Enter");
        enemy.Patrol();
    }

    public void Execute()
    {
        enemy.Patrol();

        if (enemy.NearPlayer())
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public void Exit()
    {
        Debug.Log("PatrolState.cs : Exite");
    }
}

