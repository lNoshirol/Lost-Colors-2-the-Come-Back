using System.Collections;
using UnityEngine;

public class PlayerDashVFX : MonoBehaviour
{
    [SerializeField] private GameObject _spriteTrailPrefab;
    [SerializeField] private int _numOfIntermediateSprites;
    private Pool _spritesPool;

    void Start()
    {
        _spritesPool = new(_spriteTrailPrefab, _numOfIntermediateSprites, this.transform);
    }

    public IEnumerator BalanceSpriteTrail()
    {
        for (int i = 0; i < _numOfIntermediateSprites; i++)
        {
            GameObject sprite = _spritesPool.GetObject();
            sprite.transform.SetParent(null, false);
            sprite.transform.position = PlayerMain.Instance.transform.position;
            sprite.TryGetComponent(out SpriteRenderer renderer);
            renderer.flipX = PlayerMain.Instance.Move._moveInput.x < 0;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
