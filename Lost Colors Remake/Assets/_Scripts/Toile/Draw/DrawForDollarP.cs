using PDollarGestureRecognizer;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class DrawForDollarP : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;
    [SerializeField] private List<Vector3> points = new();
    public Vector2 _currentPoint;
    [SerializeField] float _drawOffset;
    private DrawData _drawData;
    [SerializeField] private Color _currentColor;

    public List<Gesture> trainingSet = new List<Gesture>();

    public Camera Cam;
    public bool touchingScreen = false;

    public ExternalDrawFunctions _extDrawFunc;
    [SerializeField] private DetectEnemyInShape _detectEnemyInShape;
    public CatchEnnemyOnDraw _catchEnnemy;

    public LayerMask IgnoreMeUwU;
    public LayerMask IgnoreMeUwU2;

    /*    private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }*/

    void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/LostColors/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        Cam = Camera.main;


        //ClickManager.instance.OnClickStart += OnTouchStart;
        //ClickManager.instance.OnClickEnd += OnTouchEnd;

        //Debug.Log("CastSpriteShape.cs l59/ " + _currentColor.ToString());
    }

    void Update()
    {
        /*if (touchingScreen)
        {
            //ToileMain.Instance.RaycastDraw.DrawRayCastInRealTime();
            AddPoint();
        }*/

        AddPoint2D();


        //DebugRay();
        //ToileMain.Instance.RaycastDraw.DebugRaycastLines();



    }

    public void OnTouchScreen(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log($"CastSpriteShape L74/ AAAAAAAAAAAAH {gameObject.transform.parent.gameObject.activeSelf}");

        if (callbackContext.started)
        {
            OnTouchStart();
        }

        if (callbackContext.canceled)
        {
            OnTouchEnd();
        }
    }

    [Obsolete]
    public void OnTouchStart()
    {
        Debug.Log("gawr gura AH");

        ToileMain.Instance.RaycastDraw.ClearRaycastLines();
        touchingScreen = true;
        points.Clear();
        lineRenderer.positionCount = 0;

        if (!ToileMain.Instance.gestureIsStarted && gameObject.transform.parent.gameObject.activeSelf)
            ToileMain.Instance.timerCo = StartCoroutine(ToileMain.Instance.ToileTimer());

        lineRenderer.SetColors(_currentColor, _currentColor);
    }

    public void OnTouchEnd()
    {
        if (points.Count > 10)
        {
            List<Point> drawReady = _extDrawFunc.Vec3ToPoints(_extDrawFunc.RecenterAndRotate(points));

            //GetSpellTargetPointFromCentroid(points);
            //GetSpellTargetPointFromCenter(points);

            Gesture candidate = new Gesture(drawReady.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            //Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

            //TryMakeAdaptativeCollider(GetDrawCenter(points), gestureResult);

            //_drawData = new DrawData(points, GetDrawDim(points), gestureResult, GetSpellTargetPointFromCenter(points), ColorUtility.ToHtmlStringRGB(_currentColor));
            if (gestureResult.Score < 0.8)
            {
                touchingScreen = false;

                foreach (GameObject enemy in _detectEnemyInShape.GetTargetsInShape())
                {
                    enemy.GetComponent<EnemyHealth>().EnemyHealthChange(100);

                }

                return;

            }

            switch (gestureResult.GestureClass)
            {
                case "thunder":
                    foreach (GameObject enemy in EnemyManager.Instance.CurrentEnemyList)
                    {
                        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                        if (health.enemyArmorId == "raccoon_armor")
                        {
                            health.ArmorLost();
                        }
                    }
                    foreach (GameObject enemy in _detectEnemyInShape.GetTargetsInShape())
                    {
                        Debug.Log(enemy);
                        enemy.GetComponent<EnemyHealth>().EnemyHealthChange(100);

                    }
                    break;

            }
        }

        touchingScreen = false;
    }

    private void UpdateLinePoints()
    {
        if (lineRenderer != null && points.Count > 1)
        {
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    private void AddPoint2D()
    {
        Debug.Log("TouchScreen : " + Touchscreen.current);
        Debug.Log("Mouse : " + Mouse.current);

        if (Touchscreen.current != null)
        {
            _currentPoint = Cam.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
        }

        else if (Mouse.current != null)
        {
            _currentPoint = Cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if (points.Count == 0)
        {
            points.Add(_currentPoint);
            ToileMain.Instance.RaycastDraw.DrawRayCastInRealTime();

            UpdateLinePoints();
            return;
        }
        else
        {
            currentDistance = Vector3.Distance(points[points.Count - 1], _currentPoint);

            if (currentDistance >= distanceBetweenPoint)
            {
                points.Add(_currentPoint);
                ToileMain.Instance.RaycastDraw.DrawRayCastInRealTime();

                UpdateLinePoints();
                return;
            }
        }
    }

    private void AddPoint()
    {
        Ray Ray;

        if (Touchscreen.current != null)
        {
            Ray = Cam.ScreenPointToRay(Touchscreen.current.position.ReadValue());
        }

        else if (Mouse.current != null)
        {
            Ray = Cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        else
        {
            Ray = new Ray();
        }

        RaycastHit hit;

        _catchEnnemy.EnnemyOnPath(Ray, IgnoreMeUwU2);

        if (Physics.Raycast(Ray, out hit) && hit.collider != null)
        {
            if (hit.collider.CompareTag("Writeable"))
            {
                if (points.Count == 0)
                {
                    points.Add(hit.point + new Vector3(0, _drawOffset, -_drawOffset));

                    UpdateLinePoints();
                    return;
                }
                else
                {
                    currentDistance = Vector3.Distance(points[points.Count - 1], hit.point);

                    if (currentDistance >= distanceBetweenPoint)
                    {
                        points.Add(hit.point + new Vector3(0, _drawOffset, -_drawOffset));

                        UpdateLinePoints();
                        return;
                    }
                }
            }
        }
    }

    public void Resetpoint()
    {
        lineRenderer.positionCount = 0;
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        TouchSimulation.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }


    private void FingerDown(Finger finger)
    {

    }
}
