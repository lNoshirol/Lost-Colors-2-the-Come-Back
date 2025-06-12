using DG.Tweening;
using UnityEngine;

public class PlayerMain : SingletonCreatorPersistent<PlayerMain>
{

    public PlayerMove Move { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerHealth Health { get; private set; }

    public PlayerUI UI { get; private set; }

    public PlayerAttack Attack { get; private set; }

    public PlayerCollision Collision { get; private set; }

    public PlayerDashVFX PlayerDashVFX { get; private set; }

    public Rigidbody2D Rigidbody2D { get; private set; }
    public Collider2D Collider2D { get; private set; }
    [field: SerializeField] public GameObject ProjectileSocket { get; private set; }
    public GameObject PlayerGameObject;
    [Header("Player Sprite")]
    public SpriteRenderer playerSprite;

    public Sprite playerSpriteRightColor;
    public Sprite playerSpriteLeftColor;
    public Sprite playerSpriteUpColor;
    public Sprite playerSpriteDownColor;

    public Sprite playerSpriteHitFrame;

    float horizontal;
    float vertical;

    public Animator anim;

    public ToileScriptable toileInfo;

    public GameObject paintBrushSocket;

    public bool isColorized;




    private void Start()
    {
        Move = GetComponent<PlayerMove>();
        Inventory = GetComponent<PlayerInventory>();
        Health = GetComponent<PlayerHealth>();
        UI = GetComponent<PlayerUI>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Attack = GetComponent<PlayerAttack>();
        Collider2D = GetComponent<Collider2D>();
        Collision = GetComponent<PlayerCollision>();
        PlayerDashVFX = GetComponent<PlayerDashVFX>();
        if (Inventory.CheckBrushInDatabase()){
            ColorSwitch(true);
        }

        UI.UpdatePlayerHealthUI();
    }

    private void Update()
    {
        GetDirectionAnim();
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

    private void GetDirectionAnim()
    {
        horizontal = Move._moveInput.x;
        vertical = Move._moveInput.y;

        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("X", horizontal);
            anim.SetFloat("Y", vertical);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

    }

    public void Dashing(bool isDashing)
    {
        anim.SetBool("IsDashing", isDashing);
    }

    public void HitFrameSpriteSwitch(bool yesOrNo)
    {
        Sprite oldSprite = playerSprite.sprite;
        anim.enabled = !yesOrNo;

        if (yesOrNo)
        {
            playerSprite.sprite = playerSpriteHitFrame;
            if (!Collision.collisionLocationAtPlayerRight)
                playerSprite.flipX = true;
        }
        else
        {
            playerSprite.sprite = oldSprite;
            playerSprite.flipX = false;
        }
    }

}

