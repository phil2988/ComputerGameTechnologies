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
    private float nextShotTime;
    public GameObject projectile;

    // Loot
    public GameObject lootPrefab;
    public int lootDropChance;

    private Transform _player;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    EnemyState _currentState = EnemyState.Idle;

    public void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player").transform);
        setAnimator(GetComponent<Animator>());
        setRigidBody(GetComponent<Rigidbody2D>());
    }

    public void FixedUpdate()
    {
        if(Time.time > nextShotTime)
        {
            setCurrentState(EnemyState.Attacking);
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + attackSpeed;
        }

        MoveAwayFromPlayer();
    }

    public void Attack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= attackRange)
        {
            setCurrentState(EnemyState.Attacking);

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
        setCurrentState(EnemyState.TakingDamage);
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
            setCurrentState(EnemyState.Dying);
            _animator.SetTrigger("Die");
            DropLoot();
            Destroy(gameObject);
        }
    }

    // Add movement animations depending on which way the skeleton moves - This depends in where the player is related to this object
    void MoveAwayFromPlayer()
    {
        if (_player != null)
        {
            if (Vector2.Distance(transform.position, _player.position) < minimumDistance)
            {
                setCurrentState(EnemyState.MovingAwayFromPlayer);
                Vector2 direction = (transform.position - _player.position).normalized;
                _rigidbody.AddForce(direction * (movementSpeed * Time.deltaTime));
            }
            else
            {
                setCurrentState(EnemyState.Idle);
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }

    // Setters
    public SkeletonEnemy setTarget(Transform target)
    {
        this._player = target;
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

    public void setCurrentState(EnemyState enemyState)
    {
        this._currentState = enemyState;
    }

    public EnemyState getCurrentState()
    {
        return _currentState;
    }
}
