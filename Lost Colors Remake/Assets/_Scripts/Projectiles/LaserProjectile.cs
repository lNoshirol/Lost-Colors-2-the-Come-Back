using UnityEngine;

public class LaserProjectile : Projectile
{
    Pool pool;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerMain player = collision.gameObject.TryGetComponent(out PlayerMain playerD) ? playerD : null;
            player.Health.PlayerHealthChange(_projectileDatas.Damage);
        }
    }
}
