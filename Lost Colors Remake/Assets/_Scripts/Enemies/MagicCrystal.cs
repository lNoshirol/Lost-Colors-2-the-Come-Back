using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MagicCrystal : EnemiesMain
{
    [Header("Magic Crystal Components")]
    [SerializeField]
    private Transform firePoint;

    public Vector2[] LazerCardinals;
    public Vector2[] LazerSubCardinals;
    private Vector2[] vector2s;

    public GameObject GameObjectToStorePool;

    public Pool pool;

    private bool HasStartAttckedPlayer;

    public bool ChangeList;

    public override void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject, isColorized);

        DisplayGoodUI();

        pool = new Pool(projectile, 12, GameObjectToStorePool.transform);

        vector2s = LazerCardinals;
    }

    public override void Update()
    {
        if (CheckPlayerInAttackRange() && !HasStartAttckedPlayer && !isColorized)
        {
            HasStartAttckedPlayer = true;

            if (ChangeList)
            {
                for (int i = 0; i < LazerCardinals.Length; i++)
                {
                    Vector2 setting = LazerCardinals[i];
                    ShootEncore(setting);
                }
            }
            else
            {
                for (int i = 0; i < LazerSubCardinals.Length; i++)
                {
                    Vector2 setting = LazerSubCardinals[i];
                    ShootEncore(setting);
                }
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

    async void ShootEncore(Vector2 direction)
    {
        GameObject laser = pool.GetObject();
        laser.transform.position = firePoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, angle);

        await Task.Delay(2000);

        pool.Stock(laser);
        HasStartAttckedPlayer = false;
        
    }
}
