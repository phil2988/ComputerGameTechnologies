using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyAI : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float attackDistanceThreshold = 0.8f;
    [SerializeField] private float attackDelay = 1;
    private float passedTime = 1;
    public int damageAmount;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.Log(("No player"));
            return;
        }

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= attackDistanceThreshold)
        {
            if (passedTime >= attackDelay)
            {
                passedTime = 0;
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                Debug.Log("Kill!");
                playerStats.TakeDamage(damageAmount);
            }
        }

        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }

    }
}
