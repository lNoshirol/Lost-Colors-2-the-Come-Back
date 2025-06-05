using System.Collections;
using UnityEngine;

public class EIdle : EnemiesState
{
    public float idleWaitTime;
    public override void OnEnter()
    {
        EnemiesMain.agent.velocity = Vector3.zero;
        EnemiesMain.Animation.enemyAnimator.SetBool("IsMoving", false);
        EnemiesMain.StartCoroutine(IdleWait());
    }

    public override void Do()
    {

    }

    public override void FixedDo()
    {
    }

    public override void OnExit()
    {
        
    }

    private IEnumerator IdleWait()
    {
        yield return new WaitForSeconds(idleWaitTime);
        EnemiesMain.SwitchState(EnemiesMain.EPatrolState);
    }


}
