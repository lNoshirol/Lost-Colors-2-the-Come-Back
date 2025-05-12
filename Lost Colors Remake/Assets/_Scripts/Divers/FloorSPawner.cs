using UnityEngine;

public class CorruptedFloorSpawner : MonoBehaviour
{
    public GameObject corruptedPlanePrefab; // prefab du sol corrompu
    public int numberOfPatches = 10000;
    public Vector2 floorSize = new Vector2(150, 150); // dimensions du sol principal

    void Start()
    {
        for (int i = 0; i < numberOfPatches; i++)
        {
            SpawnCorruptedPatch();
        }
    }

    void SpawnCorruptedPatch()
    {
        float x = Random.Range(-floorSize.x / 2, floorSize.x / 2);
        float z = Random.Range(-floorSize.y / 2, floorSize.y / 2);
        Vector3 position = new Vector3(x, 0.01f, z); // légèrement au-dessus du sol

        GameObject patch = Instantiate(corruptedPlanePrefab, position, Quaternion.identity, transform);

        int angle = Random.Range(0, 4) * 90; // 0, 90, 180, 270
        patch.transform.rotation = Quaternion.Euler(-90, angle, 0);
    }
}
