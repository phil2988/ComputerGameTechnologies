using UnityEngine;

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
            transform.localScale = originalScale * 0.5f;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}