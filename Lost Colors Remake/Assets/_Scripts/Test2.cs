using TMPro;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject target;

    private void Update()
    {
        text.text = target.transform.position.ToString();
    }
}
