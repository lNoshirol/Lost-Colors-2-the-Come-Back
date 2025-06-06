using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class EAttackState : EnemiesState
{
    [SerializeField]
    float attackCooldown;
    [SerializeField]
    float attackAmount;

    private SkillParentClass closeSkill;
    private SkillParentClass rangeSkill;

    SkillContext context;

    public override void OnEnter()
    {
    }

    public override void Do()
    {
        EnemiesMain.Animation.GetDirectionXAnimPlayer();
        if (!EnemiesMain.alreadyAttack) {
            EnemiesMain.agent.enabled = false;
            EnemiesMain.canLookAt = false;
            int randomSpell = Random.Range(0, 2);
            Debug.Log("J'attaque");
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
        else if (!EnemiesMain.CheckPlayerInAttackRange()) {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }
    }

    public override void OnExit()
    {
        EnemiesMain.canLookAt = true;
        EnemiesMain.agent.enabled = true;
        EnemiesMain.Animation.SetAnimTransitionParameter("isCloseAttacking", false);
        EnemiesMain.Animation.SetAnimTransitionParameter("isRangeAttacking", false);
    }

    void SkillSetupClose()
    {
        context = new(EnemiesMain.rb, this.gameObject, (EnemiesMain.player.position - transform.position).normalized, 10);
        closeSkill = new DeerCloseSkill();
    }

    void SkillSetupRange()
    {
        context = new(null, null, EnemiesMain.player.position, 5, 3);
        rangeSkill = new DeerRangeSkill();
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
        SkillSetupClose();
        closeSkill.Activate(context);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length));
    }
    public void CastRangeSkill()
    {
        EnemiesMain.Animation.SetAnimTransitionParameter("isRangeAttacking", true);
        SkillSetupRange();
        rangeSkill.Activate(context);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator WaitForEndAnime(float _animTime)
    {
        EnemiesMain.isAttacking = true;
        yield return new WaitForSeconds(_animTime);
        EnemiesMain.SwitchState(EnemiesMain.EIdleState);
    }
}