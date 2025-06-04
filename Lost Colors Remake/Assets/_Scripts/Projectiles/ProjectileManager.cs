using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ProjectileManager : AsyncSingletonPersistent<ProjectileManager>
{
    public List<ObjectAmount> ProjectileList = new();

    public Dictionary<string, Pool> ProjectilePools = new();

    public Dictionary<string, GenericPool<Projectile>> V2 = new();
    
    protected override async Task OnInitializeAsync()
    {
        await Bootstrap.Instance.WaitUntilInitializedAsync();
        ProjectilePoolCreate();
    }


    void ProjectilePoolCreate()
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
