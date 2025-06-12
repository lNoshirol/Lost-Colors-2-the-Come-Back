using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class VFXControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float transition1WaitTime;
    [SerializeField] string transition1Name;

    IEnumerator WaitForNext(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(transition1Name, true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        EnemyManager.Instance.RePackInPool(gameObject, EnemyManager.Instance.vfxPool);
    }

    public void OnPlay()
    {
        StartCoroutine(WaitForNext(transition1WaitTime));
    }
}