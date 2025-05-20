using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MagicCrystal : EnemiesMain
{
    [Header("Magic Crystal Components")]
    [SerializeField]
    private Transform firePoint;

    public Vector2[] LazerCardinals;
    public Vector2[] LazerSubCardinals;

    public GameObject GameObjectToStorePool;

    public Pool pool;

    private bool HasStartAttckedPlayer;

    public override void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject, isColorized);

        DisplayGoodUI();

        pool = new Pool(projectile, 10, GameObjectToStorePool.transform);
    }

    public override void Update()
    {
        if (CheckPlayerInAttackRange() && !HasStartAttckedPlayer && !isColorized)
        {
            HasStartAttckedPlayer = true;
            for (int i = 0; i < LazerSubCardinals.Length; i++)
            {
                Vector2 setting = LazerSubCardinals[i];
                float delay = i * 0.5f;
                StartCoroutine(ShootInDirection(setting, delay));
            }
        }
        else if (!CheckPlayerInAttackRange())
        {
            HasStartAttckedPlayer = false;
        }
    }

    public override void DisplayGoodUI()
    {
        if (isColorized)
        {
            UI.SwitchHealtBar(false);
        }
        else
        {
            if (Stats.maxArmor == 0)
            {
                return;
            }
            else
            {
                UI.SwitchHealtBar(false);
            }
        }
    }

    public override void ColorSwitch()
    {
        spriteRenderer.material.DOFloat(1f, "_Transition", 2.5f).SetEase(Ease.OutQuad);
        isColorized = true;
    }

    IEnumerator ShootInDirection(Vector2 setting, float startDelay)
    {
        yield return new WaitForSeconds(startDelay * 2);

        while (!isColorized)
        {
            StartCoroutine(ShootLaser(setting));
            yield return new WaitForSeconds(2.25f);
        }
    }


    IEnumerator ShootLaser(Vector2 direction)
    {
        
        GameObject laser = pool.GetObject();
        laser.transform.position = firePoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(2f);
        pool.Stock(laser);
    }
}
