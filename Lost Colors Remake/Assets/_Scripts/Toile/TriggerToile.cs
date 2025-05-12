using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class TriggerToile : MonoBehaviour
{
    public bool _isActive;
    [SerializeField] private GameObject toile;
    [SerializeField] private Button toileButton;
    
    private void Start()
    {
        _isActive = false;
        toileButton.interactable = false;
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            OpenAndCloseToileMagique();
        }
    }
    public void OpenAndCloseToileMagique()
    {
        ToileMain.Instance.CastSpriteShape.Resetpoint();
        if (_isActive == false)
        {
            ToileMain.Instance.ToileUI.UpdateToileUI(ToileMain.Instance.toileTime);
            _isActive = true;
            toile.SetActive(_isActive);
            PlayerMain.Instance.UI.HidePlayerControls();
            PlayerMain.Instance.Move.canMove = false;
            //PlayerMain.Instance.playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
            //StopCoroutine(ToileMain.Instance.timerCo);
            Time.timeScale = 0;

        }
        else
        {
            StartCoroutine(DeactivateAfterFrame());
        }
    }

    public void EnableToileButton()
    {
        toileButton.interactable = true;
    }

    IEnumerator DeactivateAfterFrame()
    {
        yield return null;
        _isActive = false;
        toile.SetActive(_isActive);
        PlayerMain.Instance.UI.HidePlayerControls();
        PlayerMain.Instance.Move.canMove = true;
        ToileMain.Instance.gestureIsStarted = false;
        //StopCoroutine(ToileMain.Instance.timerCo);
        Time.timeScale = 1;
    }
}
