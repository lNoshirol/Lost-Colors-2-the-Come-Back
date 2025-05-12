using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private CanvasGroup playerControl;
    [SerializeField] private float fadeSpeed;
    public void UpdatePlayerHealthUI()
    {
        healthbarSprite.fillAmount = (float)PlayerMain.Instance.Health.playerActualHealth / PlayerMain.Instance.Health.playerBaseHealth;
    }
    private void Start()
    {
        Fade(0, blackScreen);
    }
    public void Fade(int value, CanvasGroup group)
    {
        group.DOFade(value, fadeSpeed).SetEase(Ease.InOutCubic);
    }


    public void SwitchRoomUI()
    {
        FadePlayerInput();
        FadeBlackScreen();
    }

    public void FadePlayerInput()
    {
        if (playerControl.alpha == 1)
        {
            Fade(0, playerControl);
        }
        else
        {
            Fade(1, playerControl);
        }
    }

    public void FadeBlackScreen()
    {
        if (blackScreen.alpha == 0)
        {
            Fade(1, blackScreen);
        }
        else
        {
            Fade(0, blackScreen);
        }
    }

    public void HidePlayerControls()
    {
        if(playerControl.gameObject.activeSelf)
        {
            playerControl.gameObject.SetActive(false);  
        }
        else {
            playerControl.gameObject.SetActive(true);

        }

    }

}
