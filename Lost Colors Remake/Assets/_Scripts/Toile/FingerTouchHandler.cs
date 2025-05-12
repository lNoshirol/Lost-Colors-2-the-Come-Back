using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Threading.Tasks;

public class FingerTouchHandler : MonoBehaviour
{
    [SerializeField] private CastSpriteShape castSpriteShape;

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += OnFingerUp;
    }

    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= OnFingerUp;

        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

    private void OnFingerDown(Finger finger)
    {
        if (castSpriteShape != null)
        {
            castSpriteShape.OnTouchStart();
        }
    }

    private void OnFingerUp(Finger finger)
    {
        if (castSpriteShape != null)
        {
            castSpriteShape.OnTouchEnd();
        }
    }
}
