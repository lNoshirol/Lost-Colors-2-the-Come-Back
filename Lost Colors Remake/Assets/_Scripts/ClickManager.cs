using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;

    public event Action OnClickStart;
    public event Action OnClickEnd;

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
