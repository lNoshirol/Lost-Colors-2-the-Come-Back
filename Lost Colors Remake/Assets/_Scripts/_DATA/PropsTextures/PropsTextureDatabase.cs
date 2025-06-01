using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "PropsTexturePair", menuName = "Scriptable Objects/PropsTexturePair")]
public class PropsTextureDatabase : ScriptableObject
{
    public List<PropSpritePair> Props = new();
}

[System.Serializable]
public class PropSpritePair
{
    public string PropName;
    [ShowAssetPreview(64, 64)]
    public Sprite BW;
    [ShowAssetPreview(64, 64)]
    public Sprite Colored;
}
