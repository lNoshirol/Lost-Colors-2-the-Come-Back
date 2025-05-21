using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    private void Start()
    {
        // Load your first gameplay scene additively
        SceneManager.LoadScene("FR_SP_01", LoadSceneMode.Additive);
    }

}