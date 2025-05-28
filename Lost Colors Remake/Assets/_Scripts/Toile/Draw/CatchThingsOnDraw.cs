using UnityEngine;
using System.Collections.Generic;

public class CatchThingsOnDraw : MonoBehaviour
{
    public List<GameObject> _ennemyObjectOnDraw;

    public void EnnemyOnPath(Ray2D ray, LayerMask layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 9 && !_ennemyObjectOnDraw.Contains(hit.collider.gameObject))
            {
                _ennemyObjectOnDraw.Add(hit.collider.gameObject);
            }
        }
    }

    public void CatchObjectOnLine()
    {
        foreach (GameObject obj in _ennemyObjectOnDraw)
        {
            if (obj.CompareTag("Torch"))
            {
                obj.TryGetComponent(out TorchInteract _torchInteract);

                _torchInteract.Interact();
            }
            else if (obj.CompareTag("Enemy"))
            {
                Debug.Log("ouillle, ta mere la tepu; j'ai mal");
                if (obj.TryGetComponent(out EnemyHealth _health))
                {
                    Debug.Log("AIE AIE AIE AEI AIE AIE AIE AIE AEI");
                    _health.EnemyLoseHP(0.5f);
                }
                
            }
        }
        ResetList();
    }

    public void ResetList()
    {
        _ennemyObjectOnDraw.Clear();
    }
}