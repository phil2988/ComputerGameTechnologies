using Assets.Scripts.AI;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    public float movementSpeed;
    public float detectionRadius;
    
    float distanceToPlayer;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer(player);
        }
        else
        {
            BeIdle();
        }
    }

    public void MoveTowardsPlayer(Transform player)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;

        FlipSprite(direction.x);
        anim.SetTrigger("WalkTrigger");
    }

    void FlipSprite(float directionX)
    {
        if( directionX > 0 )
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if( directionX < 0 )
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void BeIdle()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("IdleTrigger");
    }
}