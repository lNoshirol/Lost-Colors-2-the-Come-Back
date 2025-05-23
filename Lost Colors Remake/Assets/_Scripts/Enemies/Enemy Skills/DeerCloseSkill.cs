using UnityEngine;

public class DeerCloseSkill : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        Debug.Log("Cerf dash attack");
        Dash(context.Rigidbody2D, context.Direction, context.Strength);
    }
}

