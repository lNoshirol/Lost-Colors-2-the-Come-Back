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
        //string sceneToLoad = SaveSystem.Instance.GetLastSavedScene();
        //SaveSystem.Instance.SetPlayerLastPosition(); get error that stop all things

        //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        await Task.Yield();
        _initTCS.SetResult(true);
        Debug.Log("Bootstrap initialization complete.");
    }

    public void InitializeAllSingleton()
    {
        Debug.Log("=== InitializeAllSingleton START ===");

        for (int i = 0; i < singletonPrefabs.Count; i++)
        {
            var entry = singletonPrefabs[i];
            Debug.Log($"Processing entry {i}: {entry.singletonReference?.GetType()?.Name}");

            if (entry.singletonReference == null)
            {
                Debug.LogError($"Entry {i}: singletonReference is NULL");
                continue;
            }

            if (entry.prefab == null)
            {
                Debug.LogError($"Entry {i}: prefab is NULL");
                continue;
            }

            var type = entry.singletonReference.GetType();
            Debug.Log($"Entry {i}: Type = {type.Name}");

            PropertyInfo instanceProp = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

            if (instanceProp == null)
            {
                Debug.LogError($"Entry {i}: No Instance property found for {type.Name}");
                continue;
            }

            var value = instanceProp.GetValue(null);
            Debug.Log($"Entry {i}: Instance value = {value} (is null: {value == null})");

            if (value == null)
            {
                Debug.Log($"Entry {i}: Creating prefab for {type.Name}");
                GameObject created = Instantiate(entry.prefab);
                Debug.Log($"Entry {i}: Created GameObject: {created.name}");
            }
            else
            {
                Debug.Log($"Entry {i}: Instance already exists, skipping creation");
            }
        }

        Debug.Log("=== InitializeAllSingleton END ===");
    }
}


[System.Serializable]
public class SingletonPrefabEntry
{
    public GameObject prefab;
    public MonoBehaviour singletonReference;
}
