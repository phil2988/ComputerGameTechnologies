using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float movementSpeed;
    public float detectionRadius;
    public float playerIsDetectedRadius;
    public float kiteBackwardsRadius;
    public float attackRadius;
    
    float distanceToPlayer;
    public bool playerDetected = false;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private ScaleDeathAnimation scaleDeathAnimationScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scaleDeathAnimationScript = GetComponent<ScaleDeathAnimation>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRadius)
        {
            playerDetected = true;
        }
        if (playerDetected)
        {
            if(distanceToPlayer < kiteBackwardsRadius)
            {
                MoveAwayFromPlayer(player);
            }
            else if(distanceToPlayer > attackRadius)
            {
                MoveTowardsPlayer(player);
            }
            else
            {
                BeIdle();
            }
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

    public void MoveAwayFromPlayer(Transform player)
    {
        Vector2 direction = (transform.position - player.position).normalized;
        rb.velocity = direction * movementSpeed;

        FlipSprite(direction.x);
        anim.SetTrigger("WalkTrigger");
    }

    void FlipSprite(float directionX)
    {
        if (directionX > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void BeIdle()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("IdleTrigger");
    }

    public void StartDeathAnimation()
    {
        if (scaleDeathAnimationScript != null)
        {
            scaleDeathAnimationScript.enabled = true;
        }
    }

    public void EndDeathAnimation()
    {
        if (scaleDeathAnimationScript != null)
        {
            scaleDeathAnimationScript.enabled = false;
        }
    }
}