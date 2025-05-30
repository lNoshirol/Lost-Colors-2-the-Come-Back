using UnityEngine;

[CreateAssetMenu(fileName = "ToileScriptable", menuName = "Toile/Creat ToileStat")]
public class ToileScriptable : ScriptableObject
{
    public float toileTime;
    public float lineDamage;
    public float shapeDamage;
    public float paintAmount;
    public float slowMotionScale;
}
