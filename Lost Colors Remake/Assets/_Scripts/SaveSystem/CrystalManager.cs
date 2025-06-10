using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;

public class CrystalManager : SingletonCreatorPersistent<CrystalManager>
{
    #region Dictionnaire
    [Serializable]
    public class ListCrystal
    {
        [field: SerializeField]
        public Crystal Crystal{ get; set; }

        [field: SerializeField]
        public string CrystalName { get; set; }

        [field: SerializeField]
        public bool IsColorized{ get; set; }

        [field: SerializeField]
        public string WhichScene {  get; set; }
    }

    public List<ListCrystal> _ListCrystal = new();

    #endregion

    public void AddToDict(Crystal crystal, string name, bool isColorized, string whichScene)
    {
        bool _isInDict = _ListCrystal.Any(item=> item.Crystal == crystal && item.CrystalName == name && item.IsColorized == isColorized && item.WhichScene == whichScene);

        if (SceneManager.GetActiveScene().name != whichScene) return;

        if (!_isInDict)
        {
            var newListCrystal = new ListCrystal
            {
                Crystal = crystal,
                CrystalName = crystal.gameObject.name,
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

    public void Check(Crystal crystal, bool isColorized, string whichScene)
    {
        var newListCrystal = new ListCrystal
        {
            Crystal = crystal,
            IsColorized = isColorized,
            WhichScene = whichScene,
        };

        if (!_ListCrystal.Contains(newListCrystal)) _ListCrystal.Add(newListCrystal);
    }

    public void UpdateIsColorizedIfChanged(Crystal crystal, string name, bool newIsColorized, string whichScene)
    {
        var newListCrystal = new ListCrystal
        {
            Crystal = crystal,
            CrystalName = name,
            IsColorized = newIsColorized,
            WhichScene = whichScene,
        };

        for (int i =0;  i < _ListCrystal.Count; i++)
        {
            if (_ListCrystal[i].Crystal == newListCrystal.Crystal && _ListCrystal[i].WhichScene == newListCrystal.WhichScene)
            {
                _ListCrystal[i].IsColorized = newIsColorized;
            }
        }
    }

    public void LoadList(List<ListCrystal> loadedList)
    {
        _ListCrystal = loadedList;

        foreach (var crystal in _ListCrystal)
        {
            UpdateIsColorizedIfChanged(crystal.Crystal, crystal.CrystalName, crystal.IsColorized, crystal.WhichScene);
        }
    }

    public void ClearListCrystal()
    {
        _ListCrystal?.Clear();
    }

}
