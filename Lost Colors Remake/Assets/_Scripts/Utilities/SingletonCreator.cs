using System.Threading.Tasks;
using UnityEngine;

// video link : https://www.youtube.com/watch?v=-AJ4J-lph6A

// On attend une ini de la part du boostrap pour pas avoir de soucis de conflit d'awake
public abstract class AsyncSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    private TaskCompletionSource<bool> _initTCS = new();

    public Task WaitUntilInitializedAsync() => _initTCS.Task;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
        DontDestroyOnLoad(gameObject);

        _ = InitializeAsync(); 
    }

    private async Task InitializeAsync()
    {
        await OnInitializeAsync();
        _initTCS.SetResult(true);
    }

    protected abstract Task OnInitializeAsync();
}

public abstract class AsyncSingletonPersistent<T> : AsyncSingleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        base.Awake(); // Calls the non-async Awake()
    }
}


// Pas besoin d'attendre une initilisation
public abstract class SingletonCreatorBootStrap<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}


