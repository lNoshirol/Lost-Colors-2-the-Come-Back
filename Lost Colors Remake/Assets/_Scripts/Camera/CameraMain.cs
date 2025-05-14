using UnityEngine;

public class CameraMain : MonoBehaviour
{

    public static CameraMain Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void CenterCameraAtPosition(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }
}
