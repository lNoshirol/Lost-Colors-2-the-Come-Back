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
        WaitForEachFX(1);
    }

    async void WaitForEachFX(int WaitTimeBetween)
    {
        foreach(Vector2 pos in VFXListPos)
        {
            await Task.Delay(WaitTimeBetween * 1000);
            Debug.Log("VFX at " + pos);
            //Gizmos.DrawCube(new Vector3(pos.x, pos.y, 0), new Vector3(1, 1, 1));
        }
    }

    //Debug.Log("Cerf fireball");
    //GameObject fireBall = GetProjectile("EnemyFireBall");
    //AlignToTransform(fireBall.transform, context.Caster.transform);
    //Projectile proj = fireBall.TryGetComponent(out Projectile projectile) ? projectile : null;
    //proj.Launch(context);
}
