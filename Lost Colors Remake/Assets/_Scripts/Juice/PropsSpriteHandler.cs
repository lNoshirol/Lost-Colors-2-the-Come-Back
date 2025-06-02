using System.Collections.Generic;
using UnityEngine;

public class PropsSpriteHandler : MonoBehaviour
{
    [SerializeField] private PropsTextureDatabase _textureDatabase;

    public Dictionary<string, Sprite> PropsColoredTextures { get; set; } = new();

    // Singleton
    #region Singleton
    private static PropsSpriteHandler _instance;

    public static PropsSpriteHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Props Sprite Handler");
                _instance = go.AddComponent<PropsSpriteHandler>();
                Debug.Log("<color=#8b59f0>Props Sprite Handler</color> instance <color=#58ed7d>created</color>");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("<color=#8b59f0>Props Sprite Handler</color> instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        FillDictionaryFromDatabase();
    }

    private void FillDictionaryFromDatabase()
    {
        foreach (PropSpritePair pair in _textureDatabase.Props)
        {
            PropsColoredTextures[pair.PropName] = pair.Colored;
        }
    }
}
