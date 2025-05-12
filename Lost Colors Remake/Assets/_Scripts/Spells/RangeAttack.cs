using UnityEngine;
using UnityEngine.VFX;

public abstract class RangeAttack : SkillParentClass
{
    public float Damage;
    public float TravelSpeed;

    public GameObject LaunchVFX;
    public GameObject TravelVFX;
    public GameObject HitVFX;
}
