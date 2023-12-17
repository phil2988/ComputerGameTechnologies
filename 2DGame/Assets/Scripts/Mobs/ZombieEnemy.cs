using Assets.Scripts.AI;
using System.Collections;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour, IEnemy
{
<<<<<<< Updated upstream
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
=======
    public float health = 100;
    public float movementSpeed = 1;
    public float detectionRadius = 5;
    public float distanceToPlayer;
    public float damage = 10;
    public float attackRange = 3;

    //public GameObject lootPrefab;
    //public int lootDropChange;

    bool isIdle = true;
    bool isCharging = false;

    public Transform _target;
    public Animator _animator;
    public Rigidbody2D _rigidbody;
>>>>>>> Stashed changes

    public void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player").transform);
    } 

    public void FixedUpdate()
    {
<<<<<<< Updated upstream
        _animator.SetTrigger("IdleTrigger");
        float distanceToPlayer = Vector2.Distance(transform.position, _target.position);
        if (distanceToPlayer < detectionRadius)
        {
            if (distanceToPlayer > attackRange)
=======
        distanceToPlayer = Vector2.Distance(transform.position, _target.position);
        // Debug.Log("Distance to Player: " + distanceToPlayer);
        if (distanceToPlayer < detectionRadius)
        {
            Debug.Log("Player detected");
            while (distanceToPlayer > attackRange)
>>>>>>> Stashed changes
            {
                MoveTowardsTarget(_target);
            }
            
            if(attackRange > distanceToPlayer)
            {
<<<<<<< Updated upstream
                Attack();
=======
                StartCoroutine(ChargeAttackCoroutine());
>>>>>>> Stashed changes
            }
        }
        else
        {
            StopMoving();
        }
    }

    public void MoveTowardsTarget(Transform target)
    {
<<<<<<< Updated upstream
        _animator.SetTrigger("WalkTrigger");
        transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
=======
        Vector2 direction = (target.position - transform.position).normalized;
        _rigidbody.velocity = direction * movementSpeed;

        if (isIdle)
        {
            _animator.SetTrigger("WalkTrigger");
            isIdle = false;
        }

        Debug.Log("Walking towards target");
>>>>>>> Stashed changes
    }

    private void StopMoving()
    {
        if (!isIdle)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("IdleTrigger");
            Debug.Log("Zombie is idle");
            isIdle = true;
        }
    }

    public void ChargeAttack()
    {
        Debug.Log("ChargeAttack");
        if (!isCharging)
        {
            StartCoroutine(ChargeAttackCoroutine());
        }
    }

    IEnumerator ChargeAttackCoroutine()
    {
        Debug.Log("ChargeAttackCoroutine started");
        isCharging = true;

        _animator.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1f);

        Vector2 direction = (_target.position - transform.position).normalized;
        _rigidbody.velocity = direction * (movementSpeed * 2f);
        
        yield return new WaitForSeconds(0.5f);

        _rigidbody.velocity = Vector2.zero;

        _animator.SetTrigger("IdleTrigger");

        yield return new WaitForSeconds(0.5f);

        isCharging = false;
    }

    public void Attack()
    {
        Debug.Log("Zombie is attacking");
        _animator.SetTrigger("AttackTrigger");
        
        /*
        * Deal damage to player
        player.GetComponent<Health>.TakeDamage(finalDamage);
        */
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
