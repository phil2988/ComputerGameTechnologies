using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for when the small slime dies.
/// </summary>
public class OnSmallSlimeDeath : MonoBehaviour
{
    public void OnDeath()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
