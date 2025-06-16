using System.Collections;
using UnityEngine;

public class EIdle : EnemiesState
{
    public float idleWaitTime;
    public override void OnEnter()
    {
        if (EnemiesMain.doingColorization) return;
        else
        {
            EnemiesMain.agent.isStopped = true;
            EnemiesMain.agent.velocity = Vector3.zero;
            EnemiesMain.Animation.enemyAnimator.SetBool("IsMoving", false);
            EnemiesMain.Animation.SetAnimTransitionParameter("isCloseAttacking", false);
            EnemiesMain.Animation.SetAnimTransitionParameter("isRangeAttacking", false);
        
        EnemiesMain.StartCoroutine(IdleWait());
        }

    }

    public override void Do()
    {

    }

    public override void FixedDo()
    {
    }

    public override void OnExit()
    {
        if (EnemiesMain.doingColorization) return;
        else EnemiesMain.agent.isStopped = false;
    }

    private IEnumerator IdleWait()
    {
        yield return new WaitForSeconds(idleWaitTime);
        EnemiesMain.SwitchState(EnemiesMain.EPatrolState);
    }


}
