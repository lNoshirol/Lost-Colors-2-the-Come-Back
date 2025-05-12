using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class ExternalDrawFunctions : MonoBehaviour
{
    public Camera Cam;
    public GameObject CubeCentroid;
    public GameObject CubeCentre;
    public GameObject CubeTest;
    public LayerMask IgnoreMeUwU;
    public LayerMask IgnoreMeUwU2;
    public Vector3 vecTest;
    public Vector3 vecTest2;

    public CatchEnnemyOnDraw _catchEnnemy;

    public List<Vector3> RecenterAndRotate(List<Vector3> points)
    {
        Vector3 centroid = GetDrawCentroid(points);

        List<Vector3> recenterDraw = new List<Vector3>();

        foreach (Vector3 point in points)
        {
            Vector3 newPoint = point - centroid;

            newPoint = Quaternion.Euler(-45, 0, 0) * newPoint;

            newPoint = Quaternion.Euler(0, 0, 180) * newPoint;

            recenterDraw.Add(newPoint);
        }

        return recenterDraw;
    }

    public Vector3 GetDrawCentroid(List<Vector3> points)
    {
        Vector3 sum = Vector3.zero;

        foreach (Vector3 point in points)
        {
            sum += point;
        }

        Vector3 centroid = sum / points.Count;

        return centroid;
    }

    public Vector3 GetDrawCenter(List<Vector3> points)
    {
        float minX = points[0].x;
        float maxX = points[0].x;

        float minY = points[0].y;
        float maxY = points[0].y;

        float minZ = points[0].z;
        float maxZ = points[0].z;

        foreach (Vector3 point in points)
        {
            minX = point.x < minX ? point.x : minX;
            maxX = point.x > maxX ? point.x : maxX;

            minY = point.y < minY ? point.y : minY;
            maxY = point.y > maxY ? point.y : maxY;

            minZ = point.z < minZ ? point.z : minZ;
            maxZ = point.z > maxZ ? point.z : maxZ;
        }

        float x = (maxX + minX) / 2;
        float y = (maxY + minY) / 2;
        float z = (maxZ + minZ) / 2;

        return new Vector3(x, y, z);
    }

    public Vector2 GetDrawDim(List<Vector3> points)
    {
        float minX = points[0].x;
        float maxX = points[0].x;

        float minY = points[0].y;
        float maxY = points[0].y;

        float minZ = points[0].z;
        float maxZ = points[0].z;

        foreach (Vector3 point in points)
        {
            minX = point.x < minX ? point.x : minX;
            maxX = point.x > maxX ? point.x : maxX;

            minY = point.y < minY ? point.y : minY;
            maxY = point.y > maxY ? point.y : maxY;

            minZ = point.z < minZ ? point.z : minZ;
            maxZ = point.z > maxZ ? point.z : maxZ;
        }

        /*Debug.Log($"MinX : {minX}, MaxX : {maxX}, minY : {minY}, maxY : {maxY}");
        Debug.Log($"distance X : {maxX - minX}, distance Y : {maxY - minY}");*/

        float X = minX >= 0 & maxX >= 0 ? minX : maxX;

        return new(maxX - minX, maxY - minY);
    }

    public void GetSpellTargetPointFromCentroid(List<Vector3> points)
    {
        Vector3 centroid = GetDrawCentroid(points);

        Ray Ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(centroid));
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit, 200f, ~IgnoreMeUwU))
        {
            //Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Ground"))
            {
                //Debug.Log($"Spell cast location from centroid : {hit.point}");
                CubeCentroid.transform.position = hit.point;
            }
        }
    }

    public Vector3 GetSpellTargetPointFromCenter(List<Vector3> points)
    {
        Vector3 center = GetDrawCenter(points);

        Ray Ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(center));
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit, 200f, ~IgnoreMeUwU))
        {
            //Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Ground"))
            {
                //Debug.Log($"Spell cast location from center : {hit.point}");
                CubeCentre.transform.position = hit.point;
                return hit.point;
            }

            Debug.LogError("No Ground Hit");
            return Vector3.zero;
        }

        Debug.LogError("No Object Hit");
        return Vector3.zero;
    }

    public List<Point> Vec3ToPoints(List<Vector3> list)
    {
        List<Point> listPoint = new List<Point>();

        foreach (Vector3 point in list)
        {
            Point newPoint = new Point(point.x, point.y, 1);
            listPoint.Add(newPoint);
        }

        return listPoint;
    }

    public void TryMakeAdaptativeCollider(List<Vector3> points, Vector3 center, Result result)
    {
        GameObject collider = new();
        collider.transform.position = GetSpellTargetPointFromCenter(points);

        switch (result.GestureClass)
        {
            case "Circle":
                collider.AddComponent<SphereCollider>();
                SphereCollider sphereColliderComponent;
                collider.TryGetComponent(out sphereColliderComponent);
                sphereColliderComponent.isTrigger = true;

                Vector2 drawDim = GetDrawDim(points);

                sphereColliderComponent.radius = (drawDim.x >= drawDim.y ? drawDim.x : drawDim.y) * 1.5f;

                SimpleDash fireBall = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
                SkillContext context = new(PlayerMain.Instance.Rigidbody2D, PlayerMain.Instance.gameObject, PlayerMain.Instance.transform.forward, 4);
                fireBall.Activate(context);
                //SpellManager.Instance.Spells["FireBall;Circle;E50037"].Activate(new(PlayerMain.Instance.Rigidbody, PlayerMain.Instance.gameObject, PlayerMain.Instance.transform.forward, 4));

                break;
            case "Square":
                collider.AddComponent<BoxCollider>();
                BoxCollider boxColliderComponent;
                collider.TryGetComponent(out boxColliderComponent);

                Vector3 cameraForward = Cam.transform.forward;
                Vector3 cameraRight = Cam.transform.right;

                Vector3 toTarget = (collider.transform.position - Cam.transform.position).normalized;

                float signedAngle = Vector3.SignedAngle(cameraForward, toTarget, Vector3.up);
                float signedAngleUp = Vector3.SignedAngle(cameraRight, toTarget, Vector3.forward);

                //Debug.Log($"Angle : {signedAngle}, AngleUp {signedAngleUp}");

                collider.transform.rotation = Quaternion.Euler(new Vector3(signedAngleUp + 90, 0, signedAngle));

                boxColliderComponent.isTrigger = true;

                Vector2 dim = GetDrawDim(points);

                Vector3 size = new(dim.x, Mathf.Abs(center.y), dim.y);

                //Debug.Log($"Centre : {center}, Size : {size}");

                boxColliderComponent.size = size;


                break;
            case "DiagoU" or "DiagoD" or "LineH" or "LineV":
                Debug.Log("LE DESSIN C'EST UNE LIGNE, ATTAQUE NOOPY ATTAQUE");

                fireBall = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
                context = new(PlayerMain.Instance.Rigidbody2D, PlayerMain.Instance.gameObject, PlayerMain.Instance.transform.forward, 4);
                fireBall.Activate(context);

                if (_catchEnnemy._ennemyObjectOnDraw.Count > 0)
                {
                    foreach (GameObject ennemy in _catchEnnemy._ennemyObjectOnDraw)
                    {
                        Debug.Log(ennemy.transform.position);
                    }
                }

                _catchEnnemy._ennemyObjectOnDraw.Clear();
                break;
        }
    }
}