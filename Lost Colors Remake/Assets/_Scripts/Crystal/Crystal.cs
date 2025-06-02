using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{
    public bool IsColorized;
    [field: SerializeField] public TileMapCorruptionWaveHandler ColorWaveHandler;  

    void Start()
    {
        //CrystalManager.Instance.AddToDict(this, IsColorized, SceneManager.GetActiveScene().name);   
    }
}
