using System.Collections;
using UnityEngine;

public class ToileMain : MonoBehaviour
{
    [SerializeField] private float timeAmount;
    [SerializeField] public float toileTime = 60;

    [SerializeField] public DrawForDollarP CastSpriteShape;

    public static ToileMain Instance { get; private set; }
    public ToileUI ToileUI { get; private set; }
    public TriggerToile TriggerToile { get; private set; }

    public RaycastDraw RaycastDraw { get; private set; }

    public bool gestureIsStarted = false;

    public Coroutine timerCo;

    public Animator toileAnimator;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


    }

    void Start()
    {
        ToileUI = GetComponent<ToileUI>();
        TriggerToile = GetComponent<TriggerToile>();
        RaycastDraw = GetComponent<RaycastDraw>();

        ToileUI.UpdateToileUI(timeAmount, toileTime);
        toileTime = (int)PlayerMain.Instance.toileInfo.toileTime;
    }

    public IEnumerator ToileTimer()
    {
        gestureIsStarted = true;
        timeAmount = toileTime;
        ToileUI.UpdateToileUI(timeAmount, toileTime);
        while (timeAmount > 0) {
            ToileUI.UpdateToileUI(timeAmount / toileTime, toileTime);
            yield return new WaitForSeconds(0.01f*PlayerMain.Instance.toileInfo.slowMotionScale);
            timeAmount-= 0.01f;
            //ToileUI.UpdateToileUI(timeAmount, toileTime);
        }
        gestureIsStarted = false;
        TriggerToile.OpenAndCloseToileMagique();
        yield break;
    }
}
