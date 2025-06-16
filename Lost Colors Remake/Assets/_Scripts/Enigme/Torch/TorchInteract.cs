using UnityEngine;
using System;
using DG.Tweening;

public class TorchInteract : MonoBehaviour
{
    [SerializeField] GameObject _torchFlame;
    public bool IsTrigger {  get; private set; }

    public event Action Trigger;

    public void Interact()
    {
        Debug.Log($"Interact {gameObject.name} {IsTrigger}");

        if (!IsTrigger)
        {
            IsTrigger = true;
            TryGetComponent(out SpriteRenderer spriteRenderer);
            spriteRenderer.material.SetTexture("_ColoredTex", PropsSpriteHandler.Instance.PropsColoredTextures[spriteRenderer.sprite.ToString().Replace("_BW (UnityEngine.Sprite)", "")].texture);
            spriteRenderer.material.DOFloat(1f, "_Transition", 2f);
            //_torchFlame.SetActive(true);
            Trigger?.Invoke();
        }
    }
}
