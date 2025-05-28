using System.Collections;
using UnityEngine;

public class JuiceManager : MonoBehaviour
{
    public static JuiceManager Instance;

    bool waiting;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    
    // TEMP
    public void WhiteSprite()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 3; i++)
        {
            color = Color.black;
            Wait(0.2f);
            color = Color.white;
        }
    }


    public void PlayerHit(float stopDuration)
    {
        if (waiting) return;
        Time.timeScale = 0f;
        StartCoroutine(Wait(stopDuration));

    }

    public IEnumerator Wait(float waitDuration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(waitDuration);
        Time.timeScale = 1f;
        waiting = false;
    }


}
