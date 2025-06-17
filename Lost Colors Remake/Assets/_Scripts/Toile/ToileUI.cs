using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToileUI : MonoBehaviour
{
    //public Image TimeIndicator;
    //[SerializeField] public SpriteRenderer _toileUISprite;
    [SerializeField] TextMeshProUGUI _timerUGUI;
    [SerializeField] TextMeshProUGUI _paintAmountUGUI;
    [SerializeField] Slider _paintSlider;

    public void UpdateToileUI(float timerText, float maxTime)
    {
        //TimeIndicator.fillAmount = timerText;
    }

    public void UpdateToilePaintAmount()
    {
        //_paintAmountUGUI.text = "paintAmount : " + PlayerMain.Instance.Inventory.currentPaintAmont;
        _paintSlider.value = PlayerMain.Instance.Inventory.currentPaintAmont;
    }
}
