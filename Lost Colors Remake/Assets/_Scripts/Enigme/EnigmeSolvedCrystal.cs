using UnityEngine;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystal;
    [SerializeField] private CeciSeraSupprim�Subrepticement _waveManager;
    
    public Sprite crystalUncorrupted;

    public override void Interact()
    {
        crystal.sprite = crystalUncorrupted;

        _waveManager.Anim();
    }
}