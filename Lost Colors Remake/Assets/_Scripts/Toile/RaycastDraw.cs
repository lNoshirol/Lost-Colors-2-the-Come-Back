using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using NaughtyAttributes.Test;
using System.Drawing;
using Unity.VisualScripting;
using System.Linq;

public class RaycastDraw : MonoBehaviour
{
    [Header("Settings")]
    public Camera mainCamera;
    public LayerMask raycastLayerMask;

    //private Vector3[] vertices = new Vector3[4];
    //private int triangles = new int[6];

    [Header("Draw Data")]
    public List<Vector2> points2D = new();
    public List<Vector3> points3D = new();

    private GameObject meshObject;
    private Mesh mesh;

    [SerializeField] private CastSpriteShape _pen;
    [SerializeField] private MeshFilter _filter;

    //test
    Vector3[] pointsDebug;
    Vector3 _pointCenter;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        mesh = new Mesh { name = "Procedural Shape" };

        meshObject = new GameObject("ProceduralShape", typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider));
        meshObject.GetComponent<MeshFilter>().mesh = mesh;
        meshObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void DrawRayCastInRealTime()
    {
        Vector2 screenPoint;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPoint = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenPoint = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        points2D.Add(screenPoint);

        Vector3 worldPoint = ConvertToWorldSpaceWithRaycast(screenPoint);
        if (worldPoint != Vector3.zero)
        {
            points3D.Add(worldPoint);
        }

        DebugRaycastLines();
    }

    private Vector3 ConvertToWorldSpaceWithRaycast(Vector2 screenPoint)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayerMask))
        {
            return hit.point;
        }

        return ray.GetPoint(10f);
    }

    public void DebugRaycastLines()
    {
        if (points3D.Count < 2) return;

        for (int i = 0; i < points3D.Count - 1; i++)
        {
            Debug.DrawLine(points3D[i], points3D[i + 1], UnityEngine.Color.red);
        }
        if (points3D.Count > 0)
        {
            for (int i = 0; i < points3D.Count - 1; i++)
            {
                Debug.DrawLine(points3D[i], points3D[i + 1], UnityEngine.Color.red);
            }

            Debug.DrawLine(points3D[points3D.Count - 1], points3D[0], UnityEngine.Color.red);
        }
    }

    public void GenerateSpellShapeMesh()
    {
        mesh.Clear();
        
        points3D = points3D.Distinct().ToList();

        _pointCenter = Vector3.zero;
        foreach(Vector3 point in points3D)
        {
            _pointCenter += point;
        }
        _pointCenter /= points3D.Count;

        List<Vector3> vertices = new();

        // Set vertices
        // The first is the center of the shape, the rest is all its other points
        vertices.Add(_pointCenter);
        vertices.AddRange(points3D);

        pointsDebug = vertices.ToArray();

        List<Vector2> uvs = new();
        List<Vector4> tangents = new();
        List<int> triangless = new List<int>();

        for (int i = 1; i < vertices.Count - 1; i += 1)
        {
            triangless.Add(0);
            triangless.Add(i);
            triangless.Add(i + 1);
        }

        triangless.Add(0);
        triangless.Add(vertices.Count - 1);
        triangless.Add(1);

        mesh.Clear(); 
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangless.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Mesh collider update
        MeshCollider mc = meshObject.gameObject.GetComponent<MeshCollider>();
        if (mc != null)
        {
            mc.sharedMesh = null;
            mc.sharedMesh = mesh;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.cyan;
        Gizmos.DrawSphere(_pointCenter, 0.3f);

        foreach (var point in points3D)
        {
            Gizmos.color = UnityEngine.Color.black;
            Gizmos.DrawSphere(point, 0.2f);
        }


        if (pointsDebug != null)
        {
            Gizmos.color = UnityEngine.Color.red;
            foreach (var point in pointsDebug)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }
    }

    public void ClearRaycastLines()
    {
        points2D.Clear();
        points3D.Clear();
    }
}
