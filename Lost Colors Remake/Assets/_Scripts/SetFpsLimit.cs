using TMPro;
using UnityEngine;

public class SetFpsLimit : MonoBehaviour
{
    public int targetedFps;
    public TextMeshProUGUI text;

    private void Awake()
    {
        Application.targetFrameRate = targetedFps;
        text.text = targetedFps.ToString() + " fps targeted";
        
    }
}
