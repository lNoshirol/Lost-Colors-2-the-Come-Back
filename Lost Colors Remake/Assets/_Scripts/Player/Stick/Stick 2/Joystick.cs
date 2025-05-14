using UnityEngine;
using UnityEngine.InputSystem;

public class Joystick : MonoBehaviour
{
    [SerializeField] Vector2 startsPos, endPos, initialPos;
    [SerializeField] private bool touchStart;
    public Transform Player;
    public float speed;

    //[SerializeField] GameObject _stickCenter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPos = transform.parent.position;
    }

    private void OnMouseDown()
    {
        startsPos = /*Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue())*/ transform.parent.position;
        Debug.Log("Je vais te toucher la nuit");
    }

    private void OnMouseDrag()
    {
        touchStart = true;
        endPos = Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
        Debug.Log("Dit, le fécondeur d'homme");
    }

    private void OnMouseUp()
    {
        touchStart = false;
    }

    private void Update()
    {
        if (touchStart)
        {
            Vector2 offset = endPos - startsPos;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
            MovePlayer(direction);
            transform.position = new Vector2(startsPos.x + direction.x, startsPos.y + direction.y);
        }
        else
        {
            transform.position = transform.parent.position;
        }
    }

    public void MovePlayer(Vector2 _direction)
    {
        Player.Translate(Time.deltaTime * speed * _direction);
    }
}
