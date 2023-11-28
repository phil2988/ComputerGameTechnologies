using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* The Skeleton is a ranged enemy
 * Throws skulls at the player
 * Kites backwards if the player gets too close
 */

public class SkeletonEnemy : MonoBehaviour, IEnemy
{
    private float health;
    private float movementSpeed;
    private float detectionRadius;
    private float minimumDistance;

    // Attack
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float nextShotTime;
    public GameObject projectile;

    // Loot
    public GameObject lootPrefab;
    public int lootDropChance;
    

    public Transform _target;
    public Animator _animator;
    public Rigidbody2D _rigidbody;

    public void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void FixedUpdate()
    {
        // If next attack is ready and player is within attack range
        if(Time.time > nextShotTime && Vector2.Distance(transform.position, _target.position) < attackRange)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + attackSpeed;
        }

        MoveAwayFromTarget();
    }

    public void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        _animator.SetTrigger("WalkTrigger");
    }

    // Add movement animations depending on which way the skeleton moves - This depends in where the player is related to this object
    void MoveAwayFromTarget()
    {
        if (_target != null)
        {
            if (Vector2.Distance(transform.position, _target.position) < minimumDistance)
            {
                _animator.SetTrigger("WalkTrigger");
                Vector2 direction = (transform.position - _target.position).normalized;
                _rigidbody.AddForce(direction * (movementSpeed * Time.deltaTime));
            }
            else
            {
                _animator.SetTrigger("WalkTrigger");
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }

    public void Attack()
    {
        // If next attack is ready and player is within attack range
        if (Time.time > nextShotTime && Vector2.Distance(transform.position, _target.position) < attackRange)
        {
            // Play an attack animation
            _animator.SetTrigger("AttackTrigger");
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + attackSpeed;

            /*
             * Need a script attached to player containing the below stats for calculation to work:
             * 
             * Armor calculation
            int playerArmor = player.GetComponent<PlayerStats>().armor;
            float damageReductionPercentage = 0.2f
            int finalDamage =  Mathf.Max(0, Mathf.RoundToInt(baseDamage - (playerArmor * damageReductionPercentage)));

            * Deal damage to player
            player.GetComponent<Health>.TakeDamage(finalDamage);
            */
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void DropLoot()
    {
        if (Random.Range(0, 100) < lootDropChance && lootPrefab != null)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        if(_animator != null)
        {
            _animator.SetTrigger("Die");
            DropLoot();
            Destroy(gameObject);
        }
    }

    // Setters
    public SkeletonEnemy setTarget(Transform target)
    {
        this._target = target;
        return this;
    }

    public SkeletonEnemy setAnimator(Animator animator)
    {
        this._animator = animator;
        return this;
    }

    public SkeletonEnemy setRigidBody(Rigidbody2D rigidbody)
    {
        this._rigidbody = rigidbody;
        return this;
    }
}
