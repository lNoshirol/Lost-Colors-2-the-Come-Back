using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class VFXControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int transition1WaitTime;
    [SerializeField] string transition1Name;

    IEnumerator WaitForNext(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(transition1Name, true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        EnemyManager.Instance.RePackInPool(gameObject, ProjectileManager.Instance.ProjectilePools);
    }

    public void OnPlay()
    {
        StartCoroutine(WaitForNext(transition1WaitTime));
    }

}
