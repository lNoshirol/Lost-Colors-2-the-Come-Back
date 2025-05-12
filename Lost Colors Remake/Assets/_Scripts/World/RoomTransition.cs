using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private float fadeSpeed;


    private void Start()
    {
        Fade(0);
    }
    public void Fade(int value)
    {
        blackScreen.DOFade(value, fadeSpeed).SetEase(Ease.InOutCubic);
    }
}
