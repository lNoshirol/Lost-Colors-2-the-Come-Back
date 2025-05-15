using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;

    public event Action OnClickStart;
    public event Action OnClickEnd;

    public bool TouchScreen {  get; private set; }
    public bool isScreenTouch;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnClickStart?.Invoke();
            TouchScreen = true;
            isScreenTouch = true;
            Debug.Log("click start");
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnClickEnd?.Invoke();
            TouchScreen = false;
            isScreenTouch= false;
            Debug.Log("Click End");
        }
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Oh tu prefere moi ou ton pere ?");

        if (callbackContext.started)
        {
            OnClickStart?.Invoke();
            Debug.Log("click start");
        }

        if (callbackContext.canceled)
        {
            OnClickEnd?.Invoke();
            Debug.Log("Click End");
        }
    }
}
