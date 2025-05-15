using UnityEngine;

public class StickAppearrAtClickLoc : MonoBehaviour
{
    [SerializeField] GameObject _stick;
    public Bounds bounds;

    private Vector2 lastTouchPos;

    private void Start()
    {
        ClickManager.instance.OnClickStart += OnTouchStart;
        ClickManager.instance.OnClickEnd += OnTouchEnd;
    }

    public void OnTouchStart()
    {
        Joystick.instance._joystickAnime.BoolSwitcher("IsHere", true);

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lastTouchPos = touchPosition;
        _stick.SetActive(true);

        if (bounds.Contains(touchPosition))
        {
            _stick.transform.position = touchPosition;
            Debug.Log("Position Changed inside bounds");

        }
        else
        {
            touchPosition = bounds.ClosestPoint(touchPosition);
            _stick.transform.position = touchPosition;
            Debug.Log("Position Changed snap to closest point");
        }
    }

    public void OnTouchEnd()
    {
        Joystick.instance._joystickAnime.BoolSwitcher("IsHere", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
