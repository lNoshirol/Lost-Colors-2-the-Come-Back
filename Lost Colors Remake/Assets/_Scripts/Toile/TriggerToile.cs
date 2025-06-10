using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class TriggerToile : MonoBehaviour
{
    public static TriggerToile instance;

    public bool _isActive;
    [SerializeField] private GameObject toile;
    [SerializeField] private Button toileButtonIn;

    public event Action<bool> WhenTriggerToile;

    public TimeManager time;

    private Coroutine coroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _isActive = false;
        //toileButton.interactable = false;
    }

    //private void Update()
    //{
    //    if (false)
    //    {
    //        OpenAndCloseToileMagique();
    //    }
    //}

    public void OpenAndCloseToileMagique()
    {
        ToileMain.Instance.CastSpriteShape.Resetpoint();
        if (_isActive == false)
        {
            ToileMain.Instance.ToileUI.UpdateToileUI(ToileMain.Instance.toileTime);
            _isActive = true;
            toile.SetActive(_isActive);
            DrawForDollarP.instance.SetActiveLine(true);
            WhenTriggerToile?.Invoke(!_isActive);
            PlayerMain.Instance.UI.HidePlayerControls();
            PlayerMain.Instance.Move.canMove = false;
            PlayerMain.Instance.Rigidbody2D.linearVelocity = new Vector2(0,0);
            //StopCoroutine(ToileMain.Instance.timerCo);
            //Time.timeScale = Time.timeScale / 3;
            coroutine = StartCoroutine(time.DoSlowmotion(ToileMain.Instance.toileTime));
            PlayerMain.Instance.Inventory.ResetCurrentPaintAmount();

        }
        else
        {
            StartCoroutine(DeactivateAfterFrame());
            StopCoroutine(coroutine);
            time.InSlowMotion = false;
            PlayerMain.Instance.UI.StartToileCooldownAsync(PlayerMain.Instance.toileInfo.toileCooldown);

        }
    }

    public void EnableToileButton()
    {
        toileButtonIn.interactable = true;
    }

    IEnumerator DeactivateAfterFrame()
    {
        yield return null;
        _isActive = false;
        toile.SetActive(_isActive);
        WhenTriggerToile?.Invoke(!_isActive);
        PlayerMain.Instance.UI.HidePlayerControls();
        PlayerMain.Instance.Move.canMove = true;
        ToileMain.Instance.gestureIsStarted = false;
        //StopCoroutine(ToileMain.Instance.timerCo);
        Time.timeScale = 1;
        PlayerMain.Instance.Inventory.ResetCurrentPaintAmount();

        ApplyDamageAfterDraw.Instance.ApplyDamage();
        ApplyDamageAfterDraw.Instance.TejArmor();
    }


}
