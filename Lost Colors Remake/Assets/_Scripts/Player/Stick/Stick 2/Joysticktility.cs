using UnityEngine;

public class Joysticktility
{
    public static Vector3 GetClosestPointOnRect(RectTransform rectTransform, Vector2 screenPoint, Camera uiCamera = null)
    {
        // Convertir le point d'écran en local dans le rect
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, uiCamera, out Vector2 localPoint);

        // Obtenir les limites du rect en local
        Rect rect = rectTransform.rect;

        // Clamper à l'intérieur du rect
        float clampedX = Mathf.Clamp(localPoint.x, rect.xMin, rect.xMax);
        float clampedY = Mathf.Clamp(localPoint.y, rect.yMin, rect.yMax);
        Vector2 clampedLocal = new Vector2(clampedX, clampedY);

        // Convertir en world position
        Vector3 worldPoint = rectTransform.TransformPoint(clampedLocal);
        return worldPoint;
    }
}
