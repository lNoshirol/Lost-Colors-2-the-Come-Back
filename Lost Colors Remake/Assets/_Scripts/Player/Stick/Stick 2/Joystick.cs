using UnityEngine;

public class Joystick : MonoBehaviour
{
    public static Joystick instance;

    public Vector3 normalizePos;
    public Vector3 normalPos;
    [SerializeField] Vector2 endPos;
    public Vector3 targetPos;
    public Vector3 targetPos2;


    public Transform Player;
    public float speed;

    public bool canMove;

    public Touch stickTouch;
    public int stickTouchFingerId;

    public JoystickAnime _joystickAnime;
    private StickAppearrAtClickLoc _stickAppearrAtClickLoc;

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
        _joystickAnime = GetComponent<JoystickAnime>();
        _stickAppearrAtClickLoc = StickAppearrAtClickLoc.Instance;
    }

    private void OnMouseUp()
    {
        StickDisappear.instance.StartCoroutineDisapear();
    }

    private void Update()
    {
        normalPos = transform.parent.position;
        normalizePos = transform.position - normalPos;
    }

    public void MoveStick(Touch touch)
    {


        if (Input.touchCount > 0 && canMove)
        {
            targetPos = Camera.main.ScreenToWorldPoint(touch.position) - normalPos;
            targetPos2 = Camera.main.ScreenToWorldPoint(touch.position);

            Debug.Log("trying to move");
            endPos = Camera.main.ScreenToWorldPoint(touch.position /*new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y)*/ /*Input.mousePosition*/);

            Vector2 offset = endPos - new Vector2(transform.parent.position.x, transform.parent.position.y);
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
            PlayerMain.Instance.Move.SetMoveInput(direction);
            transform.position = new Vector2(transform.parent.position.x + direction.x, transform.parent.position.y + direction.y);
        }
        else
        {
            Resetos();
        }
    }

    public void Resetos()
    {
        transform.position = transform.parent.position;

    }

    public void MovePlayer()
    {

        //PlayerMain.Instance.Move.SetMoveInput(_direction);
        _stickAppearrAtClickLoc.clickArea.center = Player.position + _stickAppearrAtClickLoc.clickAreaBasePos;
        _stickAppearrAtClickLoc.joystickMoveArea.center = Player.position + _stickAppearrAtClickLoc.moveAreaBasePos;

    }
}