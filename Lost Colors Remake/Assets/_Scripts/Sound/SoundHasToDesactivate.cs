using System.Collections;
using UnityEngine;

public class SoundHasToDesactivate : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitToDesactivate());
    }

    private IEnumerator WaitToDesactivate()
    {
        yield return new WaitForSeconds(1.0f);
        SoundsManager.Instance._poolObjectSound.GetPoolSound().Stock(this.gameObject);
    }
}
