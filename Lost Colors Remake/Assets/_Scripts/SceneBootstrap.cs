using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    private void Start()
    {
        string sceneToLoad = SaveSystemJson.Instance.GetLastSavedScene();
        Debug.Log(SaveSystemJson.Instance);
        SaveSystemJson.Instance.SetPlayerLastPosition();
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

}