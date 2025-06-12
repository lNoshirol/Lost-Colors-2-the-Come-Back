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


    public override void OnEnter()
    {
    }

    public override void Do()
    {
        EnemiesMain.Animation.GetDirectionXAnimPlayer();
        if (!EnemiesMain.alreadyAttack)
        {
            EnemiesMain.agent.isStopped = true;
            EnemiesMain.canLookAt = false;
            int randomSpell = Random.Range(0, 2);
            if (randomSpell == 0)
            {
                CastCloseSkill();
            }
            else
            {
                CastRangeSkill();
            }
            EnemiesMain.alreadyAttack = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        else if (!EnemiesMain.CheckPlayerInAttackRange())
        {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }
    }

    public override void OnExit()
    {
        EnemiesMain.canLookAt = true;
        EnemiesMain.agent.isStopped = false;
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