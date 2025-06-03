using UnityEngine;

// video link : https://www.youtube.com/watch?v=-AJ4J-lph6A
public abstract class SingletonCreator<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Duplicate singleton of type {typeof(T)} detected on {gameObject.name}, destroying.");
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
    }
}

public abstract class SingletonCreatorPersistent<T> : SingletonCreator<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        base.Awake(); 
    }
}
