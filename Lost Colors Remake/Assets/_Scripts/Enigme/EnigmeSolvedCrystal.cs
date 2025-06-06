using System;
using DG.Tweening;
using UnityEngine;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystal;
    [SerializeField] private TileMapCorruptionWaveHandler _waveManager;
    
    public Sprite crystalUncorrupted;

    public event Action ISColorized;

    public override void Interact()
    {
        CrystalColorLerp();
        _waveManager.Anim();
        ISColorized?.Invoke();
    }

    private void CrystalColorLerp()
    {
        crystal.material.SetTexture("_ColoredTex", crystalUncorrupted.texture);
        crystal.material.DOFloat(1f, "_Transition", 2f);
    }
}