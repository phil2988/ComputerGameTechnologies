using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 2f;
    public float fireInterval = 2f;
    public float projectileLifetime = 5f;

    private Transform player;
    private float timeSinceLastFire = 0f;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (CanFireProjectile())
        {
            FireProjectile();
            timeSinceLastFire = 0f;
        }
        timeSinceLastFire += Time.deltaTime;
    }

    bool IsPlayerInAttackRange()
    {
        return Vector2.Distance(transform.position, player.position) <= GetComponent<SkeletonMovement>().attackRadius;
    }
    
    bool CanFireProjectile()
    {
        return timeSinceLastFire >= fireInterval && IsPlayerInAttackRange();
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.GetComponent<DamagePlayer>().damage = gameObject.GetComponent<EnemyAI>().damageAmount;
        
        Vector2 direction = (player.position - transform.position).normalized;
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;
        Destroy(projectile, projectileLifetime);
    }
}
