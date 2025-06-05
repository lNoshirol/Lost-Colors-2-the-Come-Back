using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystemJson : MonoBehaviour
{
    // Singleton
    #region Singleton
    private static SaveSystemJson _instance;
    public static SaveSystemJson Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SaveSystemJson");
                _instance = go.AddComponent<SaveSystemJson>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField]
    private PlayerMain playerMain;

    private string _lastScene;

    private string _savePath => Path.Combine(Application.persistentDataPath, "savefile.json");

    [System.Serializable]
    public class SaveData
    {
        public string SceneName;
        public float PlayerHealth;
        public Vector3 PlayerPosition;
        public bool PlayerInventory;
        public List<CrystalData> CrystalsList = new();
    }

    [System.Serializable]
    public class CrystalData
    {
        public string Crystal;
        public bool IsColorized;
        public string WhichScene;
    }

    private void Start()
    {
        if (!File.Exists(_savePath))
        {
            SaveData initialData = new SaveData
            {
                SceneName = "FR_SP_01_Clean",
                PlayerHealth = 3,
                PlayerPosition = Vector3.zero,
                PlayerInventory = false,
                CrystalsList = new()
            };

            File.WriteAllText(_savePath, JsonUtility.ToJson(initialData, true));
        }
        Debug.Log(_savePath);
    }

    public void SaveDataToJson()
    {
        SaveData data = new SaveData
        {
            SceneName = SceneManager.GetActiveScene().name,
            PlayerHealth = playerMain.Health.playerActualHealth,
            PlayerPosition = WorldMain.Instance.FindCorrectSpawn(WorldMain.Instance.CurrentRoomName).transform.GetChild(0).position,
            PlayerInventory = playerMain.Inventory.ItemDatabase[0],
            CrystalsList = GetCrystalsList()
        };

        File.WriteAllText(_savePath, JsonUtility.ToJson(data, true));
    }

    public void LoadDataFromJson()
    {
        if (!File.Exists(_savePath)) return;

        SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_savePath));

        playerMain.Health.playerActualHealth = data.PlayerHealth;
        playerMain.UI.UpdatePlayerHealthUI();
        playerMain.transform.position = data.PlayerPosition;
        playerMain.Inventory.ItemDatabase[0] = data.PlayerInventory;
        CrystalManager.Instance.LoadList(LoadCrystalsList(data.CrystalsList));
    }

    private List<CrystalManager.ListCrystal> LoadCrystalsList(List<CrystalData> crystalsData)
    {
        List<CrystalManager.ListCrystal> loadedList = new();

        foreach (var crystalData in crystalsData)
        {
            GameObject crystalGO = GameObject.Find(crystalData.Crystal);
            Crystal crystalReference = crystalGO != null ? crystalGO.GetComponent<Crystal>() : null;

            loadedList.Add(new CrystalManager.ListCrystal
            {
                Crystal = crystalReference,
                IsColorized = crystalData.IsColorized,
                WhichScene = crystalData.WhichScene
            });
        }

        return loadedList;
    }

    private List<CrystalData> GetCrystalsList()
    {
        var list = new List<CrystalData>();

        foreach (var crystal in CrystalManager.Instance._ListCrystal)
        {
            list.Add(new CrystalData
            {
                Crystal = crystal.Crystal.name,
                IsColorized = crystal.IsColorized,
                WhichScene = crystal.WhichScene
            });
        }

        return list;
    }

    public string GetLastSavedScene()
    {
        if (!File.Exists(_savePath)) return "FR_SP_01_Clean";

        SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_savePath));
        _lastScene = data.SceneName;
        return _lastScene;
    }

    public void SetPlayerLastPosition()
    {
        if (!File.Exists(_savePath)) return;

        SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_savePath));
        playerMain.transform.position = data.PlayerPosition;
    }
}
