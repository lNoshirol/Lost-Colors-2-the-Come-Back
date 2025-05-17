using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCorruptionWaveHandler : MonoBehaviour
{
    [SerializeField] public Vector2 _waveStartPoint, _waveRatio;
    public float _waveWidth, WaveProgress;


    private TilemapRenderer _tilemapRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        
        MaterialPropertyBlock mPropertyBlock = new();
        mPropertyBlock.SetVector("_WaveDiffusionStartPoint", _waveStartPoint);
        _tilemapRenderer.SetPropertyBlock(mPropertyBlock);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            WaveAnim();
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Reset();
        }
    }

    public void Reset()
    {
        MaterialPropertyBlock propertyBlock = new();
        propertyBlock.SetVector("_WaveDiffusionStartPoint", _waveStartPoint);
        propertyBlock.SetFloat("_WaveProgress", 0f);
        _tilemapRenderer.SetPropertyBlock(propertyBlock);
    }

    private void WaveAnim()
    {
        MaterialPropertyBlock mPropertyBlock = new();
        _tilemapRenderer.material.DOFloat(0f, "_WaveProgress", 0f);
        WaveProgress = _tilemapRenderer.material.GetFloat("_WaveProgress");
        mPropertyBlock.SetVector("_WaveDiffusionStartPoint", _waveStartPoint);
        _tilemapRenderer.SetPropertyBlock(mPropertyBlock);
        _tilemapRenderer.material.DOFloat(20f, "_WaveProgress", 2.4f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_waveStartPoint, 0.3f);
    }
}
