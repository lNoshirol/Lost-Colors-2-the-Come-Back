using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DeerRangeSkill : SkillParentClass
{
    List<Vector2> VFXListPos = new();
    private float timeToWaitBeforeAttack = 0.6f;
    public override async void Activate(SkillContext context)
    {
        await Task.Delay((int)(timeToWaitBeforeAttack * 1000));
        VFXListPos.Add(context.Direction);
        foreach (Vector2 pos in Vector2AroundPlayer(context.Direction, context.MaxDistance, context.Strength))
        {
            VFXListPos.Add(pos);
        };
        WaitForEachFX(0.1f);
    }

    async void WaitForEachFX(float WaitTimeBetween)
    {
        foreach(Vector2 pos in VFXListPos)
        {
            await Task.Delay((int)(WaitTimeBetween * 1000));
            if (EnemyManager.Instance == null) return;
            SearchVFX("Thunder").transform.position = pos;
        }
    }

    //Debug.Log("Cerf fireball");
    //GameObject fireBall = GetProjectile("EnemyFireBall");
    //AlignToTransform(fireBall.transform, context.Caster.transform);
    //Projectile proj = fireBall.TryGetComponent(out Projectile projectile) ? projectile : null;
    //proj.Launch(context);
}
