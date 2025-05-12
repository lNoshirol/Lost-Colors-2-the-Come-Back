using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyInShape : MonoBehaviour
{
    [SerializeField] private RaycastDraw _rayCastDraw;
    [SerializeField] private CastSpriteShape _pen;
    private List<Vector2> _shapePoints = new();
    private List<Vector2> _target2DPos = new();
    private List<GameObject> _targets = new();
    private Vector2 _enemyPoint;

    public void Init()
    {
        // à convertir en List<Vector2> + _pen.GetDrawData().points ne renvoit pas souvent de points
        _shapePoints = _rayCastDraw.points2D;
        _targets = EnemyManager.Instance.CurrentEnemyList;
    }

    public List<GameObject> GetTargetsInShape()
    {
        Init();
        Debug.Log(_shapePoints.Count);

        List<GameObject> result = new();

        _target2DPos = TargetsPosToScreenPos(_targets);

        _shapePoints.Add(_shapePoints[0]);

        for(int i = 0; i < _target2DPos.Count; i++)
        {
            if (IsInside(_target2DPos[i]))
            {
                result.Add(_targets[i]);
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
            print($"[D.E.I.S.] 3D : {target.transform.position}, 2D : {Camera.main.WorldToScreenPoint(target.transform.position)}");
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
}
