using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool isDashing;
    public void DashOnClick()
    {
        if (isDashing) return;

        Debug.Log("PlayerDash");
        SimpleDash dash = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
        SkillContext context = new(PlayerMain.Instance.Rigidbody2D, PlayerMain.Instance.gameObject, PlayerMain.Instance.Move._moveInput, 15);
        dash.Activate(context);
        StartCoroutine(WaitDash(0.5f));
    }

    IEnumerator WaitDash(float delay)
    {
       isDashing = true;
       PlayerMain.Instance.UI.DashButton(false);
       yield return new WaitForSeconds(delay);
       isDashing = false;
       PlayerMain.Instance.UI.DashButton(true);
    }
}
