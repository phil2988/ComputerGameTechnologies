using UnityEngine;

public class EnemySoundsManager : MonoBehaviour, IEnemySound
{
    private AudioSource audioSource;
    public SoundSettingsSO soundSettings;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (soundSettings == null)
        {
            Debug.LogError("Sound settings not assigned to EnemySoundsManager");
        }
    }

    public void DropLoot(GameObject lootPrefab, int lootAmount)
    {
        for (int i = 0; i < lootAmount; i++)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void PlayDeathSound(AudioClip deathSound)
    {
        PlaySound(deathSound);
    }

    public void PlayIdleSound(AudioClip idleSound)
    {
        PlaySound(idleSound);
    }

    public void PlayAttackSound(AudioClip attackSound)
    {
        PlaySound(attackSound);
    }

    public void PlayTakeDamageSound(AudioClip takeDamageSound)
    {
        PlaySound(takeDamageSound);
    }

    private void PlaySound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip not set");
        }
    }
}
