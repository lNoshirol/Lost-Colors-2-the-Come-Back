using UnityEngine;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystal;
    [SerializeField] private TileMapCorruptionWaveHandler _waveManager;
    
    public Sprite crystalUncorrupted;

    public override void Interact()
    {
        crystal.sprite = crystalUncorrupted;

        _waveManager.Anim();
    }
}