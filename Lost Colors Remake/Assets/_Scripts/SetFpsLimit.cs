using TMPro;
using UnityEngine;

public class SetFpsLimit : MonoBehaviour
{
    public int targetedFps;
    public TextMeshProUGUI text;

    private void Awake()
    {
        Application.targetFrameRate = targetedFps;

        if (text != null)
        {
            text.text = targetedFps.ToString() + " fps targeted";
        }
        
    }
}