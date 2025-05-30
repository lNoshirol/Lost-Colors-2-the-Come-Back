using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class CeciSeraSuppriméSubrepticement : MonoBehaviour
{
    [Header("Wave values")]
    [SerializeField] private Vector2 _waveStartPoint, _waveRatio;
    [SerializeField] private float _waveWidth, _waveDuration;

    [Header("WaveVFX")]
    [SerializeField] private VisualEffect _vfx;

    [Header("Affected tilemaps")]
    [SerializeField] private List<TilemapRenderer> _tilemapRenderers = new();

    // Elliptic propagation
    private float _waveProgress = 0f;
    private HashSet<Transform> _touchedPaintables = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anim();
        }

        // Detect every paintable in wave
        Collider2D[] hits = Physics2D.OverlapBoxAll(_waveStartPoint, _waveRatio * _waveProgress, 0f);

        foreach (var hit in hits)
        {
            if (_touchedPaintables.Contains(hit.transform)) continue;

            Vector2 pos = hit.transform.position;
            Vector2 delta = pos - _waveStartPoint;

            float ellipseValue = (delta.x * delta.x ) / ((_waveRatio.x * _waveProgress) * (_waveRatio.x * _waveProgress)) + (delta.y * delta.y) / ((_waveRatio.y * _waveProgress)* (_waveRatio.y * _waveProgress));
            if (Mathf.Abs(ellipseValue -1f) < 0.05f)
            {
                print(hit.gameObject.name + "TOUCHE COULE ♫");
            }
        }
    }

    public void Anim()
    {
        Setup();
        //_vfx.SetFloat("_WaveProgress", 0f);

        //_vfx.Play();

        // Anime Progress de 0 à 1 en 2 secondes
        //DOTween.To(
        //    () => _vfx.GetFloat("_WaveProgress"),
        //    x => _vfx.SetFloat("_WaveProgress", x),
        //    40f,            // valeur finale
        //    _waveDuration             // durée
        //); 


        foreach (var renderer in _tilemapRenderers)
        {
            DOTween.To(() => _waveProgress, x => _waveProgress = x, 40, _waveDuration);
            renderer.material.DOFloat(40f, "_WaveProgress", _waveDuration);
        }
    }

    private void Setup()
    {
        MaterialPropertyBlock mPropertyBlock = new();
        mPropertyBlock.SetVector("_WaveDiffusionStartPoint", _waveStartPoint);
        mPropertyBlock.SetVector("_WaveRatio", _waveRatio);

        foreach (var renderer in _tilemapRenderers)
        {
            renderer.material.DOFloat(0f, "_WaveProgress", 0f);
            renderer.SetPropertyBlock(mPropertyBlock);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_waveStartPoint, 0.3f);
    }
}
