using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Zombie is a melee enemy
 * Charges at the player at high speed, dealing damage at collision
 * Or just runs towards the player
 */

public class ZombieEnemy : MonoBehaviour, IEnemy
{
    public int health;
    public float movementSpeed;
    private float detectionRadius;

    // Attack
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float chargeSpeed;

    public GameObject lootPrefab;
    public int lootDropChange;

    public Transform _target;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void FixedUpdate()
    {
        _animator.SetTrigger("IdleTrigger");
        float distanceToPlayer = Vector2.Distance(transform.position, _target.position);
        if (distanceToPlayer < detectionRadius)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsTarget(_target);
            }
            else
            {
                Attack();
            }
        }
    }

    public void MoveTowardsTarget(Transform target)
    {
        _animator.SetTrigger("WalkTrigger");
        transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
    }

    public void ChargeTowardsTarget(Transform target)
    {
        _animator.SetTrigger("WalkTrigger");
        transform.position = Vector2.MoveTowards(transform.position, target.position, chargeSpeed *Time.deltaTime);
    }

    public void Attack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _target.position);

        if (distanceToPlayer <= attackRange)
        {
            // Play attack animation
            _animator.SetTrigger("AttackTrigger");

            /*
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

    public void Die()
    {
        if(_animator != null)
        {
            _animator.SetTrigger("DieTrigger");
            DropLoot();
            Destroy(gameObject);
        }
    }

    public void DropLoot()
    {
        if (Random.Range(0, 100) < lootDropChange && lootPrefab != null)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }

    // Setters
    public ZombieEnemy setTarget(Transform target)
    {
        this._target = target;
        return this;
    }
    public ZombieEnemy setAnimator(Animator animator)
    {
        this._animator = animator;
        return this;
    }
    public ZombieEnemy setRigidBody(Rigidbody2D rigidbody)
    {
        this._rigidbody = rigidbody;
        return this;
    }
}
