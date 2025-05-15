using DG.Tweening;
using UnityEngine;

public class EnemyColoration : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GetComponent<SpriteRenderer>().sharedMaterial.DOFloat(0, "_Transition", 0);

            GetComponent<SpriteRenderer>().sharedMaterial.DOFloat(0.1f, "_Transition", 1.2f).SetEase(Ease.OutQuad);
        }
    }
}
