using UnityEngine;

public class EnemyFireBall : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        // PROTO
        GameObject fireBall = GetProjectile("FireBall");
        AlignToTransform(fireBall.transform, context.Caster.transform);
        Projectile proj = fireBall.TryGetComponent(out Projectile projectile) ? projectile : null;
        proj.Launch();
    }
}
