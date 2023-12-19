using UnityEngine;

/// <summary>
/// This script causes the mob to not flip its sprite towards the player when moving.
/// Therefore it should be disabled.
/// </summary>

public class ScaleDeathAnimation : MonoBehaviour
{
    private Animator anim;
    private Vector3 originalScale;

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("enemyDeath"))
        {
            ApplyDeathScale();
        }
        else
        {
            ResetScale();
        }
    }

    private void ApplyDeathScale()
    {
        transform.localScale = originalScale * 0.5f;
    }

    private void ResetScale()
    {
        transform.localScale = originalScale;
    }
}
