using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Queue<GameObject> ObjectStock { get; private set; } = new();

    public Pool(GameObject item, int amount, Transform parent)
    {
        for (int i = 0; i < amount; i++)
        {
            Stock(GameObject.Instantiate(item, parent));
        }
        Debug.Log($"[Pool] Successfully instantiated {amount} {item}.");
    }

    /// <summary>
    /// Get an item from the pool and activate it.
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        GameObject item = ObjectStock.Dequeue();
        item.gameObject.SetActive(true);
        return item;
    }

    /// <summary>
    /// Stock an item in the pool and deactivate it.
    /// </summary>
    /// <param name="item"></param>
    public void Stock(GameObject item)
    {
        item.gameObject.SetActive(false);
        ObjectStock.Enqueue(item);
    }
}
