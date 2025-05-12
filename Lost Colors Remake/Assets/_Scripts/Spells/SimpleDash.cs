using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        Dash(context.Rigidbody, context.Direction, context.Strength);
    }
}
