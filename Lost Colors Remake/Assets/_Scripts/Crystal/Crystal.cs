using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class Crystal : MonoBehaviour
{
    public bool IsColorized;
    [field: SerializeField] public TileMapCorruptionWaveHandler ColorWaveHandler;

    [SerializeField]
    private List<EnemyMain> _list;

    void Start()
    {
        TryGetComponent(out EnigmeSolvedCrystal enigmeSolvedCrystal);
        enigmeSolvedCrystal.ISColorized += LOL;

        foreach(GameObject p in EnemyManager.Instance.CurrentEnemyList)
        {
            p.TryGetComponent(out EnemyMain enemy);
            _list.Add(enemy);
        }
    }
    
    public void LOL()
    {
        CrystalManager.Instance.AddToDict(this, name, true, SceneManager.GetActiveScene().name);
        SaveSystem.Instance.SaveCrystalWhenIsColorized();
    }
}
