using UnityEngine;
using System.Threading.Tasks;

public class WolfRangeSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 0.6f;
    public override async void Activate(SkillContext context)
    {
        await Task.Delay((int)(timeToWaitBeforeAttack * 1000));
        SearchVFX("FlameThrower").transform.position = context.Direction;
        var sound = SoundsManager.Instance;
        int RandowWolf = Random.Range(0, sound.SoundEffectWolfAttackRanged.Length);
        sound.PlaySound(sound.SoundEffectWolfAttackRanged[RandowWolf], false);
    }
}