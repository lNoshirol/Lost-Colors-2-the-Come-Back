using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystal;
    [SerializeField] private TileMapCorruptionWaveHandler _waveManager;

    public Sprite crystalUncorrupted;
    [SerializeField] private Volume _localDarkVolume;

    public override void Interact()
    {
        var Sound = SoundsManager.Instance;
        Sound.ChangeMusicSmoothly(Sound.MusicForestColored);
        CrystalColorLerp();
        _waveManager.Anim();
        ToileMain.Instance.TriggerToile.OpenAndCloseToileMagique();
    }

    private void CrystalColorLerp()
    {
        StartCoroutine(FadePPVolume());
        crystal.material.SetTexture("_ColoredTex", crystalUncorrupted.texture);
        crystal.material.DOFloat(1f, "_Transition", 2f);
        var Sound = SoundsManager.Instance;
        int RandomSound = Random.Range(0, Sound.Activate.Length);
        Sound.PlaySound(Sound.Activate[RandomSound], false);
    }

    private IEnumerator FadePPVolume()
    {
        for (float i = 1; i > 0; i-=0.01f) 
        {
            _localDarkVolume.weight = i;
            yield return new WaitForSeconds(0.01f);
        }
    }
}