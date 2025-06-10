using UnityEngine;
using UnityEngine.XR;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _topSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private float _friction;
    [SerializeField] private Joystick NEW_JOYSTICK;
    public bool canMove;

    public Vector2 _moveInput;
    private Vector3 _movementForce;


    private void Start()
    {
        canMove = true;

        ClickManager.instance.OnClickEnd += CancelMoveInput;
    }
    void Update()
    {
        if (!canMove) return;

        if (Input.GetKey(KeyCode.W))
        {
            _moveInput += Vector2.up;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _moveInput -= Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveInput += Vector2.left;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _moveInput += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveInput += Vector2.down;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _moveInput += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveInput += Vector2.right;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _moveInput += Vector2.right;
        }



        Vector2 _normalizedMoveInput = _moveInput.normalized/2;

        float targetSpeedX = _normalizedMoveInput.x * _topSpeed;
        float targetSpeedZ = _normalizedMoveInput.y * _topSpeed;

        float speedDifX = targetSpeedX - PlayerMain.Instance.Rigidbody2D.linearVelocity.x;
        float speedDifZ = targetSpeedZ - PlayerMain.Instance.Rigidbody2D.linearVelocity.y;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? _acceleration : _deceleration;
        float accelRateY = (Mathf.Abs(targetSpeedZ) > 0.01f) ? _acceleration : _deceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, _velocityPower) * Mathf.Sign(speedDifX);
        float movementZ = Mathf.Pow(Mathf.Abs(speedDifZ) * accelRateY, _velocityPower) * Mathf.Sign(speedDifZ);

        _movementForce = Vector3.right * movementX + Vector3.up * movementZ;

        PlayerMain.Instance.Rigidbody2D.AddForce(_movementForce * Time.deltaTime * 50);

        if (Mathf.Abs(_normalizedMoveInput.x) < 0.01f)
        {
            float frictionX = Mathf.Min(Mathf.Abs(PlayerMain.Instance.Rigidbody2D.linearVelocity.x), Mathf.Abs(_friction));
            frictionX *= Mathf.Sign(PlayerMain.Instance.Rigidbody2D.linearVelocity.x);
            PlayerMain.Instance.Rigidbody2D.AddForce(Vector3.right * -frictionX);
        }

        if (Mathf.Abs(_normalizedMoveInput.y) < 0.01f)
        {
            float frictionY = Mathf.Min(Mathf.Abs(PlayerMain.Instance.Rigidbody2D.linearVelocity.y), Mathf.Abs(_friction));
            frictionY *= Mathf.Sign(PlayerMain.Instance.Rigidbody2D.linearVelocity.y);
            PlayerMain.Instance.Rigidbody2D.AddForce(Vector3.up * -frictionY);
        }

        //JoystickLucas.instance.MovePlayer();


        //Juste pour avoir un semblant d'anime quand on est au clavier
        //if (new Vector2(NEW_JOYSTICK.Horizontal, NEW_JOYSTICK.Vertical) != new Vector2(0, 0))
        //{
        //    SetMoveInput(new Vector2(NEW_JOYSTICK.Horizontal, NEW_JOYSTICK.Vertical));
        //}
        //else
        //{
        //    SetMoveInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        //}
    }


    public void Move()
    {
        if (canMove)
        {
            _moveInput = new Vector2().normalized;
        }
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        //moveInput.Normalize();
        _moveInput = moveInput;
    }

    public void CancelMoveInput()
    {
        _moveInput = Vector2.zero;
    }
}
