using System.Collections;
using System.Linq.Expressions;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    private float _baseTime;
    private float _baseFixed;
    private float _timeScale;

    public bool InSlowMotion = false;

    public Volume volume;

    private ChromaticAberration _chromaticAberration;
    private LensDistortion _lensDistortion;

    private void Start()
    {
        _timeScale = PlayerMain.Instance.toileInfo.slowMotionScale;
        _baseFixed = Time.fixedDeltaTime;
        _baseTime = Time.timeScale;

        if (volume.profile.TryGet(out ChromaticAberration chromatic))
        {
            _chromaticAberration = chromatic;
        }

        if (volume.profile.TryGet(out LensDistortion lens))
        {
            _lensDistortion = lens;
        }
    }

    void Update()
    {
        if (InSlowMotion)
        {
            float origTimeScale = InSlowMotion ? _timeScale : 1;
            float timeScale = InSlowMotion ? 1 : _timeScale;
            Debug.LogWarning(timeScale);
            DOVirtual.Float(origTimeScale, timeScale, .2f, SetTimeScale);
        }
    }

    public IEnumerator DoSlowmotion(float value)
    {
        PlayerMain.Instance.Rigidbody2D.linearVelocity = Vector2.zero;
        InSlowMotion = true;


        _chromaticAberration.intensity.value = 0f;
        _lensDistortion.intensity.value = 0f;

        DOTween.To(() => _chromaticAberration.intensity.value, x => _chromaticAberration.intensity.value = x,0.85f,0.3f).SetEase(Ease.InBounce);
        DOTween.To(() => _lensDistortion.intensity.value, x => _lensDistortion.intensity.value = x,0.3f,0.3f).SetEase(Ease.InOutElastic);

        yield return new WaitForSeconds(value);

        StopSlowMotion();
    }


    void SetTimeScale(float x)
    {
        Time.timeScale = x * 1.5F;
    }

    public void StopSlowMotion()
    {
        InSlowMotion = false;
        Time.timeScale = _baseTime;
        Time.fixedDeltaTime = _baseFixed;

        DOTween.To(() => _chromaticAberration.intensity.value, x => _chromaticAberration.intensity.value = x, 0f, 0.3f).SetEase(Ease.InBounce);
        DOTween.To(() => _lensDistortion.intensity.value, x => _lensDistortion.intensity.value = x, 0.0f, 0.3f).SetEase(Ease.InOutElastic);
    }
}
