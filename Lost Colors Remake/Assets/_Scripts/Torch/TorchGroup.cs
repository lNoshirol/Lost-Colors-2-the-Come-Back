using System.Collections.Generic;
using UnityEngine;

public class TorchGroup : MonoBehaviour
{
    public List<TorchInteract> _torchList = new List<TorchInteract>();

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
        //Debug.Log("Bili zaboul bili bili ?");
    }
}
