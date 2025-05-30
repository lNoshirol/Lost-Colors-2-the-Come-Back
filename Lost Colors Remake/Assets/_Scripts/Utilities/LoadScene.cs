using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void Onclick(string SceneToLoad)
    {
        SceneManager.LoadScene(sceneName);
    }
}
