using UnityEngine;

public class EAttackState : EnemiesState
{
    [SerializeField]
    float attackCooldown;
    [SerializeField]
    float attackAmount;
    [SerializeField]
    bool alreadyAttack;

    private SkillParentClass closeSkill;
    private SkillParentClass rangeSkill;
    SkillContext context;


    private void Start()
    {
        
    }

    public override void OnEnter()
    {
        

    }

    public override void Do()
    {
        EnemiesMain.UpdateSpriteDirectionPlayer();
        if (!alreadyAttack) {
            CastCloseSpell();
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        else if (!EnemiesMain.CheckPlayerInAttackRange()) {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }
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
    public override void OnExit()
    {
        ResetAttack();
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    public void CastCloseSpell()
    {
        SkillSetup();
        closeSkill.Activate(context);
    }
    public void CastRangeSpell()
    {
        SkillSetup();
        rangeSkill.Activate(context);
    }


    //Vector2 direction = (EnemiesMain.player.position - transform.position).normalized;
    //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //Quaternion rotation = Quaternion.Euler(0, 0, angle);
    //Rigidbody2D rb = Instantiate(EnemiesMain.projectile, transform.position, rotation).GetComponent<Rigidbody2D>();
    //rb.AddForce(direction * 10f, ForceMode2D.Impulse);
}
