using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for when the slime boss dies.
/// Summons smaller slimes and destroys the boss
/// </summary>
public class OnSlimeBossDeath : MonoBehaviour
{
    public int smallSlimesSpawnCount;
    public GameObject smallSlime;
 
    public void OnDeath()
    {
        if (gameObject != null)
        {
               Destroy(gameObject);
        }
    }
}
