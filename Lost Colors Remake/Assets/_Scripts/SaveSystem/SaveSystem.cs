using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerMain playerMain;


    XmlWriter xmlWriter;
    XmlWriterSettings xml = new XmlWriterSettings
    {
        NewLineOnAttributes = true,
        Indent = true,
    };

    public void ToSave()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "savefile.xml");

        xmlWriter = XmlWriter.Create(savePath, xml);

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("Data");
        WriteXML(xmlWriter, "PlayerHealth", playerMain.Health.playerActualHealth.ToString() );
        WriteVector3(xmlWriter, "PlayerPosition", playerMain.gameObject.transform.position);
        WriteXML(xmlWriter, "PlayerInventory", playerMain.Inventory.ItemDatabase[0].ToString());
        WriteCrystalsList(xmlWriter);


        xmlWriter.Close();
    }

    private void Start()
    {
        XmlDocument saveFile = new XmlDocument();

        if (!System.IO.File.Exists(Path.Combine(Application.persistentDataPath, "savefile.xml")))
        {
            string savePath = Path.Combine(Application.persistentDataPath, "savefile.xml");
            xmlWriter = xmlWriter = XmlWriter.Create(savePath, xml); ;
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Data");
            xmlWriter.WriteStartElement("PlayerHealth");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("PlayerPosition");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("PlayerInventory");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.WriteStartElement("CrystalsList");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }

    public void LoadSave()
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
                if (crystalReference == null)
                {
                    Debug.LogWarning($"Le GameObject '{crystalName}' n'a pas de composant CrystalMain.");
                }
            }
            else
            {
                Debug.LogWarning($"Le GameObject '{crystalName}' est introuvable dans la scène.");
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


}
