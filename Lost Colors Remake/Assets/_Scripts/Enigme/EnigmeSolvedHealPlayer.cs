using UnityEngine;

public class EnigmeSolvedHealPlayer : EnigmeSolved
{
    [SerializeField] private int _healAmount;

    public override void Interact()
    {
        PlayerMain.Instance.Health.PlayerGainHP(_healAmount);
    }
}