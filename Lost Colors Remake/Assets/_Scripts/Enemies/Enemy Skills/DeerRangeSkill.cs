using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DeerRangeSkill : SkillParentClass
{
    List<Vector2> VFXListPos = new();
    public override void Activate(SkillContext context)
    {
        VFXListPos.Add(context.Direction);
        foreach (Vector2 pos in Vector2AroundPlayer(context.Direction, context.MaxDistance, context.Strength))
        {
            VFXListPos.Add(pos);
        };
        WaitForEachFX(0.5f);
    }

    async void WaitForEachFX(float WaitTimeBetween)
    {
        foreach(Vector2 pos in VFXListPos)
        {
            await Task.Delay((int)(WaitTimeBetween * 1000));
            GameObject newVFX = EnemyManager.Instance.SearchInPool("Thunder", EnemyManager.Instance.vfxPool);
            newVFX.transform.position = pos;
        }
    }

    //Debug.Log("Cerf fireball");
    //GameObject fireBall = GetProjectile("EnemyFireBall");
    //AlignToTransform(fireBall.transform, context.Caster.transform);
    //Projectile proj = fireBall.TryGetComponent(out Projectile projectile) ? projectile : null;
    //proj.Launch(context);
}
