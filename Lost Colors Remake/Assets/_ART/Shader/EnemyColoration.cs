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
            GetComponent<SpriteRenderer>().material.DOFloat(0, "_Transition", 0);

            GetComponent<SpriteRenderer>().material.DOFloat(1f, "_Transition", 2.5f).SetEase(Ease.OutQuad);
        }
    }

}
