using UnityEngine;

public abstract class SingletonCreator<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance {  get; private set; }

    protected virtual void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.LogWarning("Trying to create a duplicate singleton instance");
            Destroy(gameObject);
        }
        else
        {
            Instance = this as T;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);    
    }


}
public abstract class SingletonCreatorPersistant<TDerived> : SingletonCreator<TDerived> where TDerived : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}
