using Assets.Scripts.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    public float movementSpeed;
    public float detectionRadius;
    public float soundCooldown;
    
    float distanceToPlayer;
    float timeSinceLastSound;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private ScaleDeathAnimation scaleDeathAnimationScript; // Disabled
    private EnemySoundsManager soundsManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scaleDeathAnimationScript = GetComponent<ScaleDeathAnimation>();
        soundsManager = GetComponent<EnemySoundsManager>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer();
        }
        else
        {
            BeIdle();
        }
    }

    public void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;

        FlipSprite(direction.x);
        anim.SetTrigger("WalkTrigger");

        if(soundsManager != null )
        {
            if (CanPlaySound())
            {
                this.soundsManager.PlayIdleSound();
                ResetSoundCooldown();
            }
        }
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
        //anim.SetTrigger("IdleTrigger");
    }

    private bool CanPlaySound()
    {
        return Time.time - timeSinceLastSound >= soundCooldown;
    }

    private void ResetSoundCooldown()
    {
        timeSinceLastSound = Time.time;
    }

    public void StartDeathAnimation()
    {
        if(scaleDeathAnimationScript != null)
        {
            scaleDeathAnimationScript.enabled = true;
        }
    }

    public void EndDeathAnimation()
    {
        if(scaleDeathAnimationScript != null)
        {
            scaleDeathAnimationScript.enabled = false;
        }
    }
}