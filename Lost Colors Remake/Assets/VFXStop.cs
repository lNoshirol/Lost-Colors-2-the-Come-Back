using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXStop : MonoBehaviour
{

    [SerializeField] float timeToFalse;
    [SerializeField] VisualEffect effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Waitandfalse(timeToFalse));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Waitandfalse(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        effect.Stop();
        yield return new WaitForSeconds(timeToWait+1);
        EnemyManager.Instance.RePackInPool(gameObject, EnemyManager.Instance.vfxPool);
    }
}
