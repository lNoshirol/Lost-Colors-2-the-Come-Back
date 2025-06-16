using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToileUI : MonoBehaviour
{
    [SerializeField] private Image _timeIndicator;
    [SerializeField] TextMeshProUGUI _timerUGUI;
    [SerializeField] TextMeshProUGUI _paintAmountUGUI;
    [SerializeField] Slider _paintSlider;

    public void UpdateToileUI(int timerText, int maxTime)
    {
        //_timerUGUI.text = "timeAmountLeft : " + timerText;
        //_timeIndicator.fillAmount = timerText / maxTime;
        print(maxTime / timerText);
    }

    public void UpdateToilePaintAmount()
    {
        //_paintAmountUGUI.text = "paintAmount : " + PlayerMain.Instance.Inventory.currentPaintAmont;
        _paintSlider.value = PlayerMain.Instance.Inventory.currentPaintAmont;
    }
}
