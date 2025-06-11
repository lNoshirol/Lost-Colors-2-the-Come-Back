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
                if (obj.TryGetComponent(out EnemyHealth _health))
                {
                    ApplyDamageAfterDraw.Instance.AddEnnemyDamage(_health, PlayerMain.Instance.toileInfo.lineDamage);
                    
                    //ancienne ligne au cas où prb
                    //_health.EnemyLoseHP(PlayerMain.Instance.toileInfo.lineDamage);
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