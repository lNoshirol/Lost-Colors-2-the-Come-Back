using System.Collections;
using UnityEngine;

public class StickDisappear : MonoBehaviour
{
    public float timeBeforeDisappear;
    public Coroutine disappearCountdown;

    IEnumerator DisappearCountDown()
    {
        yield return new WaitForSeconds(timeBeforeDisappear);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StopCoroutine(disappearCountdown);
    }
}
