using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private bool isDashing;
    [SerializeField] float dashTime;
    [SerializeField] float dashPower;
    private Vector2 positionBeforeDash;
    public LayerMask whatIsGround;
    [SerializeField] private Collider2D playerFeetCollider;

    [Header("Layer For Dash")]
    [SerializeField] int whatIsPlayer;
    [SerializeField] int playerFeetLayer;
    [SerializeField] int dashLayer;

    public void DashOnClick()
    {
        if (isDashing) return;
        Dash();
    }

    void Dash()
    {
        var Sound = SoundsManager.Instance;
        int RandomSound = Random.Range(0, Sound.Dash.Length);
        Sound.PlaySound(Sound.Dash[RandomSound], false);

        positionBeforeDash = PlayerMain.Instance.PlayerGameObject.transform.position;
        PlayerMain.Instance.Rigidbody2D.linearVelocity = PlayerMain.Instance.Move._moveInput * dashPower;
        isDashing = true;
        PlayerMain.Instance.Dashing(true);
        PlayerMain.Instance.PlayerDashVFX.LaunchFeedback();
        StartCoroutine(WaitDash(dashTime));
    }

    IEnumerator WaitDash(float delay)
    {
        SwitchLayer(isDashing);
        PlayerMain.Instance.UI.DashButton(false);
        yield return new WaitForSeconds(delay);
        isDashing = false;
        PlayerMain.Instance.UI.DashButton(true);
        SwitchLayer(isDashing);
        PlayerMain.Instance.Dashing(false);
        CheckLayer();
    }

    void SwitchLayer(bool isDashing)
    {
        if(isDashing){
            PlayerMain.Instance.gameObject.layer = dashLayer;
            playerFeetCollider.gameObject.layer = dashLayer;

        }
        else
        {
            PlayerMain.Instance.gameObject.layer = whatIsPlayer;
            playerFeetCollider.gameObject.layer = playerFeetLayer;
        }
    }
    void CheckLayer()
    {
        bool isGrounded = Physics2D.OverlapCircle(PlayerMain.Instance.PlayerGameObject.transform.position, 0.1f, whatIsGround);
        if (isGrounded) return;
        else TakeDamageAndTP();
    }

    void TakeDamageAndTP()
    {
        PlayerMain.Instance.Health.PlayerLoseHP(0.5f);
        PlayerMain.Instance.PlayerGameObject.transform.position = positionBeforeDash;
    }
}
