using TMPro;
using UnityEngine;

public class DebugPos : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = PlayerMain.Instance.gameObject.transform.position.ToString();
    }
}
