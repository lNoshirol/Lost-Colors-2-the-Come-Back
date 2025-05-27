using System.Collections.Generic;
using UnityEngine;
using System;

public class TorchGroup : MonoBehaviour
{
    public List<TorchInteract> _torchList = new List<TorchInteract>();
    public event Action Triggered;

    private void Start()
    {
        foreach(TorchInteract torch in _torchList)
        {
            torch.Trigger += CheckTriggeredTorch;
        }
    }

    public void CheckTriggeredTorch()
    {
        foreach(var torch in _torchList)
        {
            if (!torch.IsTrigger)
            {
                return;
            }
        }

        TriggerEvent();
    }

    public void TriggerEvent()
    {
        Triggered?.Invoke();
    }
}
