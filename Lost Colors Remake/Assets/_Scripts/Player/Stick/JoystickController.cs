using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private OnScreenStick stick;
    [SerializeField] private RectTransform stickRect;

    void Awake()
    {
        stick = GetComponent<OnScreenStick>();
        stickRect = GetComponent<RectTransform>();
    }

    public void SimulateTouch(Vector2 screenPosition)
    {
        // Création d'un faux PointerEventData
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = screenPosition;

        // Conversion en local UI
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            stickRect.parent as RectTransform,
            screenPosition,
            null,
            out localPoint);

        stickRect.anchoredPosition = localPoint;

        // Simuler PointerDown
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.beginDragHandler);
    }

    public void SimulateDrag(Vector2 screenPosition)
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = screenPosition;
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.dragHandler);
        Debug.Log("bouge ta mere fdp");
    }

    public void SimulateRelease(Vector2 screenPosition)
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = screenPosition;
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.endDragHandler);
    }
}