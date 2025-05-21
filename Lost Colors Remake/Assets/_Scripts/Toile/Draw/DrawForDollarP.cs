using PDollarGestureRecognizer;
using System.Collections.Generic;
using System;
using UnityEngine;

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

        if (ClickManager.instance.TouchScreen)
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 50, Color.blue);

            AddPoint2D();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            _catchEnnemy.EnnemyOnPath(ray, IgnoreMeUwU);
        }


        //ToileMain.Instance.RaycastDraw.DebugRaycastLines();


    }

    [Obsolete]
    public void OnTouchStart()
    {

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
        _catchEnnemy.IlEstTaMereLaPute();

        if (points.Count > 10)
        {
            List<Point> drawReady = _extDrawFunc.Vec3ToPoints(_extDrawFunc.RecenterAndRotate(points));

            //GetSpellTargetPointFromCentroid(points);
            //GetSpellTargetPointFromCenter(points);

            Gesture candidate = new Gesture(drawReady.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
            _drawData = new DrawData(points, _extDrawFunc.GetDrawDim(points), gestureResult, _extDrawFunc.GetSpellTargetPointFromCenter(points), ColorUtility.ToHtmlStringRGB(_currentColor));

            OnDrawFinish?.Invoke(_drawData);

            Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

            //TryMakeAdaptativeCollider(GetDrawCenter(points), gestureResult);

            //TEMP A FIXER PLUS TARD
            if (gestureResult.Score < 0.1)
            {
                touchingScreen = false;

                foreach (GameObject enemy in _detectEnemyInShape.GetTargetsInShape())
                {
                    enemy.GetComponent<EnemyHealth>().EnemyHealthChange(100);

                }

                return;

            }
            else
            {
                EnemyManager.Instance.ArmorLost(gestureResult.GestureClass);
            }



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

        _currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

    public void Resetpoint()
    {
        lineRenderer.positionCount = 0;
        points.Clear();
    }
}
