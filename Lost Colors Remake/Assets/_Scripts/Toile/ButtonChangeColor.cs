using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeColor : MonoBehaviour
{
    private CastSpriteShape _spriteShape;

    private void Start()
    {
        _spriteShape = CastSpriteShape.instance;
    }

    public void OnClick()
    {
        TryGetComponent(out Image image);


        //_spriteShape.ChangeColor(image.color);

        Debug.Log(ColorUtility.ToHtmlStringRGB(image.color));
    }
}