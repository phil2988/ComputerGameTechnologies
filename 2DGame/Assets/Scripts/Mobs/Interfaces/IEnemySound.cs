using UnityEngine;

public interface IEnemySound
{
    void PlayIdleSound(AudioClip clip);
    void PlayAttackSound(AudioClip clip);
    void PlayTakeDamageSound(AudioClip clip);
    void PlayDeathSound(AudioClip clip);
}