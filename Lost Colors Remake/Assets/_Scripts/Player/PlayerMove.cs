using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _topSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private float _friction;
    public bool canMove;

    public Vector2 _moveInput;
    private Vector3 _movementForce;


    private void Start()
    {
        canMove = true;
    }
    void Update()
    {
        float targetSpeedX = _moveInput.x * _topSpeed;
        float targetSpeedZ = _moveInput.y * _topSpeed;

        float speedDifX = targetSpeedX - PlayerMain.Instance.Rigidbody2D.linearVelocity.x;
        float speedDifZ = targetSpeedZ - PlayerMain.Instance.Rigidbody2D.linearVelocity.y;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? _acceleration : _deceleration;
        float accelRateY = (Mathf.Abs(targetSpeedZ) > 0.01f) ? _acceleration : _deceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, _velocityPower) * Mathf.Sign(speedDifX);
        float movementZ = Mathf.Pow(Mathf.Abs(speedDifZ) * accelRateY, _velocityPower) * Mathf.Sign(speedDifZ);

        _movementForce = Vector3.right * movementX + Vector3.up * movementZ;

        PlayerMain.Instance.Rigidbody2D.AddForce(_movementForce * Time.deltaTime * 50);

        if (Mathf.Abs(_moveInput.x) < 0.01f)
        {
            float frictionX = Mathf.Min(Mathf.Abs(PlayerMain.Instance.Rigidbody2D.linearVelocity.x), Mathf.Abs(_friction));
            frictionX *= Mathf.Sign(PlayerMain.Instance.Rigidbody2D.linearVelocity.x);
            PlayerMain.Instance.Rigidbody2D.AddForce(Vector3.right * -frictionX);
        }

        if (Mathf.Abs(_moveInput.y) < 0.01f)
        {
            float frictionY = Mathf.Min(Mathf.Abs(PlayerMain.Instance.Rigidbody2D.linearVelocity.y), Mathf.Abs(_friction));
            frictionY *= Mathf.Sign(PlayerMain.Instance.Rigidbody2D.linearVelocity.y);
            PlayerMain.Instance.Rigidbody2D.AddForce(Vector3.up * -frictionY);
        }
    }

    public void Move()
    {
        if (canMove)
        {
            _moveInput = new Vector2();

            if (_moveInput != Vector2.zero)
            {
                //PlayerMain.Instance.PlayerMesh.transform.rotation = Quaternion.LookRotation(_moveInput);
            }
        }

    }
}
