using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private float _lineLength;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _lineLength = ExternalDrawFunctions.GetDrawLength(DrawForDollarP.instance.GetDrawPoints());

        }
    }
}
