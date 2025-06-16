using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : MonoBehaviour
{
    public bool IsColorized;
    [field: SerializeField] public TileMapCorruptionWaveHandler ColorWaveHandler;

    [SerializeField]
    private List<EnemyMain> _list;

    void Start()
    {
        //TryGetComponent(out EnigmeSolvedCrystal enigmeSolvedCrystal);
        //NEED TO FINISH
        //enigmeSolvedCrystal.IsColorized += CrystalSave;

        foreach (GameObject p in EnemyManager.Instance.CurrentEnemyList)
        {
            p.TryGetComponent(out EnemyMain enemy);
            _list.Add(enemy);
        }
        // Ligne ci dessous � ne pas retirer pour le lerp des props, merci
        CrystalManager.Instance.AddToDict(this, gameObject.name, false, SceneManager.GetActiveScene().name);
    }
}
