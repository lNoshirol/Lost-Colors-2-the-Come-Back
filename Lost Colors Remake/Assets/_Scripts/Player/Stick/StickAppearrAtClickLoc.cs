using NaughtyAttributes;
using Unity.Android.Gradle;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public class StickAppearrAtClickLoc : MonoBehaviour
{
    public static StickAppearrAtClickLoc Instance { get; private set; }

    [Header("Stick Box Parameter")]
    [Range(0, 100), OnValueChanged("ChangeCenterPosWidth")]
    public int percentageCenterPos;

    [Header("Box")]
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
        /*ClickManager.instance.OnClickStart += OnTouchStart;
        ClickManager.instance.OnClickEnd += OnTouchEnd;

        TriggerToile.instance.WhenTriggerToile += forceSetActive;*/

        ChangeCenterPosWidth();
        
        
        moveAreaBasePos = joystickMoveArea.center;
        clickAreaBasePos = clickArea.center;

        Debug.Log(Screen.width);
        Debug.Log(Screen.height);

    }

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            //Debug.Log(Input.GetTouch(0).phase);

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.touches[i].phase == TouchPhase.Began && Joystick.instance.stickTouchFingerId == -1 && clickArea.Contains(Camera.main.ScreenToWorldPoint(Input.touches[i].position)))
                {
                    OnTouchStart(Input.touches[i]);
                    Debug.Log("tié vraiment une chienne mamie");
                    Joystick.instance.stickTouch = Input.touches[i];
                    Joystick.instance.stickTouchFingerId = Input.touches[i].fingerId;
                }
                else if ((Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary) && Input.touches[i].fingerId == Joystick.instance.stickTouchFingerId)
                {
                    Joystick.instance.MoveStick(Input.touches[i]);
                    Debug.Log("BOUGE");
                }

                if (Input.touches[i].phase == TouchPhase.Ended && Input.touches[i].fingerId == Joystick.instance.stickTouchFingerId)
                {
                    Joystick.instance.stickTouchFingerId = -1;
                    Joystick.instance.Resetos();
                    Debug.Log("suce");
                }

                if (Joystick.instance.stickTouchFingerId == -1)
                {
                    OnTouchEnd();
                    PlayerMain.Instance.Move.CancelMoveInput();
                    Joystick.instance.stickTouchFingerId = -1;
                    Debug.Log("OH CASSEU TOII SALE PUTE");
                }
            }
        }
        else
        {
            OnTouchEnd();
            PlayerMain.Instance.Move.CancelMoveInput();
            Joystick.instance.stickTouchFingerId = -1;
            Debug.Log("OH CASSEU TOII");
        }
    }

    public void OnTouchStart(Touch touch)
    {
        //Debug.Log("begin touch");

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

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

    #region C'est pas propre ? M'en branle ça sert due dans l'editeur

    public void ChangeCenterPosWidth()
    {
        
        Debug.Log($"Width {Screen.width} Height {Screen.height}");
        Vector3 centerPosX = Camera.main.ScreenToWorldPoint( new (percentageCenterPos * Screen.width / 100, 0, 0));
        joystickMoveArea.center = new(centerPosX.x, joystickMoveArea.center.y, joystickMoveArea.center.z);
    }

    #endregion
}