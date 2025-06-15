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

    [Header("Global Volume")]
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private VolumeProfile _BWVolumeProfile;
    [SerializeField] private VolumeProfile _ColoredVolumeProfile;

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
        StartCoroutine(FadeLocalVolume());
        StartCoroutine(SwitchGlobalVolume());
        crystal.material.SetTexture("_ColoredTex", crystalUncorrupted.texture);
        crystal.material.DOFloat(1f, "_Transition", 2f);
        var Sound = SoundsManager.Instance;
        int RandomSound = Random.Range(0, Sound.Activate.Length);
        Sound.PlaySound(Sound.Activate[RandomSound], false);
    }

    private IEnumerator FadeLocalVolume()
    {
        for (float i = 1; i > 0; i-=0.01f) 
        {
            _localDarkVolume.weight = i;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator SwitchGlobalVolume()
    {
        for (float i = 1; i > 0; i-=0.01f)
        {
            _globalVolume.weight = i;
            yield return new WaitForSeconds(0.01f);
        }
        _globalVolume.profile = _ColoredVolumeProfile;
        for (float j = 0; j < 1; j += 0.01f)
        {
            _globalVolume.weight = j;
            yield return new WaitForSeconds(0.01f);
        }
    }
}