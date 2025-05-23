using UnityEngine;

public class DeerRangeSkill : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        Dash(context.Rigidbody2D, context.Direction, context.Strength);
    }
}
