using PDollarGestureRecognizer;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class DrawForDollarP : MonoBehaviour
{
    public static DrawForDollarP instance;

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

        lineRenderer.startColor = _currentColor;
        lineRenderer.endColor = _currentColor;

        PlayerMain.Instance.Inventory.SetStartAmount();
    }

    public void OnTouchEnd()
    {
        if (points.Count > 10)
        {
            //List<Point> drawReady = _extDrawFunc.Vec3ToPoints(_extDrawFunc.RecenterAndRotate(points)); //20% de précision si on dégage le recenter and rotate
            List<Point> drawReady = _extDrawFunc.Vec3ToPoints(points);

            //GetSpellTargetPointFromCentroid(points);
            //GetSpellTargetPointFromCenter(points);

            Gesture candidate = new Gesture(drawReady.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
            _drawData = new DrawData(points, _extDrawFunc.GetDrawDim(ExternalDrawFunctions.GetMinMaxCoordinates(points)), gestureResult, _extDrawFunc.GetSpellTargetPointFromCenter(points), ColorUtility.ToHtmlStringRGB(_currentColor));

            OnDrawFinish?.Invoke(_drawData);

            Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

            //TryMakeAdaptativeCollider(GetDrawCenter(points), gestureResult);

            _catchEnnemy.CatchObjectOnLine();

            //TEMP A FIXER PLUS TARD
            if (gestureResult.Score < 0.9)
            {
                foreach (GameObject enemy in _detectEnemyInShape.GetTargetsInShape())
                {
                    if (_detectEnemyInShape.GetTargetsInShape().Count != 0) enemy.GetComponent<EnemyHealth>().EnemyLoseHP(PlayerMain.Instance.toileInfo.shapeDamage);
                    else { _catchEnnemy.CatchObjectOnLine(); }
                }

                return;
            }
            else
            {
                EnemyManager.Instance.ArmorLost(gestureResult.GestureClass);
            }
            touchingScreen = false;

            //switch (gestureResult.GestureClass)
            //{
            //    case "thunder":
            //        foreach (GameObject enemy in EnemyManager.Instance.CurrentEnemyList)
            //        {
            //            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            //            if (health.enemyArmorId == "raccoon_armor")
            //            {
            //                //health.ArmorLost();
            //            }
            //        }
            //        foreach (GameObject enemy in _detectEnemyInShape.GetTargetsInShape())
            //        {
            //            Debug.Log(enemy);
            //            enemy.GetComponent<EnemyHealth>().EnemyHealthChange(100);

            //        }
            //        break;

            //}
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
}
