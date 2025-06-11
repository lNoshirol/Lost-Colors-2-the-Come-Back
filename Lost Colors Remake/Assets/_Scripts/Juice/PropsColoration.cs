using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PropsColoration : MonoBehaviour
{
    private float _timeBeforeTouch;

    private async void Start()
    {
        await Task.Delay(1);

        if (CrystalManager.Instance._ListCrystal.Count > 0)
        {
            CrystalManager.Instance._ListCrystal[0].Crystal.ColorWaveHandler.OnColorizeEvent += LaunchCoroutine;
            float distance = Vector2.Distance(CrystalManager.Instance._ListCrystal[0].Crystal.transform.position, this.transform.position);
            float vitesse = CrystalManager.Instance._ListCrystal[0].Crystal.ColorWaveHandler.WaveDuration;
            _timeBeforeTouch = distance / vitesse;
        }

        //print($"Nom : {this.name}, distance : {distance}, vitesse : {vitesse}, cd : {_timeBeforeTouch}");
    }

    private void LaunchCoroutine()
    {
        StartCoroutine(OnCrystalColorize());
    }

    private IEnumerator OnCrystalColorize()
    {
        yield return new WaitForSeconds(_timeBeforeTouch);
        TryGetComponent(out SpriteRenderer spriteRenderer);
        spriteRenderer.material.SetTexture("_ColoredTex", PropsSpriteHandler.Instance.PropsColoredTextures[spriteRenderer.sprite.ToString().Replace("_BW (UnityEngine.Sprite)", "")].texture);
        spriteRenderer.material.DOFloat(1f, "_Transition", 2f);
    }

    // CHAT GPT [OBSOLETE]
    public static float DistanceToEllipse(Vector2 point, float a, float b, int maxIterations = 100, float tolerance = 1e-6f)
    {
        float theta = Mathf.Atan2(point.y, point.x); // Bonne estimation initiale
        float px = point.x;
        float py = point.y;

        for (int i = 0; i < maxIterations; i++)
        {
            float cosT = Mathf.Cos(theta);
            float sinT = Mathf.Sin(theta);

            float ex = a * cosT;
            float ey = b * sinT;

            float dx = ex - px;
            float dy = ey - py;

            float dD = 2 * (dx * (-a * sinT) + dy * (b * cosT));
            float d2D = 2 * ((-a * sinT) * (-a * sinT) + dx * (-a * cosT) + (b * cosT) * (b * cosT) + dy * (-b * sinT));

            if (Mathf.Abs(dD) < tolerance)
                break;

            theta = theta - dD / d2D;
        }

        // Coordonnées du point le plus proche sur l’ellipse
        float closestX = a * Mathf.Cos(theta);
        float closestY = b * Mathf.Sin(theta);

        return Vector2.Distance(new Vector2(closestX, closestY), point);
    }

}
