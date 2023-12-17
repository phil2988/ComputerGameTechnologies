using Assets.Scripts.AI;
using System.Collections;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{
    public int health;
    public float movementSpeed;
    public float detectionRadius;
    float distanceToPlayer;
    public float damage;
    public float attackRange;

    public Transform _target;
    public Animator _animator;
    public Rigidbody2D _rigidbody;

    void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, _target.transform.position);

        if (distanceToPlayer < detectionRadius)
        {
            if (attackRange > distanceToPlayer)
            {
                Attack();
            }
            else
            {
                MoveTowardsTarget(_target);
            }
        }
    }

    void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(this.transform.position, _target.transform.position, movementSpeed * Time.deltaTime);
    }

    void Attack()
    {
        distanceToPlayer = Vector2.Distance(transform.position, _target.transform.position);
        transform.position = Vector2.MoveTowards(this.transform.position, _target.transform.position, movementSpeed * Time.deltaTime);
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
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