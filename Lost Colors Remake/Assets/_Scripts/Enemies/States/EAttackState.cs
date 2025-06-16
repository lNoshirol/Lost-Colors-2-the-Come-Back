using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;
using static EnemyDATA;
using static EnemyMain;

public class EAttackState : EnemiesState
{
    [SerializeField]
    float attackCooldown;
    [SerializeField]
    float attackAmount;
    [SerializeField]
    string attackName;

    [SerializeField] int numberOfAttack;


    public override void OnEnter()
    {
        if (EnemiesMain.isColorized)
        {
            EnemiesMain.SwitchState(EnemiesMain.EFleeState);
        }
    }

    public override void Do()
    {
        if (EnemiesMain.isColorized || EnemiesMain.doingColorization) return;
        EnemiesMain.Animation.GetDirectionXAnimPlayer();
        if (!EnemiesMain.alreadyAttack)
        {
            EnemiesMain.agent.isStopped = true;
            //EnemiesMain.canLookAt = false;
            int randomSpell = Random.Range(0, numberOfAttack);
            EnemiesMain.AnimalContextCheck();
            if (randomSpell == 1)
            {
                CastCloseSkill();
            }
            else
            {
                CastRangeSkill();
            }
            EnemiesMain.alreadyAttack = true;
            Invoke(nameof(ResetAttack), attackCooldown);
            //EnemiesMain.canLookAt = false;
        }
        else if (!EnemiesMain.CheckPlayerInAttackRange() && !EnemiesMain.alreadyAttack)
        {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }

    }

    public override void OnExit()
    {
        EnemiesMain.canLookAt = true;
        if(!EnemiesMain.isColorized)EnemiesMain.agent.isStopped = false;
        EnemiesMain.Animation.SetAnimTransitionParameter("isCloseAttacking", false);
        EnemiesMain.Animation.SetAnimTransitionParameter("isRangeAttacking", false);
    }

    public override void FixedDo()
    {
    }

    private void ResetAttack()
    {
        EnemiesMain.isAttacking = false;
        EnemiesMain.alreadyAttack = false;
    }

    public void CastCloseSkill()
    {
        EnemiesMain.Animation.SetAnimTransitionParameter("isCloseAttacking", true);
        EnemiesMain.closeSkill.Activate(EnemiesMain.contextClose);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length));
    }
    public void CastRangeSkill()
    {
        EnemiesMain.canLookAt = false;
        EnemiesMain.Animation.SetAnimTransitionParameter("isRangeAttacking", true);
        EnemiesMain.rangeSkill.Activate(EnemiesMain.contextRange);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length));
    }
    IEnumerator WaitForEndAnime(float _animTime)
    {
        EnemiesMain.isAttacking = true;
        yield return new WaitForSeconds(_animTime);
        EnemiesMain.SwitchState(EnemiesMain.EIdleState);
    }
}