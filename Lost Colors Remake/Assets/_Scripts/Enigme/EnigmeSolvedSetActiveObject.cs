using UnityEngine;

public class EnigmeSolvedSetActiveObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDepop;
    [SerializeField] private bool _setActiveBool;

    private void Start()
    {
        Enigma _enigma;
        TryGetComponent(out _enigma);
        _enigma.OnEnigmaSolve += Interact;
    }

    public void Interact()
    {
        _objectToDepop.SetActive(_setActiveBool);
    }
}
