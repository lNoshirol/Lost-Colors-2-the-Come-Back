using UnityEngine;

public class WolfCloseSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 0.7f;

    public override void Activate(SkillContext context)
    {
        DelayedFunction(() => EnemyDash(context.Agent, context.Strength, context.MaxDistance), timeToWaitBeforeAttack);
    }
}
