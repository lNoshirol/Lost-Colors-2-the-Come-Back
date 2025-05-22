using System;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;

    public event Action OnClickStart;
    public event Action OnClickEnd;

    public bool TouchScreen {  get; private set; }
    public bool isScreenTouch; 

    public RuntimePlatform platform { get ; private set; }
    public RuntimePlatform DebugPlatform;

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

        platform = Application.platform;
        DebugPlatform = platform;
    }

    private void Update()
    {

        if (platform == RuntimePlatform.Android)
        {

        }
        else if (platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnClickStart?.Invoke();
                TouchScreen = true;
                isScreenTouch = true; //Debug bool
                //Debug.Log($"click start {Input.touchCount}");
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                OnClickEnd?.Invoke();
                TouchScreen = false;
                isScreenTouch = false; //Debug bool
                //Debug.Log($"Click End");
            }
        }


    }
}
