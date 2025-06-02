using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EChaseState : EnemiesState
{

    [SerializeField]
    private float chaseSpeedMultiplier;


    public override void OnEnter()
    {
        EnemiesMain.Animation.SetAnimTransitionParameter("IsMoving", true);
    }
    public override void Do()
    {
        EnemiesMain.agent.SetDestination(EnemiesMain.player.position);

        EnemiesMain.Animation.GetDirectionXAnimPlayer();

        if (EnemiesMain.CheckPlayerInAttackRange() && !EnemiesMain.alreadyAttack)
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
