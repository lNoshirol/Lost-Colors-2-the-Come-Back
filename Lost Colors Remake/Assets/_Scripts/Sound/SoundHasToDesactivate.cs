using System.Collections;
using UnityEngine;

public class SoundHasToDesactivate : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    public IEnumerator WaitToDesactivate()
    {
        yield return new WaitForSeconds(m_AudioSource.clip.length);
        SoundsManager.Instance._poolObjectSound.GetPoolSound().Stock(gameObject);
    }
}
