using UnityEngine;

public class WolfCloseSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 0.7f;

    public override void Activate(SkillContext context)
    {
        DelayedFunction(() => EnemyDash(context.Agent, context.Strength, context.MaxDistance), timeToWaitBeforeAttack);
        var sound = SoundsManager.Instance;
        int RandowWolf = Random.Range(0, sound.SoundEffectWolfAttackMelee.Length);
        sound.PlaySound(sound.SoundEffectWolfAttackMelee[RandowWolf], false);
    }
}
