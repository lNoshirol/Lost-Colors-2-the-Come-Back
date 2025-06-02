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
    private float _timeScale;

    public bool InSlowMotion = false;

    private void Awake()
    {
        _timeScale = PlayerMain.Instance.toileInfo.slowMotionScale;
    }

    private void Start()
    {
        _baseFixed = Time.fixedDeltaTime;
        _baseTime = Time.timeScale;
    }

    void Update()
    {
        if (InSlowMotion)
        {
            float origTimeScale = InSlowMotion ? _timeScale : 1;
            float timeScale = InSlowMotion ? 1 : _timeScale;
            DOVirtual.Float(origTimeScale, timeScale, .2f, SetTimeScale);
        }
    }

    public IEnumerator DoSlowmotion(float value)
    {
        PlayerMain.Instance.Rigidbody2D.linearVelocity = new Vector2(0, 0);
        InSlowMotion = true;
        yield return new WaitForSeconds(value);
        InSlowMotion = false;
        Time.timeScale = _baseTime;
        Time.fixedDeltaTime = _baseFixed;
    }

    void SetTimeScale(float x)
    {
        Time.timeScale = x;
    }
}
