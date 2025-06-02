using System;
using UnityEngine;

public class PropsColoration : MonoBehaviour
{
    private float _timeBeforeTouch;

    void Start()
    {
        float distance = Vector3.Distance(CrystalManager.Instance._ListCrystal[0].Crystal.transform.position, this.transform.position);        
        
    }

    private async void OnCrystalColorize()
    {

    }
}
