using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool collisionLocationAtPlayerRight = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 )
        {
            if (collision.gameObject.CompareTag("Torch")) return;
            GetCollisionLocationAtPlayer(collision.gameObject.transform);
            PlayerMain.Instance.Health.PlayerLoseHP(1f);

        }
    }

    private void GetCollisionLocationAtPlayer(Transform collision)
    {
        if(PlayerMain.Instance.gameObject.transform.position.x > collision.position.x)
            collisionLocationAtPlayerRight = false;
        else
            collisionLocationAtPlayerRight = true;
    }
}
