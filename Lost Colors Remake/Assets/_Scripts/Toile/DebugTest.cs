using UnityEngine;

public class DebugTest : MonoBehaviour
{
    public GameObject test;

    // Update is called once per frame
    void Update()
    {
        test.transform.LookAt(Camera.main.transform.position);
    }
}
