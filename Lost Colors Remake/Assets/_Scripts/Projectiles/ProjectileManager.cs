using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<ObjectAmount> ProjectileList = new();

    public Dictionary<string, Pool> ProjectilePools = new();

    public Dictionary<string, GenericPool<Projectile>> V2 = new();
    
    // Singleton
    #region Singleton
    private static ProjectileManager _instance;

    public static ProjectileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Projectile Manager");
                _instance = go.AddComponent<ProjectileManager>();
                Debug.Log("<color=#8b59f0>Projectile Manager</color> instance <color=#58ed7d>created</color>");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("<color=#8b59f0>Projectile Manager</color> instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        foreach (ObjectAmount duo in ProjectileList)
        {
            GameObject parent = new("[Pool Parent]" + duo.ObjectPrefab.name);
            parent.transform.parent = this.transform;

            Pool newPool = new(duo.ObjectPrefab, duo.Amount, parent.transform);
            ProjectilePools.Add(duo.ObjectPrefab.name, newPool);
        }
    }
}
