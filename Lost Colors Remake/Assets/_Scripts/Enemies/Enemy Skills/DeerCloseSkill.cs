using UnityEngine;

public class DeerCloseSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 1f;
    public override void Activate(SkillContext context)
    {
        DelayedFunction(() => Dash(context.Rigidbody2D, context.Direction, context.Strength), timeToWaitBeforeAttack);
        Debug.Log("Cerf dash attack");
        Dash(context.Rigidbody2D, context.Direction, context.Strength);
        DelayedFunction(() => StopRigidBody(context.Rigidbody2D), 1.5f);
    }
}

