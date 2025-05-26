using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileDatas _projectileDatas;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = TryGetComponent(out Rigidbody2D rb) ? rb : null;
    }

    /// <summary>
    /// Launch the projectile in the direction of its forward vector, with its Datas speed
    /// </summary>
    public void Launch(SkillContext context)
    {
        float angle = Mathf.Atan2(context.Direction.y, context.Direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);
        _rb.AddForce(context.Direction * this._projectileDatas.Speed, ForceMode2D.Impulse);
        // Jouer SFX et VFX OnLaunch
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerMain player = collision.gameObject.TryGetComponent(out PlayerMain playerD) ? playerD : null;
            player.Health.PlayerLoseHP(1);
            gameObject.SetActive(false);
        }
    }
}
