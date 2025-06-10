using DG.Tweening;
using UnityEngine;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystal;
    [SerializeField] private TileMapCorruptionWaveHandler _waveManager;

    [SerializeField]
    private Crystal Crystal;
    
    public Sprite crystalUncorrupted;

    public override void Interact()
    {
        CrystalColorLerp();
        _waveManager.Anim();
        ToileMain.Instance.TriggerToile.OpenAndCloseToileMagique();
    }

    private void CrystalColorLerp()
    {
        crystal.material.SetTexture("_ColoredTex", crystalUncorrupted.texture);
        crystal.material.DOFloat(1f, "_Transition", 2f);
        Crystal.CrystalSave();
    }
}