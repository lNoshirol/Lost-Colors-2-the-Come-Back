using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CatchEnnemyOnDraw : MonoBehaviour
{
    public List<GameObject> _ennemyObjectOnDraw;

    public void EnnemyOnPath(Ray ray, LayerMask layerMask)
    {
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 200f, ~layerMask))
        {

            if (hit.collider.gameObject.layer == 8 && _ennemyObjectOnDraw.Contains(hit.collider.gameObject))
            {
                _ennemyObjectOnDraw.Add(hit.collider.gameObject);
            }
        }
    }
}