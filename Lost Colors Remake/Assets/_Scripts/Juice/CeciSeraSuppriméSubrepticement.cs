using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CeciSeraSuppriméSubrepticement : MonoBehaviour
{
    [SerializeField] private List<TilemapRenderer> _tilemapRenderers = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("COLOR WAVE PROPAGATION ACTIVATED");
            Anim();
        }
    }

    public void Anim()
    {
        foreach (var renderer in _tilemapRenderers) 
        {
            renderer.material.DOFloat(40f, "_WaveProgress", 3f);
        }
        //_tilemapRenderer.material.DOFloat(40f, "_WaveProgress", 2.4f);

    }
}
