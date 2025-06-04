using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.Threading.Tasks;

public class Bootstrap : SingletonCreatorBootStrap<Bootstrap>
{
    [Header("Singleton Prefabs")]
    public List<SingletonPrefabEntry> singletonPrefabs;

    private TaskCompletionSource<bool> _initTCS = new();
    public Task WaitUntilInitializedAsync() => _initTCS.Task;


    private async void Start()
    {
        InitializeAllSingleton();

        string sceneToLoad = SaveSystem.Instance.GetLastSavedScene();
        SaveSystem.Instance.SetPlayerLastPosition();

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);

        await Task.Yield(); 
        _initTCS.SetResult(true);
        Debug.Log("Bootstrap initialization complete.");
    }

    public void InitializeAllSingleton()
    {
        foreach (var entry in singletonPrefabs)
        {
            if (entry.singletonReference == null || entry.prefab == null)
                continue;

            var type = entry.singletonReference.GetType();
            PropertyInfo instanceProp = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

            if (instanceProp != null)
            {
                var value = instanceProp.GetValue(null);
                if (value == null)
                {
                    Instantiate(entry.prefab);
                }
            }
        }
    }
}


[System.Serializable]
public class SingletonPrefabEntry
{
    public GameObject prefab;
    public MonoBehaviour singletonReference;
}
