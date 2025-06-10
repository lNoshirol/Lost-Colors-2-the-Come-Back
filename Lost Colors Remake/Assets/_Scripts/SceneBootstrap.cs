using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    private void Start()
    {
        //string sceneToLoad = SaveSystem.Instance.GetLastSavedScene();
        //SaveSystem.Instance.SetPlayerLastPosition();
        //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.LoadScene("FR_SP_01_Clean", LoadSceneMode.Additive);
    }

}