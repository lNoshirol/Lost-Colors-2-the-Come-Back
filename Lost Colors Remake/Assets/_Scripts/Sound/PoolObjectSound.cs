using UnityEngine;

public class PoolObjectSound : MonoBehaviour
{
    [SerializeField]
    private GameObject soundSource;

    private Pool _pool;

    private void Start()
    {
        _pool = new(soundSource, 20, this.transform);
    }

    public AudioSource GetAudioSourcePool()
    {
        _pool.GetObject().TryGetComponent(out AudioSource source);
        return source;
    }

    public Pool GetPoolSound()
    {
        return _pool;
    }
}
