using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CrystalMain : EnemiesMain
{
    [Header("Magic Crystal Components")]
    [SerializeField] private Transform firePoint;

    public Vector2[] LazerCardinals;
    public Vector2[] LazerSubCardinals;

    private Vector2[] vector2s;

    public GameObject GameObjectToStorePool;

    public Pool pool;

    //private bool HasStartAttckedPlayer;

    public bool ChangeList;

    //private bool isShooting;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Health = GetComponent<EnemyHealth>();
        UI = GetComponent<EnemyUI>();
        SetupStats();
    }
    public override void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject, isColorized);
        DisplayGoodUI();

        //pool = new Pool(projectile, 20, GameObjectToStorePool.transform);
        vector2s = LazerCardinals;
    }

    public override void Update()
    {
        //if (isColorized || isShooting) return;

        //bool playerInRange = CheckPlayerInAttackRange();

        //if (playerInRange && !HasStartAttckedPlayer)
        //{
        //    HasStartAttckedPlayer = true;

        //    vector2s = ChangeList ? LazerCardinals : LazerSubCardinals;

        //    StartCoroutine(ShootInAllDirections(vector2s));
        //}
        //else if (!playerInRange)
        //{
        //    HasStartAttckedPlayer = false;
        //}
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
                Armor.AddGlyph();
            }
        }
    }

    public override void ColorSwitch()
    {
        spriteRenderer.material.DOFloat(1f, "_Transition", 2.5f).SetEase(Ease.OutQuad);
        isColorized = true;
    }

    //private IEnumerator ShootInAllDirections(Vector2[] directions)
    //{
    //    isShooting = true;

    //    foreach (var direction in directions)
    //    {
    //        ShootLaser(direction);
    //    }

    //    yield return new WaitForSeconds(2f);

    //    isShooting = false;

    //    HasStartAttckedPlayer = false;
    //    ChangeList = !ChangeList;
    //}

    //private async void ShootLaser(Vector2 direction)
    //{
    //    GameObject laser = pool.GetObject();
    //    laser.transform.position = firePoint.position;

    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    laser.transform.rotation = Quaternion.Euler(0, 0, angle);

    //    await Task.Delay(2000);

    //    pool.Stock(laser);
    //}

}
