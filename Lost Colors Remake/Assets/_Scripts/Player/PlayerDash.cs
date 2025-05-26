using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool isDashing;
    private Vector2 positionBeforeDash;
    public LayerMask whatIsGround;
    
    public void DashOnClick()
    {
        if (isDashing) return;
        positionBeforeDash = PlayerMain.Instance.PlayerGameObject.transform.position;
        PlayerMain.Instance.Dashing(true);

        SimpleDash dash = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
        SkillContext context = new(PlayerMain.Instance.Rigidbody2D, PlayerMain.Instance.gameObject, PlayerMain.Instance.Move._moveInput, 15);
        dash.Activate(context);
        StartCoroutine(WaitDash(0.5f));
    }

    IEnumerator WaitDash(float delay)
    {
       PlayerMain.Instance.PlayerGameObject.layer = 11;
       isDashing = true;

       PlayerMain.Instance.UI.DashButton(false);
       yield return new WaitForSeconds(delay);
       isDashing = false;
       PlayerMain.Instance.UI.DashButton(true);
       PlayerMain.Instance.PlayerGameObject.layer = 8;
        PlayerMain.Instance.Dashing(false);
        CheckLayer();
    }

    void TakeDamageAndTP()
    {
        PlayerMain.Instance.Health.PlayerLoseHP(1);
        PlayerMain.Instance.PlayerGameObject.transform.position = positionBeforeDash;
    }

    void CheckLayer()
    {
        bool isGrounded = Physics2D.OverlapCircle(PlayerMain.Instance.PlayerGameObject.transform.position, 0.1f, whatIsGround);
        if (isGrounded) return;
        else TakeDamageAndTP();
    }
}
