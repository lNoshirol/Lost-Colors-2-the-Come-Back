using NUnit.Framework;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "ProjectileDatas", menuName = "Scriptable Objects/ProjectileDatas")]
public class ProjectileDatas : ScriptableObject
{
    [Header("Stats")]
    public string Name;
    public int Damage;
    public float Speed;

    [Header("Feedback")]
    public VisualEffect OnLaunchVFX, OnHitVFX;
    public AudioClip OnLaunchSFX, OnHitSFX;
    
}
