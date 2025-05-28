using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalMain : EnemiesMain
{
    [Header("Magic CrystalName Components")]
    [SerializeField] private Transform firePoint;

    public Vector2[] LazerCardinals;
    public Vector2[] LazerSubCardinals;

    private Vector2[] vector2s;

    public GameObject GameObjectToStorePool;

    public Pool pool;

    public bool ChangeList;

    private string _nameScene;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Health = GetComponent<EnemyHealth>();
        UI = GetComponent<EnemyUI>();
        SetupStats();
        _nameScene = SceneManager.GetActiveScene().name;
    }

    public override void Start()
    {
        EnemyManager.Instance.AddEnemiesToListAndDic(gameObject, isColorized);
        DisplayGoodUI();
        CrystalManager.Instance.AddToDict(this, isColorized, _nameScene);
        vector2s = LazerCardinals;
    }

    public override void Update()
    {

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
        CrystalManager.Instance.UpdateIsColorizedIfChanged(this, isColorized, _nameScene);
    }
}

