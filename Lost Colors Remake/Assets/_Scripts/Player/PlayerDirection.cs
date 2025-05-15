using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public enum Direction
    {
        Idle,
        Up,
        Down,
        Left,
        Right
    }

    public Direction CurrentDirection { get; private set; }

    private Rigidbody2D _rb;
    private Vector2 _lastMove;
    private Direction _previousDirection;

    [SerializeField] private Animator _animator;

    void Start()
    {
        _rb = PlayerMain.Instance.Rigidbody2D;
        _previousDirection = Direction.Idle;
    }

    void Update()
    {
        Vector2 velocity = _rb.linearVelocity;

        if (velocity.magnitude > 0.1f)
        {
            _lastMove = velocity.normalized;

            if (Mathf.Abs(_lastMove.x) > Mathf.Abs(_lastMove.y))
            {
                CurrentDirection = (_lastMove.x > 0) ? Direction.Right : Direction.Left;
            }
            else
            {
                CurrentDirection = (_lastMove.y > 0) ? Direction.Up : Direction.Down;
            }
        }
        else
        {
            CurrentDirection = Direction.Idle;
        }
        DirectionAnimation();
    }

    public void DirectionAnimation()
    {
        if (CurrentDirection != _previousDirection)
        {
            switch (CurrentDirection)
            {
                case Direction.Right:
                    Debug.Log("👉 Le joueur regarde vers la DROITE");
                    _animator.Play("Walk" + $"{CurrentDirection}");
                    break;
                case Direction.Left:
                    Debug.Log("👈 Le joueur regarde vers la GAUCHE");
                    _animator.Play("Walk" + $"{CurrentDirection}");
                    break;
                case Direction.Up:
                    Debug.Log("👆 Le joueur regarde vers le HAUT");
                    _animator.Play("Walk" + $"{CurrentDirection}");
                    break;
                case Direction.Down:
                    Debug.Log("👇 Le joueur regarde vers le BAS");
                    _animator.Play("Walk" + $"{CurrentDirection}");
                    break;
                case Direction.Idle:
                    Debug.Log("😴 Le joueur est IMMOBILE");
                    _animator.Play("Idle");
                    break;
            }

            _previousDirection = CurrentDirection;
        }
    }
}
