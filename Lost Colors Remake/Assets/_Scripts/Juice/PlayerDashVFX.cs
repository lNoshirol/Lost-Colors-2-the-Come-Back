using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerDashVFX : MonoBehaviour
{
    [SerializeField] private Sprite _fixedDashSprite;
    [SerializeField] private GameObject _spriteTrailPrefab;
    [SerializeField] private int _numOfIntermediateSprites;
    [SerializeField] private TrailRenderer _trail;
    private Pool _spritesPool;

    void Start()
    {
        _trail.emitting = false;
        _spritesPool = new(_spriteTrailPrefab, 25, this.transform);
    }

    public void LaunchFeedback()
    {
        StartCoroutine(BalanceSpriteTrail());
    }

    public IEnumerator BalanceSpriteTrail()
    {
        PlayerMain.Instance.anim.enabled = false;
        _trail.emitting = true;
        List<GameObject> usedSprites = new();
        for (int i = 0; i < _numOfIntermediateSprites; i++)
        {
            GameObject sprite = _spritesPool.GetObject();
            sprite.transform.SetParent(null);
            sprite.transform.rotation = Quaternion.identity;
            sprite.transform.position = PlayerMain.Instance.transform.position + Vector3.up * 0.4f;
            sprite.TryGetComponent(out SpriteRenderer renderer);
            renderer.flipX = PlayerMain.Instance.Move._moveInput.x < 0;
            renderer.DOFade(0, 0.5f);
            usedSprites.Add(sprite);
            yield return new WaitForSeconds(0.1f);
        }
        _trail.emitting = false;
        PlayerMain.Instance.anim.enabled = true;

        yield return new WaitForSeconds(0.4f);

        foreach (GameObject sprite in usedSprites)
        {
            _spritesPool.Stock(sprite);
            sprite.TryGetComponent(out SpriteRenderer renderer);
            renderer.DOFade(1, 0f);
            sprite.transform.SetParent(this.transform);
        }
    }
}
