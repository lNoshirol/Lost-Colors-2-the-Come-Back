using UnityEngine;

[CreateAssetMenu(fileName = "ToileData", menuName = "Scriptable Objects/ToileData", order = 2)]
public class ToileScriptable : ScriptableObject
{
    public float toileTime;
    public float lineDamage;
    public float shapeDamage;
    public float paintAmount;
    public float slowMotionScale;
}