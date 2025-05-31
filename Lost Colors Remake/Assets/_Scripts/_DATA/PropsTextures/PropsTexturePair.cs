using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "PropsTexturePair", menuName = "Scriptable Objects/PropsTexturePair")]
public class PropsTexturePair : ScriptableObject
{
    public string PropName;
    [ShowAssetPreview(64, 64)]
    public Texture2D BWTexture;
    [ShowAssetPreview(64, 64)]
    public Texture2D ColoredTexture;
}
