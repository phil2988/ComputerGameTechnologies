using Assets.Scripts.AI;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public static EnemyAudioManager enemyAudioManager;

    void Awake()
    {
        if (enemyAudioManager == null)
        {
            enemyAudioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleIdle(IEnemySound enemySound, SoundSettingsSO soundSettings)
    {
        enemySound.PlayIdleSound(soundSettings.idleSound);
    }

    public void HandleAttack(IEnemySound enemySound, SoundSettingsSO soundSettings)
    {
        enemySound.PlayAttackSound(soundSettings.attackSound);
    }

    public void HandleTakeDamage(IEnemySound enemySound, SoundSettingsSO soundSettings)
    {
        enemySound.PlayTakeDamageSound(soundSettings.takeDamageSound);
    }
    public void HandleDeath(IEnemySound enemySound, SoundSettingsSO soundSettings)
    {
        enemySound.PlayDeathSound(soundSettings.deathSound);
    }
}
