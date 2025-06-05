using System.Collections;
using UnityEngine;

public class ToileMain : MonoBehaviour
{
    [SerializeField] private int timeAmount;
    [SerializeField] public int toileTime = 60;

    [SerializeField] public DrawForDollarP CastSpriteShape;

    public static ToileMain Instance { get; private set; }
    public ToileUI ToileUI { get; private set; }
    public TriggerToile TriggerToile { get; private set; }

    public RaycastDraw RaycastDraw { get; private set; }

    public bool gestureIsStarted = false;

    public Coroutine timerCo;


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

        ToileUI.UpdateToileUI(timeAmount);
        toileTime = (int)PlayerMain.Instance.toileInfo.toileTime;
    }

    public IEnumerator ToileTimer()
    {
        gestureIsStarted = true;
        timeAmount = toileTime;
        while (timeAmount > 0) {
            ToileUI.UpdateToileUI(timeAmount);
            yield return new WaitForSeconds(1*PlayerMain.Instance.toileInfo.slowMotionScale);
            timeAmount--;
            ToileUI.UpdateToileUI(timeAmount);
        }
        gestureIsStarted = false;
        TriggerToile.OpenAndCloseToileMagique();
        yield break;
    }
}
