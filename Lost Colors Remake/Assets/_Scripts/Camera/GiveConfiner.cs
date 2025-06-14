using Unity.Cinemachine;
using UnityEngine;

public class GiveConfiner : MonoBehaviour
{
    private void Start()
    {
        CameraMain.Instance.gameObject.GetComponent<CinemachineConfiner2D>().BoundingShape2D = GetComponent<PolygonCollider2D>();
    }
}