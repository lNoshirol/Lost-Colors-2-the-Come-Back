using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerMain playerMain;

    private Vector3 LastPostionPlayer;


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

}
