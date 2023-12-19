using UnityEngine;

public interface IEnemySound
{
    void PlayIdleSound();
    void PlayAttackSound();
    void PlayTakeDamageSound();
    void PlayDeathSound();
}