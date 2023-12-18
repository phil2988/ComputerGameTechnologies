using System;
using Assets.Scripts.AI;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZombieEnemy : MonoBehaviour
{
    public int health;
    public float movementSpeed;
    public float detectionRadius;
    float distanceToPlayer;
    public float damage;
    public float attackRange;

    bool isMoving = false;
    bool isCharging = false;

    public Transform _target;
    public Animator _animator;
    public Rigidbody2D _rigidbody;

    private PlayerStats _playerStats;

    void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player").transform);
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, _target.transform.position);
        Vector2 direction = (_target.transform.position - transform.position);
        direction.Normalize();

        _rigidbody.velocity = direction * movementSpeed;
        //transform.position = Vector2.MoveTowards(this.transform.position, _target.transform.position, movementSpeed * Time.deltaTime);
        //if (distanceToPlayer < detectionRadius)
        //{
        //    if (distanceToPlayer > attackRange)
        //    {
        //        MoveTowardsTarget(_target);
        //    }
        //    else
        //    {
        //        ChargeAttack();
        //    }
        //}
        //else
        //{
        //    StopMoving();
        //}
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            _playerStats.TakeDamage((int)damage);
        }
    }
    

    void MoveTowardsTarget(Transform target)
    {
        if (!isMoving)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            _animator.SetTrigger("WalkTrigger");
            isMoving = true;
            Debug.Log("Walking towards target" + _rigidbody.velocity);
        }
    }

    void StopMoving()
    {
        if (isMoving)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("IdleTrigger");
            Debug.Log("Zombie is idle");
            isMoving = false;
        }
    }

    void ChargeAttack()
    {
        Debug.Log("ChargeAttack");
        if (!isCharging)
        {
            StartCoroutine(ChargeAttackCoroutine());
        }
    }

    IEnumerator ChargeAttackCoroutine()
    {
        Debug.Log("ChargeAttackCoroutine");
        isCharging = true;

        _animator.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1f);

        Vector2 direction = (_target.position - transform.position).normalized;
        _rigidbody.velocity = direction * (movementSpeed * 2f);

        yield return new WaitForSeconds(0.5f);

        _rigidbody.velocity = Vector2.zero;

        _animator.Play("zombie_idle");

        yield return new WaitForSeconds(0.5f);

        isCharging = false;
    }

    void Attack()
    {
        Debug.Log("Zombie is attacking");
        _animator.SetTrigger("AttackTrigger");

        /*
        * Deal damage to player
        player.GetComponent<Health>.TakeDamage(finalDamage);
        */
    }

    public void TakeDamage(int damage, GameObject sender)
    {
        Debug.Log("Taking damange");
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
        //KnockbackFeedback knockbackFeedback = GetComponent<KnockbackFeedback>();
        //knockbackFeedback.PlayFeedback(sender);
    }

    //public void DropLoot()
    //{
    //    if (UnityEngine.Random.Range(0, 100) < lootDropChange && lootPrefab != null)
    //    {
    //        Instantiate(lootPrefab, transform.position, Quaternion.identity);
    //    }
    //}

    void Die()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("DieTrigger");
            //DropLoot();
            Destroy(gameObject);
        }
    }

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