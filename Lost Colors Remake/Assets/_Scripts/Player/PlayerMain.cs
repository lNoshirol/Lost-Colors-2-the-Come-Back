using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain Instance { get; private set; }

    public PlayerMove Move { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerHealth Health { get; private set; }

    public PlayerUI UI { get; private set; }

    public PlayerAttack Attack { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public GameObject ProjectileSocket { get; private set; }
    public GameObject PlayerMesh;

    public PlayerInput playerInput;

    public GameObject paintBrushSocket;

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
        Rigidbody = GetComponent<Rigidbody>();
        Attack = GetComponent<PlayerAttack>();

        playerInput = GetComponent<PlayerInput>();

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        CameraMain.Instance.CenterCameraAtPosition(Instance.transform.position);
    }

}
