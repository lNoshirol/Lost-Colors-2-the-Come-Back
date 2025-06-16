using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TorchGroup : MonoBehaviour
{
    public List<TorchInteract> _torchList = new List<TorchInteract>();
    public event Action Triggered;

    [Header("Debug")]
    public TextMeshProUGUI triggeredTorch;

    private void Start()
    {
        foreach(TorchInteract torch in _torchList)
        {
            torch.Trigger += CheckTriggeredTorch;
        }
    }

    public void CheckTriggeredTorch()
    {
        int count = 0;

        foreach(var torch in _torchList)
        {
            Debug.Log(count);

            if (!torch.IsTrigger)
            {
                return;
            }
            else if (torch.IsTrigger)
            {
                count++;
                triggeredTorch.text = count.ToString();
            }
        }

        TriggerEvent();
    }

    public void TriggerEvent()
    {
        Triggered?.Invoke();
    }
}
