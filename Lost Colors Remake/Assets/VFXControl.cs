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
        PlaySound();
        StartCoroutine(WaitForNext(transition1WaitTime));
    }

    public void PlaySound()
    {
        switch (gameObject.name)
        {
            case "VFX_Thunder":
                var sound = SoundsManager.Instance;
                int RandowDeer = Random.Range(0, sound.SoundEffectDeerAttackRanged.Length);
                sound.PlaySound(sound.SoundEffectDeerAttackRanged[RandowDeer], false);
                break;

            case "VFX_FlameThrower":
                var Sound = SoundsManager.Instance;
                int RandomWolf = Random.Range(0, Sound.SoundEffectWolfAttackRanged.Length);
                Sound.PlaySound(Sound.SoundEffectWolfAttackRanged[RandomWolf], false);
                break;
        }
    }
}