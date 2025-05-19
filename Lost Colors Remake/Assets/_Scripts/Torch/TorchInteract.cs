using UnityEngine;
using System.Collections.Generic;
using System;

public class TorchInteract : MonoBehaviour
{
    [SerializeField] GameObject _torchFlame;
    public bool IsTrigger {  get; private set; }

    public event Action Trigger;

    public void Interact()
    {
        if (!IsTrigger)
        {
            IsTrigger = true;
            _torchFlame.SetActive(true);
            Trigger?.Invoke();
        }
    }
}
