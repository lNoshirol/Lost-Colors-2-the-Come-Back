using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

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

        float areaCenterX = -(Screen.width * 6.35f / 2712);
        float areaCenterY = -(Screen.height * 1.7f / 1220);

        joystickMoveArea.center = new (areaCenterX, areaCenterY);
        clickArea.center = new (areaCenterX, areaCenterY);

        float areaSizeX = (Screen.width * 1.5f / 2712)*2;
        float areaSizeY = (Screen.height * 1 / 1220)*2;

        joystickMoveArea.size = new (areaSizeX, areaSizeY, 25);
        clickArea.size = new (areaSizeX * 2.82f, areaSizeY * 3.05f, 25);


        moveAreaBasePos = joystickMoveArea.center;
        clickAreaBasePos = clickArea.center;

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
                    Joystick.instance.stickTouch = Input.touches[i];
                    Joystick.instance.stickTouchFingerId = Input.touches[i].fingerId;
                }
                else if ((Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary) && Input.touches[i].fingerId == Joystick.instance.stickTouchFingerId)
                {
                    Joystick.instance.MoveStick(Input.touches[i]);
                }

                if (Input.touches[i].phase == TouchPhase.Ended && Input.touches[i].fingerId == Joystick.instance.stickTouchFingerId)
                {
                    Joystick.instance.stickTouchFingerId = -1;
                    Joystick.instance.Resetos();
                }

                if (Joystick.instance.stickTouchFingerId == -1)
                {
                    OnTouchEnd();
                    PlayerMain.Instance.Move.CancelMoveInput();
                    Joystick.instance.stickTouchFingerId = -1;
                }
            }
        }
        else
        {
            OnTouchEnd();
            PlayerMain.Instance.Move.CancelMoveInput();
            Joystick.instance.stickTouchFingerId = -1;
        }
    }

    public void OnTouchStart(Touch touch)
    {
        //Debug.Log("begin touch");

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);


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

    #endregion
}