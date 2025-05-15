using System.Collections;
using UnityEngine;

public class StickDisappear : MonoBehaviour
{
    public static StickDisappear instance;

    public float timeBeforeDisappear;
    public Coroutine disappearCountdown;

    public GameObject joyastickParent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ClickManager.instance.OnClickStart += CancelDisappear;
        ClickManager.instance.OnClickEnd += StartCoroutineDisapear;
    }

    public void StartCoroutineDisapear()
    {
        if (transform.parent.gameObject.activeSelf)
        {
            disappearCountdown = StartCoroutine(DisappearCountDown());
        }
    }

    IEnumerator DisappearCountDown()
    {
        yield return new WaitForSeconds(timeBeforeDisappear);
        joyastickParent.SetActive(false);
    }

    private void CancelDisappear()
    {
        if (disappearCountdown != null)
        {
            StopCoroutine(disappearCountdown);
        }
    }
}
