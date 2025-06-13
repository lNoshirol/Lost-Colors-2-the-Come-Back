using UnityEngine;

public class JoystickLucas : MonoBehaviour
{
    public static JoystickLucas instance;

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
            targetPos = (Vector3)touch.position - normalPos;
            targetPos2 = touch.position;

            //Debug.Log("trying to move");
            endPos = touch.position;

            Vector2 offset = endPos - new Vector2(transform.parent.position.x, transform.parent.position.y);
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
            PlayerMain.Instance.Move.SetMoveInput(direction);

            Vector2 clampedOffset = Vector2.ClampMagnitude(offset, 150);
            transform.position = new Vector2(transform.parent.position.x + clampedOffset.x, transform.parent.position.y + clampedOffset.y);

/*            float x = Mathf.Clamp(touch.position.x, transform.parent.position.x -35, transform.parent.position.x + 35);
            float y = Mathf.Clamp(touch.position.y, transform.parent.position.y -35, transform.parent.position.y + 35);
            
            transform.position = new Vector2(x, y);

            Debug.Log(transform.position);*/
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
        //a garder au cas où
        //PlayerMain.Instance.Move.SetMoveInput(_direction);
    }
}