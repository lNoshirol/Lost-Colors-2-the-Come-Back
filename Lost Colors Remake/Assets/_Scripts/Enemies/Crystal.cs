using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Crystal Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;           
    public float projectileSpeed = 10f;

    public bool PlayerInAttackRange;
    public bool CrystalCanShoot = true;

    private float checkInterval = 0.2f;
    private float nextAttackCheckTime = 0f;


    public float attackRange;

    public LayerMask whatIsPlayer;

    [SerializeField]
    private Transform player;

    public GameObject laserPrefab;
    public Vector2[] LazerCardinals;
    public Vector2[] LazerSubCardinals;
 
    public bool CheckPlayerInAttackRange()
    {
        if (Time.time >= nextAttackCheckTime)
        {
            nextAttackCheckTime = Time.time + checkInterval;
            PlayerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
        }
        return PlayerInAttackRange;
    }

    public void Update()
    {
        if (CheckPlayerInAttackRange())
        {
            
        }
    }

    private void Start()
    {
        for (int i = 0; i < LazerSubCardinals.Length; i++)
        {
            Vector2 setting = LazerSubCardinals[i];
            float delay = i * 0.5f;
            StartCoroutine(ShootInDirection(setting, delay));
        }
    }


    IEnumerator ShootInDirection(Vector2 setting, float startDelay)
    {
        yield return new WaitForSeconds(startDelay*2);

        while (true)
        {
            ShootLaser(setting);
            yield return new WaitForSeconds(2.25f);
        }
    }


    void ShootLaser(Vector2 direction)
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(laser, 2f);
    }

}
