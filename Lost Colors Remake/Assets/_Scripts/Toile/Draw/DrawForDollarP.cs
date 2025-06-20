using PDollarGestureRecognizer;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class DrawForDollarP : MonoBehaviour
{
    public static DrawForDollarP instance;

    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;
    [SerializeField] private List<Vector3> points = new();
    public Vector2 _currentPoint;
    [SerializeField] float _drawOffset;
    private DrawData _drawData;

    [Header("Visual")]
    [SerializeField] LineRenderer lineRenderer;
    private Color _currentColor;
    [SerializeField] private List<Color> _possibleColors;

    

    public List<Gesture> trainingSet = new List<Gesture>();

    public Camera Cam;
    public bool touchingScreen = false;

    public ExternalDrawFunctions _extDrawFunc;
    [SerializeField] private DetectEnemyInShape _detectEnemyInShape;
    public CatchThingsOnDraw _catchEnnemy;

    public LayerMask IgnoreMeUwU;
    public LayerMask IgnoreMeUwU2;

    public event Action<DrawData> OnDrawFinish;

/*    [Header("Debug")]
    public TextMeshProUGUI touuchStart;*/

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/LostColors/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        Cam = Camera.main;


        ClickManager.instance.OnClickStart += OnTouchStart;
        ClickManager.instance.OnClickEnd += OnTouchEnd;


        //Debug.Log("CastSpriteShape.cs l59/ " + _currentColor.ToString());

        //ClickManager.instance.OnClickStart += Resetpoint;
    }

    void Update()
    {
        /*if (touchingScreen)
        {
            //ToileMain.Instance.RaycastDraw.DrawRayCastInRealTime();
            AddPoint();
        }*/

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                OnTouchStart();
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                OnTouchEnd();
            }
        }

        if (ClickManager.instance.TouchScreen || Input.touchCount > 0)
        {

            AddPoint2D();

            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray2D ray = new(rayOrigin, Vector2.up*0.1f);

            _catchEnnemy.EnnemyOnPath(ray, IgnoreMeUwU);
        }

        //ToileMain.Instance.RaycastDraw.DebugRaycastLines();
    }


    public void OnTouchStart()
    {

        ToileMain.Instance.RaycastDraw.ClearRaycastLines();
        touchingScreen = true;
        points.Clear();
        lineRenderer.positionCount = 0;

        if (!ToileMain.Instance.gestureIsStarted && gameObject.transform.parent.gameObject.activeSelf)
            ToileMain.Instance.timerCo = StartCoroutine(ToileMain.Instance.ToileTimer());

        _currentColor = _possibleColors[UnityEngine.Random.Range(0, _possibleColors.Count)];
        lineRenderer.startColor = _currentColor;
        lineRenderer.endColor = _currentColor;

        AddPoint2D();
        PlayerMain.Instance.Inventory.SetStartAmount();
    }

    public void OnTouchEnd()
    {
        if (points.Count > 1)
        {
            List<Point> drawReady = _extDrawFunc.Vec3ToPoints(points);

            Gesture candidate = new Gesture(drawReady.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
            _drawData = new DrawData(points, _extDrawFunc.GetDrawDim(ExternalDrawFunctions.GetMinMaxCoordinates(points)), 
                gestureResult, _extDrawFunc.GetSpellTargetPointFromCenter(points), ColorUtility.ToHtmlStringRGB(_currentColor));

            _catchEnnemy.CatchObjectOnLine(_drawData);

            if (gestureResult.Score < PlayerMain.Instance.toileInfo.tolerance)
            {
                foreach (GameObject enemy in _detectEnemyInShape.GetTargetsInShape())
                {
                    if (_detectEnemyInShape.GetTargetsInShape().Count != 0)
                    {
                        Debug.Log("grosse folle");

                        _drawData.result.GestureClass = "Rond";

                        OnDrawFinish?.Invoke(_drawData);

                        ApplyDamageAfterDraw.Instance.AddEnnemyDamage(enemy.GetComponent<EnemyHealth>(), PlayerMain.Instance.toileInfo.shapeDamage, _drawData);
                    }
                    else 
                    {
                        Debug.Log("5");

                        _drawData.result.GestureClass = "Trait";

                        OnDrawFinish?.Invoke(_drawData);

                        _catchEnnemy.CatchObjectOnLine(_drawData);
                    }
                }
            }
            else
            {
                OnDrawFinish?.Invoke(_drawData);
                ApplyDamageAfterDraw.Instance.AddEnemyGlyphToTej(gestureResult.GestureClass);
            }
            touchingScreen = false;
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
        _currentPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        if (PlayerMain.Instance.Inventory.currentPaintAmont == 0) TriggerToile.instance.OpenAndCloseToileMagique();

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

                PlayerMain.Instance.Inventory.EditPaintAmount();
                ToileMain.Instance.ToileUI.UpdateToilePaintAmount();

                UpdateLinePoints();
                return;
            }
        }
    }

    public List<Vector3> GetDrawPoints()
    {
        return points;
    }

    public void Resetpoint()
    {
        lineRenderer.positionCount = 0;
        points.Clear();
    }

    private void OnDisable()
    {
        points.Clear();
    }

    public void SetActiveLine(bool myBool)
    {
        lineRenderer.gameObject.SetActive(myBool);
    }
}
