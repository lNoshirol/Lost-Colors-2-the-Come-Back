using System.Collections;
using System.Threading.Tasks;
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
}
