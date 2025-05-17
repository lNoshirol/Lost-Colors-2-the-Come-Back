using UnityEngine;

public class EAttackState : EnemiesState
{
    [SerializeField]
    float attackCooldown;
    [SerializeField]
    float attackAmount;
    [SerializeField]
    bool alreadyAttack;


    public override void OnEnter()
    {
        //EnemiesMain.mat.color = Color.red;
    }

    public override void Do()
    {
        EnemiesMain.UpdateSpriteDirectionPlayer();
        if (!alreadyAttack) {
            Vector2 direction = (EnemiesMain.player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Rigidbody2D rb = Instantiate(EnemiesMain.projectile, transform.position, rotation).GetComponent<Rigidbody2D>();
            rb.AddForce(direction * 10f, ForceMode2D.Impulse);
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        else if (!EnemiesMain.CheckPlayerInAttackRange()) {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }
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
}
