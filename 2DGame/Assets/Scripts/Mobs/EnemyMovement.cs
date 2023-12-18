using Assets.Scripts.AI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    public float movementSpeed;
    public float detectionRadius;
    public float soundCooldown = 8f;
    
    float distanceToPlayer;
    float timeSinceLastSound;

    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private ScaleDeathAnimation scaleDeathAnimationScript; // Disabled
    private SoundSettingsSO soundSettings;

    [SerializeField]
    private EnemyAudioManager audioManager;
    [SerializeField]
    private EnemySoundsManager soundsManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scaleDeathAnimationScript = GetComponent<ScaleDeathAnimation>();
        audioManager = GetComponent<EnemyAudioManager>();
        soundsManager = GetComponent<EnemySoundsManager>();

        if(audioManager != null)
        {
            Debug.LogError("audio manager is null");
        }
        else
        {
            Debug.Log("audio manager assigned");
        }
        if (soundsManager != null)
        {
            Debug.LogError("sounds manager is null");
        }
        else
        {
            Debug.Log("sounds manager assigned");
        }
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer(player, soundsManager);
        }
        else
        {
            BeIdle(soundsManager);
        }
    }

    public void MoveTowardsPlayer(Transform player, EnemySoundsManager soundsManager)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;

        FlipSprite(direction.x);
        anim.SetTrigger("WalkTrigger");

        if (CanPlaySound())
        {
            audioManager.HandleIdle(soundsManager, soundSettings);
            ResetSoundCooldown();
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

    public void BeIdle(EnemySoundsManager enemySound)
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("IdleTrigger");
        if (CanPlaySound())
        {
            audioManager.HandleIdle(enemySound, soundSettings);
            ResetSoundCooldown();
        }
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