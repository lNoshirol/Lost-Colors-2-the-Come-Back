using System.Collections;
using System.Linq.Expressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    private float _baseTime;
    private float _baseFixed;

    [SerializeField]
    private PlayerMain _mainPlayer;

    public bool InSlowMotion = false;

    private void Start()
    {
        _baseFixed = Time.fixedDeltaTime;
        _baseTime = Time.timeScale;
    }

    void Update()
    {
        if (InSlowMotion)
        {
            float origTimeScale = InSlowMotion ? .3f : 1;
            float timeScale = InSlowMotion ? 1 : .3f;
            DOVirtual.Float(origTimeScale, timeScale, .2f, SetTimeScale);
        }
        else
        {
            Time.timeScale = _baseTime;
            Time.fixedDeltaTime = _baseFixed;
        }
    }

    public IEnumerator DoSlowmotion(float value)
    {
        _mainPlayer.Rigidbody2D.linearVelocity = new Vector2(0, 0);
        InSlowMotion = true;
        yield return new WaitForSeconds(value);
        InSlowMotion = false;
    }

    void SetTimeScale(float x)
    {
        Time.timeScale = x;
    }
}
