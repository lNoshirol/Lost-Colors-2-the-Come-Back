using System.Collections;
using UnityEngine;

public class ToileMain : MonoBehaviour
{
    [SerializeField] private int timeAmount;
    [SerializeField] public int toileTime = 60;

    [SerializeField] public CastSpriteShape CastSpriteShape;

    public static ToileMain Instance { get; private set; }
    public ToileUI ToileUI { get; private set; }
    public TriggerToile TriggerToile { get; private set; }

    public RaycastDraw RaycastDraw { get; private set; }

    public bool gestureIsStarted = false;

    public Coroutine timerCo;
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        ToileUI = GetComponent<ToileUI>();
        TriggerToile = GetComponent<TriggerToile>();
        RaycastDraw = GetComponent<RaycastDraw>();

        ToileUI.UpdateToileUI(timeAmount);
    }

    public IEnumerator ToileTimer()
    {
        gestureIsStarted = true;
        timeAmount = toileTime;
        while (timeAmount > 0) {
            ToileUI.UpdateToileUI(timeAmount);
            yield return new WaitForSeconds(1);
            timeAmount--;
            ToileUI.UpdateToileUI(timeAmount);
        }
        gestureIsStarted = false;
        TriggerToile.OpenAndCloseToileMagique();
        yield break;
        
        
    }

}
