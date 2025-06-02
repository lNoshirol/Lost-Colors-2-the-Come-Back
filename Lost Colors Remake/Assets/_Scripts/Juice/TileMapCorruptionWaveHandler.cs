using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class TileMapCorruptionWaveHandler : MonoBehaviour
{
    [Header("Wave values")]
    [SerializeField] private Vector2 _waveStartPoint, _waveRatio;
    [SerializeField] private float _waveWidth, _waveDuration, _waveMaxRange;

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
        Vector2 overlapSize = new(_waveRatio.y, _waveRatio.x);
        Collider2D[] hits = Physics2D.OverlapBoxAll(_waveStartPoint, overlapSize * 1.5f * _waveProgress, 0f, 2 | 4 | 7);

        foreach (var hit in hits)
        {
            if (_touchedPaintables.Contains(hit.transform)) continue;
            Vector2 pos = hit.transform.position;
            Vector2 delta = pos - _waveStartPoint;

            float ellipseValue = (delta.x * delta.x ) / ((_waveRatio.x * _waveProgress) * (_waveRatio.x * _waveProgress)) + (delta.y * delta.y) / ((_waveRatio.y * _waveProgress)* (_waveRatio.y * _waveProgress));
            if (Mathf.Abs(ellipseValue -1f) < 0.05f)
            {
                print("touché " + hit.gameObject.name);
                hit.TryGetComponent(out SpriteRenderer spriteRenderer);
                spriteRenderer.material.SetTexture("_ColoredTex", PropsSpriteHandler.Instance.PropsColoredTextures[spriteRenderer.sprite.ToString().Replace("_BW (UnityEngine.Sprite)", "")].texture);
                hit.GetComponent<Renderer>().material.DOFloat(1f, "_Transition", 2f);
            }
        }
    }

    public void Anim()
    {
        Setup();

        _vfx.Play();

        foreach (var renderer in _tilemapRenderers)
        {
            DOTween.To(() => _waveProgress, x => _waveProgress = x, _waveMaxRange, _waveDuration);
            renderer.material.DOFloat(_waveMaxRange, "_WaveProgress", _waveDuration);

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
        Gizmos.color = Color.cyan;
        Vector2 overlapSize = new(_waveRatio.y, _waveRatio.x);
        Gizmos.DrawWireCube(_waveStartPoint, overlapSize * 1.5f * _waveProgress);    
    }
}
