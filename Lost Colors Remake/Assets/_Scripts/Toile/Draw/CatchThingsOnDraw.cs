using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CatchThingsOnDraw : MonoBehaviour
{
    public List<GameObject> _ennemyObjectOnDraw;

    public void EnnemyOnPath(Ray ray, LayerMask layerMask)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000f, ~layerMask))
        {
            Debug.Log("oh tu grimpe dans le vaisseau en deux deux ?");

            if (hit.collider.gameObject.layer == 9 && !_ennemyObjectOnDraw.Contains(hit.collider.gameObject))
            {
                _ennemyObjectOnDraw.Add(hit.collider.gameObject);
            }
        }
    }

    public void IlEstTaMereLaPute()
    {
        foreach (GameObject obj in _ennemyObjectOnDraw)
        {
            if (obj.CompareTag("Torch"))
            {
                TorchInteract _torchInteract;

                obj.TryGetComponent(out _torchInteract);

                _torchInteract.Interact();
            }
        }
    }
}