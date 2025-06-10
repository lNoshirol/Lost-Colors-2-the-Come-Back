using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    private void Start()
    {
        string sceneToLoad = "FR_SP_01_Clean";
        //string sceneToLoad = SaveSystem.Instance.GetLastSavedScene();
        //SaveSystem.Instance.SetPlayerLastPosition();
        SceneManager.LoadScene(sceneToLoad);
        WorldMain.Instance.CurrentRoomName = SceneManager.GetActiveScene().name;
    }

}