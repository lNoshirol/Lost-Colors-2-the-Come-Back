using UnityEngine;

public class PainterPointer : MonoBehaviour
{
    private Vector3 _pointedPosition;
    Color color;
    [SerializeField] private MeshRenderer _renderer;
    private Material material;

    private void Start()
    {
        material = _renderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // You successfully hi
        if (Physics.Raycast(ray, out hit))
        {
            color = Color.red;
            _pointedPosition = hit.point;
            material.SetVector("_Position", -hit.textureCoord - Vector2.one * 0.5f  );
            print($"{material.GetVector("_Position")}, ? {hit.textureCoord}");
        }
        else 
        {
            color = Color.white;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(_pointedPosition, 1f);
    }
}
