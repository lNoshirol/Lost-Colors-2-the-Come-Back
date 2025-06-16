using System;
using System.Collections;
using System.Threading.Tasks;
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

    public async void OpenAndCloseToileMagique()
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
            var Sound = SoundsManager.Instance;
            int RandomSound = UnityEngine.Random.Range(0, Sound.OpeningCanva.Length);
            Sound.PlaySound(Sound.OpeningCanva[RandomSound], false);

            SoundsManager.Instance.SlowDownAllSound();
            ToileMain.Instance.toileAnimator.SetBool("ToileSpawn", true);

        }
        else
        {
            ToileMain.Instance.toileAnimator.SetBool("ToileSpawn", false);
            await Task.Delay(500);
            StartCoroutine(DeactivateAfterFrame());
            PlayerMain.Instance.Inventory.ResetCurrentPaintAmount();

            StopCoroutine(coroutine);
            SoundsManager.Instance.BackToNormal();
            var Sound = SoundsManager.Instance;
            Sound.PlaySound(Sound.ClosingCanva[0], false);
            time.StopSlowMotion();
            PlayerMain.Instance.UI.StartToileCooldownAsync(PlayerMain.Instance.toileInfo.cooldown);


        }
    }

    public void EnableToileButton()
    {
        if (toileButtonIn != null)
        {
            toileButtonIn.interactable = true;
        }
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

        ApplyDamageAfterDraw.Instance.TejArmor();
        ApplyDamageAfterDraw.Instance.ApplyDamage();

        ApplyDamageAfterDraw.Instance.TriggerVfxPlay();
    }


}
