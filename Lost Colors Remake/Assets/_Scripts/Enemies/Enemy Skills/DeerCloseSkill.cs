using UnityEngine;

public class DeerCloseSkill : SkillParentClass
{
    private float timeToWaitBeforeAttack = 0.7f;
    public override void Activate(SkillContext context)
    {
        DelayedFunction(() => EnemyDash(context.Agent, context.Strength, context.MaxDistance), timeToWaitBeforeAttack);
        var sound = SoundsManager.Instance;
        int RandowDeer = Random.Range(0, sound.SoundEffectDeerAttackMelee.Length);
        sound.PlaySound(sound.SoundEffectDeerAttackMelee[RandowDeer], false);
    }
}

