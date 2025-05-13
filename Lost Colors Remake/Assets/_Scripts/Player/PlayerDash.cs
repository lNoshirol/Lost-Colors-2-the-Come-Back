using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public void DashOnClick()
    {
        SimpleDash dash = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
        SkillContext context = new(PlayerMain.Instance.Rigidbody2D, PlayerMain.Instance.gameObject, PlayerMain.Instance.PlayerMesh.transform.forward, 50);
        dash.Activate(context);
    }
}
