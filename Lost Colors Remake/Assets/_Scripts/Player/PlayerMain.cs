using DG.Tweening;
using System.Drawing;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain Instance { get; private set; }

    public PlayerMove Move { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerHealth Health { get; private set; }

    public PlayerUI UI { get; private set; }

    public PlayerAttack Attack { get; private set; }

    public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public GameObject ProjectileSocket { get; private set; }
    public GameObject PlayerMesh;
    [Header("Player Sprite")]
    public SpriteRenderer playerSprite;

    public Sprite playerSpriteRightColor;
    public Sprite playerSpriteLeftColor;
    public Sprite playerSpriteUpColor;
    public Sprite playerSpriteDownColor;

    float horizontal;
    float vertical;

    public Animator anim;



    public GameObject paintBrushSocket;

    public bool isColorized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Move = GetComponent<PlayerMove>();
        Inventory = GetComponent<PlayerInventory>();
        Health = GetComponent<PlayerHealth>();
        UI = GetComponent<PlayerUI>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Attack = GetComponent<PlayerAttack>();

    }


    private void Start()
    {
        if (Inventory.CheckBrushInDatabase()){
            ColorSwitch(true);
        }
    }

    private void Update()
    {
        GetDirection();
    }


    public void ColorSwitch(bool goToColor)
    {
        if (goToColor) {
            playerSprite.material.DOFloat(1f, "_Transition", 2.5f).SetEase(Ease.OutQuad);
            isColorized = true;
        }
        else
        {
            playerSprite.material.DOFloat(0f, "_Transition", 2.5f).SetEase(Ease.OutQuad);
            isColorized = false;
        }
    }

    public void GetDirection()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Setting the animators parameters
        anim.SetFloat("X", horizontal);
        anim.SetFloat("Y", vertical);
        Debug.Log(horizontal);
        Debug.Log(vertical);

    }
}

