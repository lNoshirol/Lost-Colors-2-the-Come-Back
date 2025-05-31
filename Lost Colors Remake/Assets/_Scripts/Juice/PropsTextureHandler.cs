using System.Collections.Generic;
using UnityEngine;

public class PropsTextureHandler : MonoBehaviour
{
    public Dictionary<string, Texture> PropsColoredTextures { get; set; } = new();

    private void Start()
    {
        LoadTextures();
    }

    private void LoadTextures()
    {
        Texture2D[] loaded = Resources.LoadAll<Texture2D>("Environnement");
        if(loaded.Length ==0)
        {
            print("too bad");
            return;
        }
        foreach (Texture2D tex in loaded)
        {
           
            print(tex);
        }
    }
}
