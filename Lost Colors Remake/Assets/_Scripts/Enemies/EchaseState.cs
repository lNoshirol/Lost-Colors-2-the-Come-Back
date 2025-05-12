using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EChaseState : EnemiesState
{

    [SerializeField]
    private float chaseSpeedMultiplier;


    public override void OnEnter()
    {
        //EnemiesMain.mat.color = Color.magenta;
    }
    public override void Do()
    {
        EnemiesMain.agent.SetDestination(EnemiesMain.player.position);

        if (EnemiesMain.CheckPlayerInAttackRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EAttackState);
        }
        else if (!EnemiesMain.CheckPlayerInSightRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EPatrolState);
        }
    }

    public override void FixedDo()
    {
    }
    public override void OnExit()
    {
        EnemiesMain.agent.SetDestination(transform.position);
    }
}
