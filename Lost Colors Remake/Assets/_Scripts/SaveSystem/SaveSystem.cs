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

    string savePath;


    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savefile.xml");

        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        } 

        if (!File.Exists(savePath))
        {
            Debug.LogWarning("CreateXML");
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
                Debug.LogWarning("CreateXML" + savePath);
            }
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


    public void SaveData()
    {
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
        Debug.LogWarning("LoadXML" + savePath);

        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists(savePath)) return;
        saveFile.LoadXml(System.IO.File.ReadAllText(savePath));

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
            writer.WriteElementString("Crystal", crystal.CrystalName);
            writer.WriteElementString("Crystal", crystal.CrystalName);
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
            string crystal = crystalNode["Crystal"].InnerText;
            string crystalName = crystalNode["CrystalName"].InnerText;
            bool isColorized = bool.Parse(crystalNode["IsColorized"].InnerText);
            string whichScene = crystalNode["WhichScene"].InnerText;


            bool isInActiveScene = (whichScene == SceneManager.GetActiveScene().name);

            if (!isInActiveScene)
            {
                Debug.Log($"Cristal '{crystalName}' ignoré (appartient à la scène '{whichScene}', scène active : '{SceneManager.GetActiveScene().name}').");
                break;
            }

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
        saveFile.LoadXml(System.IO.File.ReadAllText(savePath));

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
        saveFile.LoadXml(System.IO.File.ReadAllText(savePath));

        string key;
        string value;
        Vector3 loadedPosition;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            if (key == "PlayerPosition")
            {
                float x = float.Parse(node["x"].InnerText);
                float y = float.Parse(node["y"].InnerText);
                float z = float.Parse(node["z"].InnerText);
                loadedPosition = new Vector3(x, y, z);
                playerMain.transform.position = loadedPosition;
            }
        }
    }

}
