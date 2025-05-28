using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectEnemyInShape : MonoBehaviour
{
    [SerializeField] private RaycastDraw _rayCastDraw;
    [SerializeField] private DrawForDollarP _pen;
    private List<Vector2> _shapePoints = new();
    private List<Vector2> _target2DPos = new();
    private List<GameObject> _targets = new();
    private Vector2 _enemyPoint;

    [Header("Debug")]
    public Vector2 intersection;
    public GameObject target;

    public void Init()
    {
        // à convertir en List<Vector2> + _pen.GetDrawData().points ne renvoit pas souvent de points
        _shapePoints = _rayCastDraw.points2D;
        _targets = EnemyManager.Instance.CurrentEnemyList;
    }

    public List<GameObject> GetTargetsInShape()
    {
        Init();
        Debug.Log("nombre de point de la forme : " + _shapePoints.Count);

        List<GameObject> result = new();
        bool isLine = false;

        _target2DPos = TargetsPosToScreenPos(_targets);

        if (_shapePoints.Count >= 2)
        {
            float distance = Vector2.Distance(_shapePoints[0], _shapePoints[^1]);
            float closeThreshold = 70f;

            if (distance < closeThreshold)
            {
                _shapePoints.Add(_shapePoints[0]); 
                Debug.Log("Forme fermée via points proches " + Camera.main.ScreenToWorldPoint(ExternalDrawFunctions.GetDrawCenter(ExternalDrawFunctions.GetMinMaxCoordinates(ExternalDrawFunctions.Vec2ShapeToVec3Shape(_shapePoints)))));
            }
            else if(HasSelfIntersection(_shapePoints))
            {
                Debug.Log("Forme fermée via intersection " + Camera.main.ScreenToWorldPoint(ExternalDrawFunctions.GetDrawCenter(ExternalDrawFunctions.GetMinMaxCoordinates(ExternalDrawFunctions.Vec2ShapeToVec3Shape(_shapePoints)))));
                Instantiate(PlayerMain.Instance.playerSprite, _shapePoints[0], Quaternion.identity);
            }
            else
            {
                isLine = true;
                Debug.Log("une ligne");
            }
        }

        if (!isLine)
        {
            for (int i = 0; i < _target2DPos.Count; i++)
            {
                if (IsInside(_target2DPos[i]))
                {
                    result.Add(_targets[i]);
                }
            }
        }

        return result;
    }

    private List<Vector2> TargetsPosToScreenPos(List<GameObject> targets)
    {
        List<Vector2> target2DPos = new();

        foreach (GameObject target in targets)
        {
            target2DPos.Add(Camera.main.WorldToScreenPoint(target.transform.position));
            //print($"[D.E.I.S.] 3D : {target.transform.position}, 2D : {Camera.main.WorldToScreenPoint(target.transform.position)}");
        }

        return target2DPos;
    }

    // https://www.youtube.com/watch?v=E51LrZQuuPE
    private bool IsInside(Vector2 position)
    {
        float windingNumber = 0.0f;

        //Going round in a circle
        for (int i = 0; i < this._shapePoints.Count; i++)
        {
            var a = this._shapePoints[i];
            var b = this._shapePoints[(i + 1) % this._shapePoints.Count];

            //Calculate the positions relative to the point
            var pointA = position - a;
            var pointB = position - b;

            //If one point is above and one point is below, only one of them has a negative value. Therefore if we multiply them together and
            //the number is negative, the edge crosses the horizontal line
            if (pointA.y * pointB.y < 0.0f)
            {
                //r represents the X-Coordinate relative to our position (name r was chosen in literature, it is not my doing ^^)
                //Calculating the crossing point would be
                //p = a + Mathf.InverseLerp(b.y, a.y, 0) * (b - a);
                //So calculating r is the same as:
                //r = a.x + Mathf.InverseLerp(b.y, a.y, 0) * (b.x - a.x);
                //If you write it out you'd get the code below:
                float r = pointA.x + (pointA.y * (pointB.x - pointA.x)) / (pointA.y - pointB.y);
                if (r < 0)
                {
                    if (pointA.y < 0.0f)
                    {
                        windingNumber += 1.0f;
                    }
                    else
                    {
                        windingNumber -= 1.0f;
                    }
                }
            }
            else if (pointA.y == 0.0f && pointA.x > 0.0f)
            {
                if (pointB.y > 0.0f)
                {
                    windingNumber += 0.5f;
                }
                else
                {
                    windingNumber -= 0.5f;
                }
            }
            else if (pointB.y == 0.0f && pointB.x > 0.0f)
            {
                if (pointA.y < 0.0f)
                {
                    windingNumber += 0.5f;
                }
                else
                {
                    windingNumber -= 0.5f;
                }
            }
        }

        return ((int)windingNumber % 2) != 0;
    }


    public void PROTOTrashDebug()
    {
        GetTargetsInShape();
    }

    private bool HasSelfIntersection(List<Vector2> points)
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 a1 = points[i];
            Vector2 a2 = points[i + 1];

            for (int j = i + 2; j < points.Count - 1; j++)
            {
                if (i == 0 && j == points.Count - 2) continue;

                Vector2 b1 = points[j];
                Vector2 b2 = points[j + 1];

                if (DoSegmentsIntersect(a1, a2, b1, b2))
                {
                    Vector2 intersectionPoint = GetIntersectionPoint(a1, a2, b1, b2);

                    if (intersectionPoint != Vector2.positiveInfinity)
                    //Instantiate(PlayerMain.Instance.playerSprite.gameObject, new Vector3(intersectionPoint.x, intersectionPoint.y, 0), Quaternion.identity);

                    Debug.DrawLine(a1, a2, Color.blue, 5f);
                    Debug.DrawLine(b1, b2, Color.blue, 5f);
                    Debug.Log($"Intersection entre {i}-{i + 1} et {j}-{j + 1}");
                    IntersectionListUpdate(i + 1, j, intersection);
                    return true;
                }
            }
        }
        return false;
    }

    private void IntersectionListUpdate(int secondPointIndex, int penultimatePointIndex, Vector2 intersectionPoint)
    {
        List<Vector2> newShape = new()
        {
            intersectionPoint
        };

        for (int i = 0; i < penultimatePointIndex; i++)
        {
            if (i >= secondPointIndex && i <= penultimatePointIndex)
            {
                newShape.Add(_shapePoints[i]);
            }
        }
        newShape.Add(intersectionPoint);
        
        _shapePoints = newShape;
    }


    //ALGO : Segment Intersection
    private bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        float o1 = Orientation(p1, p2, q1);
        float o2 = Orientation(p1, p2, q2);
        float o3 = Orientation(q1, q2, p1);
        float o4 = Orientation(q1, q2, p2);

        if (o1 != o2 && o3 != o4)
            return true;

        if (o1 == 0 && OnSegment(p1, q1, p2)) return true;
        if (o2 == 0 && OnSegment(p1, q2, p2)) return true;
        if (o3 == 0 && OnSegment(q1, p1, q2)) return true;
        if (o4 == 0 && OnSegment(q1, p2, q2)) return true;

        return false;
    }

    private int Orientation(Vector2 a, Vector2 b, Vector2 c)
    {
        float val = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
        if (Mathf.Approximately(val, 0)) return 0;
        return (val > 0) ? 1 : 2;
    }

    private bool OnSegment(Vector2 a, Vector2 b, Vector2 c)
    {
        return b.x <= Mathf.Max(a.x, c.x) && b.x >= Mathf.Min(a.x, c.x) &&
               b.y <= Mathf.Max(a.y, c.y) && b.y >= Mathf.Min(a.y, c.y);
    }

    private Vector2 GetIntersectionPoint(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float A1 = p2.y - p1.y;
        float B1 = p1.x - p2.x;
        float C1 = A1 * p1.x + B1 * p1.y;

        float A2 = p4.y - p3.y;
        float B2 = p3.x - p4.x;
        float C2 = A2 * p3.x + B2 * p3.y;

        float denominator = A1 * B2 - A2 * B1;

        if (Mathf.Approximately(denominator, 0))
        {
            return Vector2.positiveInfinity;
        }

        float x = (B2 * C1 - B1 * C2) / denominator;
        float y = (A1 * C2 - A2 * C1) / denominator;

        intersection = Camera.main.ScreenToWorldPoint( new Vector2(x, y));

        target.transform.position = intersection;

        return intersection;
    }
}