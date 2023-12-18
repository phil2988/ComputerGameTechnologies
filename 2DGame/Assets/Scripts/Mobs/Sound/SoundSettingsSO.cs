using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Enemy/Sound Settings")]
public class SoundSettingsSO : ScriptableObject
{
    public AudioClip idleSound;
    public AudioClip attackSound;
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
}