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
            int randomSpell = Random.Range(1, 2);
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
        EnemiesMain.agent.enabled = true;
        EnemiesMain.Animation.SetAnimTransitionParameter("Attack", false);
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
        context = new(null, null, EnemiesMain.player.position, 4, 10);
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
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length, true));
    }
    public void CastRangeSkill()
    {
        EnemiesMain.Animation.SetAnimTransitionParameter("isRangeAttacking", true);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length, false));
    }


    IEnumerator WaitForEndAnime(float _animTime, bool isCloseSkill)
    {
        yield return new WaitForSeconds(_animTime);

        EnemiesMain.isAttacking = true;
        EnemiesMain.Animation.SetAnimTransitionParameter("Attack", true);
        if (isCloseSkill)
        {
            SkillSetupClose();
            closeSkill.Activate(context);
        }
        else
        {
            SkillSetupRange();
            rangeSkill.Activate(context);
        }
        yield return new WaitForSeconds(1.5f);
        EnemiesMain.canLookAt = true;
        OnExit();
    }
    //Vector2 direction = (EnemiesMain.player.position - transform.position).normalized;
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //Quaternion rotation = Quaternion.Euler(0, 0, angle);
    //Rigidbody2D rb = Instantiate(EnemiesMain.projectile, transform.position, rotation).GetComponent<Rigidbody2D>();
    //rb.AddForce(direction * 10f, ForceMode2D.Impulse);
}
 