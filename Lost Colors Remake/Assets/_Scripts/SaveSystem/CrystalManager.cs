using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;

public class CrystalManager : MonoBehaviour
{
    // Singleton
    #region Singleton
    private static CrystalManager _instance;

    public static CrystalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CrystalManager");
                _instance = go.AddComponent<CrystalManager>();
                Debug.Log("<color=#8b59f0>Projectile Manager</color> instance <color=#58ed7d>created</color>");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        Debug.LogWarning(gameObject.name);
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("<color=#8b59f0>CrystalManager</color> instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
        }
    }
    #endregion


    #region Dictionnaire
    [Serializable]
    public class ListCrystal
    {
        [field: SerializeField]
        public CrystalMain CrystalName{ get; set; }

        [field: SerializeField]
        public bool IsColorized{ get; set; }

        [field: SerializeField]
        public string WhichScene {  get; set; }
    }

    public List<ListCrystal> _ListCrystal = new();

    #endregion

    public void AddToDict(CrystalMain crystal, bool isColorized, string whichScene)
    {
        bool ok = _ListCrystal.Any(item=> item.CrystalName == crystal && item.IsColorized==isColorized && item.WhichScene == whichScene);

        if (!ok)
        {
            var newListCrystal = new ListCrystal
            {
                CrystalName = crystal,
                IsColorized = isColorized,
                WhichScene = whichScene,
            };

            if (_ListCrystal.Contains(newListCrystal)) { }
            else
            {
                _ListCrystal.Add(newListCrystal);
            }
        }


    }

    public void Check(CrystalMain crystal, bool isColorized, string whichScene)
    {
        var newListCrystal = new ListCrystal
        {
            CrystalName = crystal,
            IsColorized = isColorized,
            WhichScene = whichScene,
        };

        if (!_ListCrystal.Contains(newListCrystal)) _ListCrystal.Add(newListCrystal);
    }

    public void UpdateIsColorizedIfChanged(CrystalMain crystal, bool newIsColorized, string whichScene)
    {
        var newListCrystal = new ListCrystal
        {
            CrystalName = crystal,
            IsColorized = newIsColorized,
            WhichScene = whichScene,
        };

        for (int i =0;  i < _ListCrystal.Count; i++)
        {
            if (_ListCrystal[i].CrystalName == newListCrystal.CrystalName && _ListCrystal[i].WhichScene == newListCrystal.WhichScene)
            {
                Debug.Log($"IsColorized a changé pour {newListCrystal.WhichScene} : {newListCrystal.IsColorized} -> {newIsColorized}");
                _ListCrystal[i].IsColorized = newIsColorized;
            }
        }
    }
}
