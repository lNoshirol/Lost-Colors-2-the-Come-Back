using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    // Singleton
    #region Singleton
    private static SaveSystem _instance;

    public static SaveSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SaveSystem");
                _instance = go.AddComponent<SaveSystem>();
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


    XmlWriter xmlWriter;
    XmlWriterSettings xml = new XmlWriterSettings
    {
        NewLineOnAttributes = true,
        Indent = true,
    };

    private void Start()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "savefile.xml");

        if (!File.Exists(savePath))
        {
            using (XmlWriter writer = XmlWriter.Create(savePath, xml))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Data");
                writer.WriteElementString("SceneName", "FR_SP_01_Clean");
                writer.WriteElementString("PlayerHealth", "");
                writer.WriteStartElement("PlayerPosition");
                writer.WriteElementString("x", "0");
                writer.WriteElementString("y", "0");
                writer.WriteElementString("z", "0");
                writer.WriteEndElement();
                writer.WriteElementString("PlayerInventory", "");
                writer.WriteStartElement("CrystalsList");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }

    public void SaveData()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "savefile.xml");

        using (XmlWriter writer = XmlWriter.Create(savePath, xml))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Data");

            WriteXML(writer, "SceneName", SceneManager.GetActiveScene().name);
            WriteXML(writer, "PlayerHealth", playerMain.Health.playerActualHealth.ToString());
            var world = WorldMain.Instance;
            WriteVector3(writer, "PlayerPosition", world.FindCorrectSpawn(world.CurrentRoomName).transform.GetChild(0).transform.position);
            WriteXML(writer, "PlayerInventory", playerMain.Inventory.ItemDatabase[0].ToString());
            WriteCrystalsList(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    public void LoadData()
    {
        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists(Path.Combine(Application.persistentDataPath, "savefile.xml"))) return;
        saveFile.LoadXml(System.IO.File.ReadAllText(Path.Combine(Application.persistentDataPath, "savefile.xml")));

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            switch (key)
            {
                case "PlayerHealth":
                    playerMain.Health.playerActualHealth = int.Parse(value);
                    playerMain.UI.UpdatePlayerHealthUI();
                    break;
                case "PlayerPosition":
                    float x = float.Parse(node["x"].InnerText);
                    float y = float.Parse(node["y"].InnerText);
                    float z = float.Parse(node["z"].InnerText);
                    Vector3 loadedPosition = new Vector3(x, y, z);
                    playerMain.transform.position = loadedPosition;
                    break;

                case "PlayerInventory":
                    playerMain.Inventory.ItemDatabase[0] = bool.Parse(value);
                    break;

                case "CrystalsList":
                    var loadedList = LoadCrystalsList(node);
                    CrystalManager.Instance.LoadList(loadedList);
                    break;

            }
        }
    }

    static void WriteXML(XmlWriter writer, string key, string value)
    {
        writer.WriteStartElement(key);
        writer.WriteString(value);
        writer.WriteEndElement();
    }

    static void WriteVector3(XmlWriter writer, string key, Vector3 position)
    {
        writer.WriteStartElement(key);

        writer.WriteElementString("x", position.x.ToString());
        writer.WriteElementString("y", position.y.ToString());
        writer.WriteElementString("z", position.z.ToString());

        writer.WriteEndElement();
    }

    private void WriteCrystalsList(XmlWriter writer)
    {
        var crystalList = CrystalManager.Instance._ListCrystal;

        writer.WriteStartElement("CrystalsList");

        foreach (var crystal in crystalList)
        {
            writer.WriteStartElement("Crystal");
            writer.WriteElementString("Crystal", crystal.Crystal.name);
            writer.WriteElementString("IsColorized", crystal.IsColorized.ToString());
            writer.WriteElementString("WhichScene", crystal.WhichScene);
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    private List<CrystalManager.ListCrystal> LoadCrystalsList(XmlNode crystalsListNode)
    {
        List<CrystalManager.ListCrystal> loadedList = new();

        foreach (XmlNode crystalNode in crystalsListNode.ChildNodes)
        {
            string crystalName = crystalNode["Crystal"].InnerText;
            bool isColorized = bool.Parse(crystalNode["IsColorized"].InnerText);
            string whichScene = crystalNode["WhichScene"].InnerText;

            GameObject crystalGO = GameObject.Find(crystalName);
            Crystal crystalReference = null;

            if (crystalGO != null)
            {
                crystalReference = crystalGO.GetComponent<Crystal>();
            }

            CrystalManager.ListCrystal newCrystal = new CrystalManager.ListCrystal
            {
                Crystal = crystalReference,
                IsColorized = isColorized,
                WhichScene = whichScene
            };

            loadedList.Add(newCrystal);
        }

        return loadedList;
    }

    public string GetLastSavedScene()
    {
        XmlDocument saveFile = new XmlDocument();
        saveFile.LoadXml(System.IO.File.ReadAllText(Path.Combine(Application.persistentDataPath, "savefile.xml")));

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            if (key == "SceneName")
            {
                _lastScene = value;
            }
        }

        return _lastScene;
    }

    public void SetPlayerLastPosition()
    {
        XmlDocument saveFile = new XmlDocument();
        saveFile.LoadXml(System.IO.File.ReadAllText(Path.Combine(Application.persistentDataPath, "savefile.xml")));

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            if (key == "PlayerPosition")
            {
                float x = float.Parse(node["x"].InnerText);
                float y = float.Parse(node["y"].InnerText);
                float z = float.Parse(node["z"].InnerText);
                Vector3 loadedPosition = new Vector3(x, y, z);
                playerMain.transform.position = loadedPosition;
            }
        }
    }

}
