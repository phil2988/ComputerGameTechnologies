using UnityEngine;

public class EnemySoundsManager : MonoBehaviour, IEnemySound
{
    private AudioSource audioSource;
    public SoundSettingsSO soundSettings;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayIdleSound()
    {
        PlaySound(soundSettings.idleSound);
    }

    public void PlayAttackSound()
    {
        PlaySound(soundSettings.attackSound);
    }

    public void PlayTakeDamageSound()
    {
        PlaySound(soundSettings.takeDamageSound);
    }
    public void PlayDeathSound()
    {
        PlaySound(soundSettings.deathSound);
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
