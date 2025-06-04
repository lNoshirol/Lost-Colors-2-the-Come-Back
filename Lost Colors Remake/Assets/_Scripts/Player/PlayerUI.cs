using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private CanvasGroup playerControl;
    [SerializeField] private Button dashButton;
    [SerializeField] private Button toileButton;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private List<Image> heartList = new List<Image>();


    public void UpdatePlayerHealthUI()
    {
        float currentHealth = PlayerMain.Instance.Health.playerActualHealth;

        for (int i = 0; i < heartList.Count; i++)
        {
            float heartHealth = currentHealth - i;

            if (heartHealth >= 1f)
            {
                heartList[i].sprite = fullHeart;
            }
            else if (heartHealth >= 0.5f)
            {
                heartList[i].sprite = halfHeart;
            }
            else
            {
                heartList[i].sprite = emptyHeart;
            }
        }
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
        if (playerControl.gameObject.activeSelf)
        {
            playerControl.gameObject.SetActive(false);
        }
        else
        {
            playerControl.gameObject.SetActive(true);

        }
    }

    public void DashButton(bool enable)
    {
        if (enable)
            dashButton.interactable = true;
        else
            dashButton.interactable = false;

    }

    public void ToileButton(bool enable)
    {
        if (enable)
            toileButton.interactable = true;
        else
            toileButton.interactable = false;

    }
}
