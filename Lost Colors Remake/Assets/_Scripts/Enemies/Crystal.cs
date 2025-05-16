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

    public float sightRange;
    public float attackRange;

    public LayerMask whatIsPlayer;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform _socket;

    public IEnumerator FireAtPlayer()
    {
        CrystalCanShoot = false;
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject pipolopo = ProjectileManager.Instance.ProjectilePools[projectilePrefab.name].GetObject();
        pipolopo.transform.position = _socket.position;
        Rigidbody2D rb = pipolopo.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        yield return new WaitForSeconds(2f);
    }

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

}
