using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 )
        {
            PlayerMain.Instance.Health.PlayerLoseHP(1f);
        }
    }
}
