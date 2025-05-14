using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCorruptionWaveHandler : MonoBehaviour
{
    [SerializeField] private Vector2 _waveStartPoint;
    private TilemapRenderer _tilemapRenderer;
    private Material _material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        
        MaterialPropertyBlock mPropertyBlock = new();
        mPropertyBlock.SetVector("_WaveDiffusionCenter", _waveStartPoint);
        _tilemapRenderer.SetPropertyBlock(mPropertyBlock);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            WaveAnim();
        }
    }

    private void WaveAnim()
    {
        _tilemapRenderer.material.DOFloat(20f, "_WaveProgress", 2.4f);
        //_tilemapRenderer.material.DOFloat(5f, "_WaveWidth", 2.4f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_waveStartPoint, 0.3f);
    }
}
