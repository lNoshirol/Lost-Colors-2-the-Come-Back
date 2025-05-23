using UnityEngine;

public class DeerRecover : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        StopRigidBody(context.Rigidbody2D);
    }
}