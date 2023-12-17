using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;
    [SerializeField] private int currHealth;

    [SerializeField] private int maxArmor;
    [SerializeField] private int minArmor;
    [SerializeField] private int currArmor;

    [SerializeField] public Transform respawnPoint;
    [SerializeField] public GameObject player;

    [SerializeField] public GameObject gameOver;
    public PlayerMovement playerMovement;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        currArmor = maxArmor;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damageAmount)
    {

        SetPlayerHealth(GetPlayerHealth() - damageAmount);

        if (GetPlayerHealth() <= minHealth )
        {
            playerMovement.gameOver = true;
            gameOver.SetActive(true);
        }
    }


    public int GetPlayerArmor()
    {
        return currArmor;
    }

    private void SetPlayerArmor(int armor)
    {
        currArmor = armor;
    }

    public int GetPlayerHealth()
    {
        return currHealth;
    }

    private void SetPlayerHealth(int health)
    {
        currHealth = health;
        healthBar.SetHealth(currHealth);
    }

    public void respawnPlayer()
    {
        gameOver.SetActive(false);
        player.transform.position = respawnPoint.position;
        SetPlayerHealth(maxHealth);
        playerMovement.gameOver = false;
    }
}
