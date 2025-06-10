using UnityEngine;

public class DeerCloseSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 0.7f;
    public override void Activate(SkillContext context)
    {
        DelayedFunction(() => EnemyDash(context.Agent, context.Strength, context.MaxDistance), timeToWaitBeforeAttack);
        //Debug.Log("Cerf dash attack");
        //Dash(context.Rigidbody2D, context.Direction, context.Strength);
        //DelayedFunction(() => StopRigidBody(context.Rigidbody2D), 1.5f);
    }
}

