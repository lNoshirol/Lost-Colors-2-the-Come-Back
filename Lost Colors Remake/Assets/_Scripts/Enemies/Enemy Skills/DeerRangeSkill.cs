using UnityEngine;

public class DeerRangeSkill : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        Debug.Log("Cerf fireball");
        GameObject fireBall = GetProjectile("EnemyFireBall");
        AlignToTransform(fireBall.transform, context.Caster.transform);
        Projectile proj = fireBall.TryGetComponent(out Projectile projectile) ? projectile : null;
        proj.Launch(context);
    }
}
