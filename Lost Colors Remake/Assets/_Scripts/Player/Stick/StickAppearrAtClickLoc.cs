using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class StickAppearrAtClickLoc : MonoBehaviour
{
    public static StickAppearrAtClickLoc Instance { get; private set; }

    [Header("Stick Box Parameter")]
    [Range(0, 100), OnValueChanged("ChangeCenterPosWidth")]
    public int percentageCenterPos;

    [Header("Box and rect")]
    [SerializeField] GameObject _stick;

    public Vector3 moveAreaBasePos;
    public Vector3 clickAreaBasePos;

    public RectTransform moveAreaRect;

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

    }

    private void Update()
    {
        Debug.Log($"Rect {moveAreaRect.rect} {moveAreaRect.rect.size} {moveAreaRect.rect.center}");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
        }

        if (Input.touchCount > 0)
        {
            //Debug.Log(Input.GetTouch(0).phase);

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.touches[i].phase == TouchPhase.Began && JoystickLucas.instance.stickTouchFingerId == -1 && RectTransformUtility.RectangleContainsScreenPoint(moveAreaRect, Input.touches[i].position))
                {
                    OnTouchStart(Input.touches[i]);
                    JoystickLucas.instance.stickTouch = Input.touches[i];
                    JoystickLucas.instance.stickTouchFingerId = Input.touches[i].fingerId;
                }
                else if ((Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary) && Input.touches[i].fingerId == JoystickLucas.instance.stickTouchFingerId)
                {
                    JoystickLucas.instance.MoveStick(Input.touches[i]);
                }

                if (Input.touches[i].phase == TouchPhase.Ended && Input.touches[i].fingerId == JoystickLucas.instance.stickTouchFingerId)
                {
                    JoystickLucas.instance.stickTouchFingerId = -1;
                    JoystickLucas.instance.Resetos();
                }

                if (JoystickLucas.instance.stickTouchFingerId == -1)
                {
                    OnTouchEnd();
                    PlayerMain.Instance.Move.CancelMoveInput();
                    JoystickLucas.instance.stickTouchFingerId = -1;
                }

                #region Old Version

                /*if (Input.touches[i].phase == TouchPhase.Began && JoystickLucas.instance.stickTouchFingerId == -1 && clickArea.Contains(Camera.main.ScreenToWorldPoint(Input.touches[i].position)))
                {
                    OnTouchStart(Input.touches[i]);
                    JoystickLucas.instance.stickTouch = Input.touches[i];
                    JoystickLucas.instance.stickTouchFingerId = Input.touches[i].fingerId;
                }
                else if ((Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary) && Input.touches[i].fingerId == JoystickLucas.instance.stickTouchFingerId)
                {
                    JoystickLucas.instance.MoveStick(Input.touches[i]);
                }

                if (Input.touches[i].phase == TouchPhase.Ended && Input.touches[i].fingerId == JoystickLucas.instance.stickTouchFingerId)
                {
                    JoystickLucas.instance.stickTouchFingerId = -1;
                    JoystickLucas.instance.Resetos();
                }

                if (JoystickLucas.instance.stickTouchFingerId == -1)
                {
                    OnTouchEnd();
                    PlayerMain.Instance.Move.CancelMoveInput();
                    JoystickLucas.instance.stickTouchFingerId = -1;
                }*/

                #endregion
            }
        }
        else
        {
            OnTouchEnd();
            PlayerMain.Instance.Move.CancelMoveInput();
            JoystickLucas.instance.stickTouchFingerId = -1;
        }
    }

    public void OnTouchStart(Touch touch)
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);


        if (!RectTransformUtility.RectangleContainsScreenPoint(moveAreaRect, touch.position))
        {
            JoystickLucas.instance.canMove = false;
            return;
        }

        JoystickLucas.instance._joystickAnime.BoolSwitcher("IsHere", true);

        JoystickLucas.instance.canMove = true;

        _stick.SetActive(true);

        if (RectTransformUtility.RectangleContainsScreenPoint(moveAreaRect, touch.position))
        {
            _stick.transform.position = Camera.main.ScreenToViewportPoint(touchPosition);
            //Debug.Log("Position Changed inside bounds");

        }
    }

    public void OnTouchEnd()
    {
        JoystickLucas.instance._joystickAnime.BoolSwitcher("IsHere", false);
    }

    private void forceSetActive(bool active)
    {
        _stick.SetActive(active);
        OnTouchEnd();
    }
}