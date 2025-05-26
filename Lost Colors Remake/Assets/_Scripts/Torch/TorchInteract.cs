using UnityEngine;
using System;

public class TorchInteract : MonoBehaviour
{
    [SerializeField] GameObject _torchFlame;
    public bool IsTrigger {  get; private set; }

    public event Action Trigger;

    public void Interact()
    {
        Debug.Log($"Interact {gameObject.name} {IsTrigger}");

        if (!IsTrigger)
        {
            IsTrigger = true;
            _torchFlame.SetActive(true);
            Trigger?.Invoke();
        }
    }
}
