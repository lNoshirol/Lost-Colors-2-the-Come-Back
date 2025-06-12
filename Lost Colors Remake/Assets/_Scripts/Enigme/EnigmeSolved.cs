using UnityEngine;

public abstract class EnigmeSolved : MonoBehaviour
{
    private void Start()
    {
        if (TryGetComponent(out Enigma _enigma))
        {
            _enigma.OnEnigmaSolve += Interact;
        }
        else if (TryGetComponent(out TorchGroup _torchGroup))
        {
            _torchGroup.Triggered += Interact;
        }
    }

    public abstract void Interact();
}