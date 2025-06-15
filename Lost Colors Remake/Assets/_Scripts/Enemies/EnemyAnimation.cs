using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] public Animator enemyAnimator;
    [SerializeField] private AnimatorOverrideController enemyAnimationOverrideBW;
    [SerializeField] private AnimatorOverrideController enemyAnimationOverrideColor;
    public GameObject currentVfxSocket;
    [SerializeField] private GameObject vfxSocketRight;
    [SerializeField] private GameObject vfxSocketLeft;

    public EnemyMain enemyMain;
    [SerializeField] float dirX;

    [Header("Enemy Hit Frames")]
    [SerializeField] private Sprite _whiteHitFrame;
    [SerializeField] private Sprite _BWHitFrame;
    [SerializeField] private Sprite _coloredHitFrame;

    private void Start()
    {
        enemyAnimator.runtimeAnimatorController = enemyAnimationOverrideBW;
    }
    // Get the direction for Patrolling & Chase
    public void GetDirectionXAnimAgent()
    {
        dirX = enemyMain.agent.velocity.x;
        if (dirX != 0 && enemyMain.canLookAt)
        {
            enemyAnimator.SetBool("IsMoving", true);
            enemyAnimator.SetFloat("X", dirX);
        }
        else
        {
            enemyAnimator.SetBool("IsMoving", false);
        }
        GetGoodSocketVFXOrientation();
    }

    // Get the direction for Attack
    public void GetDirectionXAnimPlayer()
    {
        dirX = enemyMain.player.position.x - enemyMain.transform.position.x;
        if (dirX != 0 && enemyMain.canLookAt)
        {
            enemyAnimator.SetFloat("X", dirX);
        }
        GetGoodSocketVFXOrientation();
    }

    public void GetGoodSocketVFXOrientation()
    {
        if (enemyMain.agent.velocity.x > 0) currentVfxSocket = vfxSocketRight;
        else
        {
            currentVfxSocket = vfxSocketLeft;
        }
    }

    public void SwitchAnimatorToColor()
    {
        enemyAnimator.runtimeAnimatorController = enemyAnimationOverrideColor;
    }

    public void SetAnimTransitionParameter(string AttackType, bool yesOrNo)
    {
        enemyAnimator.SetBool(AttackType, yesOrNo);
    }

    public IEnumerator OnDieAnim()
    {
        enemyAnimator.enabled = false;
        enemyMain.agent.isStopped = true;
        enemyMain.spriteRenderer.sprite = _whiteHitFrame;
        yield return new WaitForSeconds(0.1f);
        enemyMain.spriteRenderer.sprite = _BWHitFrame;
        yield return new WaitForSeconds(0.1f);
        enemyMain.spriteRenderer.material.SetTexture("_ColoredTex", _coloredHitFrame.texture);
        enemyMain.spriteRenderer.material.DOFloat(1f, "_Transition", 2f);
        yield return new WaitForSeconds(2.1f);
        enemyMain.spriteRenderer.material.DOFloat(0f, "_Transition", 0.01f);
        enemyAnimator.enabled = true;
        enemyMain.agent.isStopped = false;
        enemyMain.ColorSwitch();
    }

    public void HitAnim()
    {

    }
}
