using UnityEngine;

public class WorldPaintable : MonoBehaviour
{
    [SerializeField] private TileMapCorruptionWaveHandler colorWaveHandler;

    private void Update()
    {
        //print(IsInEllipse(colorWaveHandler._waveStartPoint, colorWaveHandler._waveRatio, colorWaveHandler._waveWidth));
    }

    private bool IsInEllipse(Vector2 ellipseCenter, Vector2 ellipseRatio, float waveWidth)
    {
        Vector2 targetPos = this.transform.position;
        float x = targetPos.x;
        float y = targetPos.y;
        float k = ellipseCenter.x;
        float h = ellipseCenter.y;
        float rx = ellipseRatio.x * colorWaveHandler.WaveProgress;
        float ry = ellipseRatio.y * colorWaveHandler.WaveProgress;

        return ((Mathf.Pow(x - h, 2) / Mathf.Pow(rx, 2)) + (Mathf.Pow(y - k, 2) / Mathf.Pow(ry, 2)) <= 1);       
    }
}
