using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    private float _baseTime;
    private float _baseFixed;

    private PlayerMain _mainPlayer;

    public bool InSlowMotion = false;

    private void Start()
    {
        _mainPlayer = FindAnyObjectByType<PlayerMain>();
        _baseFixed = Time.fixedDeltaTime;
        _baseTime = Time.timeScale;
    }

    void Update()
    {
        //Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        if (InSlowMotion)
        {
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = Time.timeScale*2f;
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
        Time.timeScale = _baseTime;
        Time.fixedDeltaTime = _baseFixed;
    }
}
