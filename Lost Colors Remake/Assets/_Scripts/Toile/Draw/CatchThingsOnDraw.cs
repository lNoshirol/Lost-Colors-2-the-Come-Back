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
            Debug.Log("ta grand mere la pute");

            if (hit.collider.gameObject.layer == 9 && !_ennemyObjectOnDraw.Contains(hit.collider.gameObject))
            {
                Debug.Log("Hit ennemy with line");
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
                Debug.Log(_torchInteract.IsTrigger);
            }
        }
    }

    public void resetList()
    {
        _ennemyObjectOnDraw.Clear();
    }
}