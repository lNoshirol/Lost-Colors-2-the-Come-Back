using UnityEditor;
using UnityEngine;

public class SpriteShaderMaterialCreatorWindow : EditorWindow
{
    [MenuItem("Window/ScriptableObjectsMaker")]
    public static void ShowWindow()
    {
        GetWindow<SpriteShaderMaterialCreatorWindow>("Scriptable Objects Maker");
    }

    private void OnGUI()
    {

    }
}
