using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToileUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _timerUGUI;
    [SerializeField] TextMeshProUGUI _paintAmountUGUI;
    [SerializeField] Slider _paintSlider;

    [SerializeField] private Image _scrollLeft;
    [SerializeField] private Image _scrollRight;
    [SerializeField] private Image _bgToile;

    public async void ToileAnim()
    {
        //_scrollLeft.transform.DOMoveY();
    }

    public void UpdateToileUI(int timerText)
    {
        _timerUGUI.text = "timeAmountLeft : " + timerText;
    }

    public void UpdateToilePaintAmount()
    {
        //_paintAmountUGUI.text = "paintAmount : " + PlayerMain.Instance.Inventory.currentPaintAmont;
        _paintSlider.value = PlayerMain.Instance.Inventory.currentPaintAmont;
    }
}
