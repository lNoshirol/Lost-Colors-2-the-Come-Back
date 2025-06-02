using UnityEngine;

public class StickAppear2 : MonoBehaviour
{
    public static StickAppear2 Instance { get; private set; }

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        moveAreaBasePos = joystickMoveArea.center;
        clickAreaBasePos = clickArea.center;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            
        }
    }

    public void OnTouchStart(Touch touch)
    {
        //Debug.Log("begin touch");

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        lastTouchPos = touchPosition;

        if (!clickArea.Contains(touchPosition))
        {
            JoystickLucas.instance.canMove = false;
            return;
        }

        JoystickLucas.instance._joystickAnime.BoolSwitcher("IsHere", true);

        JoystickLucas.instance.canMove = true;

        _stick.SetActive(true);

        if (joystickMoveArea.Contains(touchPosition))
        {
            _stick.transform.position = touchPosition;
            //Debug.Log("Position Changed inside bounds");

        }
        else
        {
            touchPosition = joystickMoveArea.ClosestPoint(touchPosition);
            _stick.transform.position = touchPosition;
            //Debug.Log("Position Changed snap to closest point");
        }
    }

    public void OnTouchEnd()
    {
        JoystickLucas.instance._joystickAnime.BoolSwitcher("IsHere", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(joystickMoveArea.center, joystickMoveArea.size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(clickArea.center, clickArea.size);
    }
}
