using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteShaderMaterialCreatorWindow : EditorWindow
{
    public string ArtFolderPath = "Assets/_ART/";

    [MenuItem("Window/SpriteShaderMaterialCreatorWindow")]
    public static void ShowWindow()
    {
        GetWindow<SpriteShaderMaterialCreatorWindow>("Scriptable Objects Maker");
    }

    private void OnEnable()
    {
        GetSprites();
    }

    private void OnGUI()
    {
        //foreach (var path in AssetDatabase.GetSubFolders("Assets/_ART"))
        //{
        //    DirectoryInfo dir = new DirectoryInfo(path);
        //    FileInfo[] infos = dir.GetFiles(path);

        //    foreach (var info in infos) 
        //    {
        //        GUI.Label(new Rect(300, 500, 50, 20), info.FullName);
        //    }

        //}


    }

    public void GetSprites()
    {
        string[] files = Directory.GetFiles(ArtFolderPath, "*.png", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            // ajouter player ptêt ?
            if(file.Contains("Animals") | file.Contains("Characters") | file.Contains("Environnement"))
            {

            }
        }
    }
}
