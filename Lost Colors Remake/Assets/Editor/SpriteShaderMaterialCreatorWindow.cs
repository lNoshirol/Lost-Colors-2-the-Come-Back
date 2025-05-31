using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class SpriteShaderMaterialCreatorWindow : EditorWindow
{
    public string ArtFolderPath = "Assets/_ART/";

    private List<string> _assetPath = new();

    [MenuItem("Window/SpriteShaderMaterialCreatorWindow")]
    public static void ShowWindow()
    {
        GetWindow<SpriteShaderMaterialCreatorWindow>("Scriptable Objects Maker");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Generate", GUILayout.Width(100), GUILayout.Height(25)))
        {
            GetSprites();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    public void GetSprites()
    {
        // BW to Colored
        Dictionary<string, string> sprites = new();

        string[] files = Directory.GetFiles(ArtFolderPath, "*.png", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            // ajouter Player ptêt ? ou Animals, Character...
            if (!(file.Contains("Environnement"))) continue;

            string fileName = Path.GetFileNameWithoutExtension(file);
            string baseSpriteName = fileName.Replace("_BW", "");

            Debug.Log(baseSpriteName);

            // On ajoute une valeur nulle si la clé n'est pas déjà dans le dico
            if (!sprites.ContainsKey(baseSpriteName)) sprites[baseSpriteName] = null;

            if (fileName.EndsWith("_BW") && !sprites.ContainsKey(baseSpriteName))
            {
                // On remplace que si aucune version a encore été trouvée
                if (!sprites.ContainsKey(baseSpriteName)) sprites[baseSpriteName] = file;
            }
            else
            {
                // On reset pour prioriser la version colorée
                sprites[baseSpriteName] = file;
            }
        }

        foreach (string spriteName in sprites.Keys)
        {
            PropsTexturePair newAsset = ScriptableObject.CreateInstance<PropsTexturePair>();
            newAsset.name = spriteName;
            newAsset.ColoredTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(sprites[spriteName]);
            AssetDatabase.CreateAsset(newAsset, $"Assets/_Scripts/_DATA/PropsTextures/{spriteName}.asset");
            Debug.Log((sprites[spriteName]));
        }
        AssetDatabase.SaveAssets();
    }
}