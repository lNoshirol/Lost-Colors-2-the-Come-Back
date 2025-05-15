using UnityEngine;

public class Joystick : MonoBehaviour
{
    public static Joystick instance;

    [SerializeField] Vector2 endPos;
    public Transform Player;
    public float speed;

    public bool canMove;

    public JoystickAnime _joystickAnime;

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
    }

    private void OnMouseUp()
    {
        StickDisappear.instance.StartCoroutineDisapear();
    }

    private void Update()
    {
        if (ClickManager.instance.TouchScreen && canMove)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 offset = endPos - new Vector2(transform.parent.position.x, transform.parent.position.y);
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
            MovePlayer(direction);
            transform.position = new Vector2(transform.parent.position.x + direction.x, transform.parent.position.y + direction.y);
        }
        else
        {
            transform.position = transform.parent.position;
        }

    }

    public void MovePlayer(Vector2 _direction)
    {
        //Player.Translate(Time.deltaTime * speed * _direction);

        Debug.DrawRay(Vector2.zero, _direction * 2, Color.red);
    }
}
