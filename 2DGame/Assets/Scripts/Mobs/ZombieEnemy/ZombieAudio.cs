using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioClip zombieIdleClip;
    public AudioClip zombieAttackClip;
    public AudioClip zombieTakeDamageClip;
    public AudioClip zombieDeathClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void PlayIdleClip()
    {
        PlaySound(zombieIdleClip);
    }

    public void PlayAttackClip()
    {
        PlaySound(zombieAttackClip);
    }

    public void PlayTakeDamageClip()
    {
        PlaySound(zombieTakeDamageClip);
    }

    public void PlayDeathClip()
    {
        PlaySound(zombieDeathClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if(clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
