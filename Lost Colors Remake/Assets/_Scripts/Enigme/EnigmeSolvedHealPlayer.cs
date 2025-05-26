using UnityEngine;

public class EnigmeSolvedHealPlayer : MonoBehaviour
{
    [SerializeField] private int _healAmount;

    private void Start()
    {
        Enigma _enigma;
        TryGetComponent(out _enigma);
        _enigma.OnEnigmaSolve += Interact;
    }

    public void Interact()
    {
        //PlayerMain.Instance.Health.PlayerHealthChange(-_healAmount);
    }
}
