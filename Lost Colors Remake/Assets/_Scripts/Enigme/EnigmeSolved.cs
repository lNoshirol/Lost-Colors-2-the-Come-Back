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

        Debug.Log("ça marche ?");
    }

    public abstract void Interact();
}