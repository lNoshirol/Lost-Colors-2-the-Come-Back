using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyAnimation : MonoBehaviour
{
    public Animator enemyAnimator;
    public EnemyMain enemyMain;
    [SerializeField] float dirX;

    // Get the direction for Patrolling
    public void GetDirectionXAnimAgent()
    {
        dirX = enemyMain.agent.velocity.x;
        if (dirX != 0)
        {
            enemyAnimator.SetBool("IsMoving", true);
            enemyAnimator.SetFloat("X", dirX);
        }
        else
        {
            enemyAnimator.SetBool("IsMoving", false);
        }
    }

    // Get the direction for Chasing and Attack
    public void GetDirectionXAnimPlayer()
    {
        dirX = enemyMain.player.position.x - enemyMain.transform.position.x;
        if (dirX != 0)
        {
            enemyAnimator.SetBool("IsMoving", true);
            enemyAnimator.SetFloat("X", dirX);
        }
        else
        {
            enemyAnimator.SetBool("IsMoving", false);
        }
    }
}
