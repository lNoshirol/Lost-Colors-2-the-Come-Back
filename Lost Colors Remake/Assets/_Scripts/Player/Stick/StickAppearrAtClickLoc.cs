using UnityEngine;

public class StickAppearrAtClickLoc : MonoBehaviour
{
    public static StickAppearrAtClickLoc Instance { get; private set; }

    [SerializeField] GameObject _stick;
    public Bounds joystickMoveArea;
    public Bounds clickArea;

    public Vector3 moveAreaBasePos;
    public Vector3 clickAreaBasePos;

    private Vector2 lastTouchPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ClickManager.instance.OnClickStart += OnTouchStart;
        ClickManager.instance.OnClickEnd += OnTouchEnd;

        TriggerToile.instance.WhenTriggerToile += forceSetActive;

        moveAreaBasePos = joystickMoveArea.center;
        clickAreaBasePos = clickArea.center;
    }

    public void OnTouchStart()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lastTouchPos = touchPosition;
        
        if (!clickArea.Contains(touchPosition))
        {
            Joystick.instance.canMove = false;
            return;
        }

        Joystick.instance._joystickAnime.BoolSwitcher("IsHere", true);

        Joystick.instance.canMove = true;

        _stick.SetActive(true);

        if (joystickMoveArea.Contains(touchPosition))
        {
            _stick.transform.position = touchPosition;
            Debug.Log("Position Changed inside bounds");

        }
        else
        {
            touchPosition = joystickMoveArea.ClosestPoint(touchPosition);
            _stick.transform.position = touchPosition;
            Debug.Log("Position Changed snap to closest point");
        }
    }

    public void OnTouchEnd()
    {
        Joystick.instance._joystickAnime.BoolSwitcher("IsHere", false);
    }

    private void forceSetActive(bool active)
    {
        _stick.SetActive(active);
        OnTouchEnd();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(joystickMoveArea.center, joystickMoveArea.size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(clickArea.center, clickArea.size);
    }
}
