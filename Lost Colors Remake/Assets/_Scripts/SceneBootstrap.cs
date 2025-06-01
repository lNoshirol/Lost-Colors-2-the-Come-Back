using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("FR_SP_01_Clean", LoadSceneMode.Additive);
    }

}