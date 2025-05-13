using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class StickAppearrAtClickLoc : MonoBehaviour
{
    [SerializeField] GameObject _stick;

    private void Start()
    {
        ClickManager.instance.OnClickStart += OnTouchStart;
        ClickManager.instance.OnClickEnd += OnTouchEnd;
    }

    public void OnTouchStart()
    {
        Debug.Log("Position Changed");
        Vector2 touchPosition = Touchscreen.current.position.ReadValue();
        _stick.SetActive(true);
        _stick.transform.position = touchPosition;
    }

    public void OnTouchEnd()
    {

    }
}
