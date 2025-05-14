using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public void DashOnClick()
    {
        SimpleDash dash = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
        SkillContext context = new(PlayerMain.Instance.Rigidbody2D, PlayerMain.Instance.gameObject, PlayerMain.Instance.Move._moveInput, 35);
        dash.Activate(context);
    }
}
