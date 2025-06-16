using UnityEngine;
using System.Threading.Tasks;

public class WolfRangeSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 0.6f;
    public override async void Activate(SkillContext context)
    {
        await Task.Delay((int)(timeToWaitBeforeAttack * 1000));
        GameObject VFX = SearchVFX("FlameThrower");
        VFX.transform.position = context.Caster.position;
        VFX.transform.rotation = context.Caster.rotation;
    }
}