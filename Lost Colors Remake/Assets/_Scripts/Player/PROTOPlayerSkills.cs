using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    public void UseSkill()
    {
        SimpleDash power = (SimpleDash)SpellManager.Instance.GetSpell("FireBall");
        //SkillContext context = new(PlayerMain.Instance.Rigidbody2D, this.gameObject, PlayerMain.Instance.PlayerGameObject.transform.forward, 50);
    //    if (power != null) power.Activate(context);
    }
}
