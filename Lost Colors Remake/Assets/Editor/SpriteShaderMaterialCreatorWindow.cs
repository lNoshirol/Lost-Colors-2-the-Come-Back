using UnityEditor;
using UnityEngine;

public class SpriteShaderMaterialCreatorWindow : EditorWindow
{
    public string ArtFolderPath = "Assets/_ART/";
    [MenuItem("Window/ScriptableObjectsMaker")]
    public static void ShowWindow()
    {
        GetWindow<SpriteShaderMaterialCreatorWindow>("Scriptable Objects Maker");
    }

    private void OnGUI()
    {

    }
}
