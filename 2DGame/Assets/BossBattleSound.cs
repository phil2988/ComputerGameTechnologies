using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleSound : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private bool hasEntered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered by: " + col.name);
        if (boss != null && !hasEntered && col.name == "Player")
        {
            Debug.Log("If");
            hasEntered = true;
            AudioManager.StopAllStatic();
            AudioManager.PlaySoundStatic("ActionMusic");
        }
    }
}
