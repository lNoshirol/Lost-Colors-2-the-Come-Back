using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPoolManager : MonoBehaviour
{
    [SerializeField] private ObjectAmount _paintBurstVFXPoolInitializer;
    private Pool _paintBurstVFXPool;
    
    // Singleton
    #region Singleton
    private static VFXPoolManager _instance;

    public static VFXPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Projectile Manager");
                _instance = go.AddComponent<VFXPoolManager>();
                Debug.Log("<color=#8b59f0>VFXPool Manager</color> instance <color=#58ed7d>created</color>");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("<color=#8b59f0>VFXPool Manager</color> instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    void Start()
    {
        _paintBurstVFXPool = new(_paintBurstVFXPoolInitializer.ObjectPrefab, _paintBurstVFXPoolInitializer.Amount, this.transform);
    }

    public async void PlayPaintVFXAt(Vector3 pos)
    {
        GameObject obj = _paintBurstVFXPool.GetObject();
        obj.TryGetComponent(out VisualEffect vfx);
        obj.transform.position = pos;
        vfx.Play();
        await Task.Delay(1000);
        _paintBurstVFXPool.Stock(obj);
        print(_paintBurstVFXPool.ObjectStock.Count);
    }
}
