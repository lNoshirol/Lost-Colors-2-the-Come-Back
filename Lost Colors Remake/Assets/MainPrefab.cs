using UnityEngine;

public class MainPrefab : MonoBehaviour
{
    private static MainPrefab Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
