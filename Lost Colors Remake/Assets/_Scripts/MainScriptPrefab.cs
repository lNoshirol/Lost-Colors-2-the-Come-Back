using UnityEngine;

public class MainScriptPrefab : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
