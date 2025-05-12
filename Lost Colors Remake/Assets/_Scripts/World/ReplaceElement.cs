using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class ReplaceElement : MonoBehaviour
{
    public List<GameObject> ListObject = new() ;
    public GameObject prefab;
    public GameObject parent;

    [Button("Replace")]
    public void Replace()
    {
        foreach(GameObject obj in ListObject)
        {
            obj.SetActive(false);
            GameObject newTree = Instantiate(prefab);
            newTree.transform.position = obj.transform.position;
            newTree.transform.parent = parent.transform;
        }
    }
}
