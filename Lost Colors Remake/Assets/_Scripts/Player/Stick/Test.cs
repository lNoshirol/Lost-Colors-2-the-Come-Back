using UnityEngine;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public List<GameObject> gameObjects = new();

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                gameObjects[i].SetActive(true);
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                //Debug.Log(Input.touches[i].fingerId);
                pos.z = 0;
                gameObjects[i].transform.position = pos;
            }
        }
    }
}
