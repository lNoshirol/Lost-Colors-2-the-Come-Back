using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool isDashing;
    private Vector2 positionBeforeDash;
    public LayerMask whatIsGround;
    [SerializeField] private Collider2D playerFeetCollider;

    [Header("Layer For Dash")]
    [SerializeField] int whatIsPlayer;
    [SerializeField] int PlayerFeet;
    [SerializeField] int Dash;


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
        SwitchLayer(true);
        isDashing = true;
        PlayerMain.Instance.UI.DashButton(false);
        yield return new WaitForSeconds(delay);
        isDashing = false;
        PlayerMain.Instance.UI.DashButton(true);
        SwitchLayer(false);
        PlayerMain.Instance.Dashing(false);
        CheckLayer();
    }

    void SwitchLayer(bool isDashing)
    {
        if(isDashing){
            PlayerMain.Instance.gameObject.layer = Dash;
            playerFeetCollider.gameObject.layer = Dash;

        }
        else
        {
            PlayerMain.Instance.gameObject.layer = whatIsPlayer;
            playerFeetCollider.gameObject.layer = PlayerFeet;
        }
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
