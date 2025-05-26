using System.IO;
using System.Xml;
using TMPro;
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
        WriteXML(xmlWriter, "PlayerPosition", playerMain.gameObject.transform.position.ToString());
        LastPostionPlayer = playerMain.gameObject.transform.position;
        xmlWriter.Close();
    }

    private void Start()
    {
        XmlDocument saveFile = new XmlDocument();

        if (!System.IO.File.Exists(Application.persistentDataPath + "savefile.xml"))
        {
            string savePath = Path.Combine(Application.persistentDataPath, "savefile.xml");
            xmlWriter = xmlWriter = XmlWriter.Create(savePath, xml); ;
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Data");
            xmlWriter.WriteStartElement("PlayerHealth");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("PlayerPosition");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }

    public void LoadSave()
    {
        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists(Application.dataPath + "/" + "ARGH" + ".xml")) return;
        saveFile.LoadXml(System.IO.File.ReadAllText(Application.dataPath + "/" + "ARGH" + ".xml"));

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            switch (key)
            {
                case "PlayerHealth":
                    break;
                case "PlayerPosition":
                    playerMain.gameObject.transform.position = LastPostionPlayer;
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
}
