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
        Debug.LogWarning("YO JE SUIS LE LOUP");
        PlaySound();
        StartCoroutine(WaitForNext(transition1WaitTime));
    }

    public void PlaySound()
    {
        Debug.Log("JE JOUE UN SON");
        if (gameObject.name.StartsWith("VFX_Thunder"))
        {
            Debug.Log("VFX_Thunder");
            var sound = SoundsManager.Instance;
            int RandowDeer = Random.Range(0, sound.SoundEffectDeerAttackRanged.Length);
            sound.PlaySound(sound.SoundEffectDeerAttackRanged[RandowDeer], false);
        }
        else if (gameObject.name.StartsWith("VFX_FlameThrower"))
        {
            Debug.Log("VFX_FlameThrower");
            var sound = SoundsManager.Instance;
            int RandowDeer = Random.Range(0, sound.SoundEffectWolfAttackRanged.Length);
            sound.PlaySound(sound.SoundEffectWolfAttackRanged[RandowDeer], false);
        }
    }
}