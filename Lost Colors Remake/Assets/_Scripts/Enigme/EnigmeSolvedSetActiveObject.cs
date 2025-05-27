using UnityEngine;

public class EnigmeSolvedSetActiveObject : EnigmeSolved
{
    [SerializeField] private GameObject _objectToDepop;
    [SerializeField] private bool _setActiveBool;

    public override void Interact()
    {
        _objectToDepop.SetActive(_setActiveBool);
    }
}
