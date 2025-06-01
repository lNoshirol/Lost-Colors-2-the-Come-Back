using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class SpriteShaderMaterialCreatorWindow : EditorWindow
{
    public static string ArtFolderPath = "Assets/_ART/";

    [MenuItem("Tools/Generate Props Sprite Atlas as Scriptable")]
    public static void GenerateSpriteAtlas()
    {
        Dictionary<string, string> colored = new();
        Dictionary<string, string> bw = new();

        string[] files = Directory.GetFiles(ArtFolderPath, "*.png", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            // ajouter Player ptêt ? ou Animals, Character...
            if (!(file.Contains("Environnement"))) continue;

            string name = Path.GetFileNameWithoutExtension(file);
            string baseName = name.Replace("_BW", "");

            // Assigne le fichier à un dict
            if (name.EndsWith("_BW"))
            {
                // BW
                bw[baseName] = file;
            }
            else
            {
                // Coloré
                colored[baseName] = file;
            }
        }

        // TODO APRES SPORT : TRANSFORMER SCRIPTABLE EN DICT ON RUNTIME ET LUTILISER POUR TRANSFORMER LES SPRITES

        foreach (var kvp in colored)
        {
            if (bw.ContainsKey(kvp.Key))
            {
                Debug.Log($"Pair trouvée ! {kvp.Key}, BW : {bw[kvp.Key]} avec Couleur : {kvp.Value}");
            }
            else
                Debug.LogWarning($"Pas de version BW pour {kvp.Key}");
        }

        var scriptable = ScriptableObject.CreateInstance<PropsTextureDatabase>();
        foreach (var kvp in colored)
        {
            var key = kvp.Key;
            if (!bw.TryGetValue(key, out string bwPath)) continue;

            var colorSprite = AssetDatabase.LoadAssetAtPath<Sprite>(kvp.Value);
            var bwSprite = AssetDatabase.LoadAssetAtPath<Sprite>(bwPath);

            PropSpritePair newSpritePair = new();
            newSpritePair.PropName = key;
            newSpritePair.Colored = colorSprite;
            newSpritePair.BW = bwSprite;

            scriptable.Props.Add(newSpritePair);
        }

        AssetDatabase.CreateAsset(scriptable, "Assets/_Scripts/_DATA/PropsTextures/PropsTextureDatabase.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = scriptable;
        Debug.Log($"ScriptableObject créé avec {scriptable.Props.Count} paires !");
    }
}