using Unity.Cinemachine;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private float boundAmount;
    private CinemachineConfiner3D confiner;
    private GameObject activeMapBounds;

    void Start()
    {
        confiner = cinemachine.GetComponent<CinemachineConfiner3D>();
        ApplyMapBounds();
    }

    void ApplyMapBounds()
    {
        confiner.BoundingVolume = activeMapBounds.GetComponentAtIndex<BoxCollider>(4);
    }
}
