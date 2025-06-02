using System.Collections;
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
        EnemiesMain.agent.isStopped = true;
    }

    public override void Do()
    {
        EnemiesMain.Animation.GetDirectionXAnimPlayer();
        if (!EnemiesMain.alreadyAttack) {

            int randomSpell = Random.Range(0, 1);
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
        ResetAttack();
        EnemiesMain.agent.isStopped = false;
        EnemiesMain.isAttacking = false;
        EnemiesMain.Animation.AnimParameters("Attack", false);
        EnemiesMain.Animation.AnimParameters("isCloseAttacking", false);
        EnemiesMain.Animation.AnimParameters("isRangeAttacking", false);
    }

    void SkillSetup()
    {
        context = new(EnemiesMain.rb, this.gameObject, (EnemiesMain.player.position - transform.position).normalized, 10);
        rangeSkill = new DeerRangeSkill();
        closeSkill = new DeerCloseSkill();
    }

    public override void FixedDo()
    {
    }

    private void ResetAttack()
    {
        EnemiesMain.alreadyAttack = false;
    }

    public void CastCloseSkill()
    {
        EnemiesMain.Animation.AnimParameters("isCloseAttacking", true);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length, true));
    }
    public void CastRangeSkill()
    {
        EnemiesMain.Animation.AnimParameters("isRangeAttacking", true);
        StartCoroutine(WaitForEndAnime(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length, false));
    }


    IEnumerator WaitForEndAnime(float _animTime, bool isCloseSkill)
    {
        yield return new WaitForSeconds(_animTime);
        SkillSetup();
        EnemiesMain.Animation.AnimParameters("Attack", true);
        EnemiesMain.isAttacking = true;
        if (isCloseSkill)
        {
            closeSkill.Activate(context);
        }
        else
        {
            rangeSkill.Activate(context);
        }
        yield return new WaitForSeconds(EnemiesMain.Animation.enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        EnemiesMain.Animation.AnimParameters("Attack", false);
        EnemiesMain.Animation.AnimParameters("isCloseAttacking", false);
        EnemiesMain.Animation.AnimParameters("isRangeAttacking", false);
    }
    //Vector2 direction = (EnemiesMain.player.position - transform.position).normalized;
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //Quaternion rotation = Quaternion.Euler(0, 0, angle);
    //Rigidbody2D rb = Instantiate(EnemiesMain.projectile, transform.position, rotation).GetComponent<Rigidbody2D>();
    //rb.AddForce(direction * 10f, ForceMode2D.Impulse);
}
