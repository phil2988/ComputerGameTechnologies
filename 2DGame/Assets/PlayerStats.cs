using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;
    [SerializeField] private int currHealth;

    [SerializeField] private int firerate;
    [SerializeField] private int damage;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int luck;

    [SerializeField] public Transform respawnPoint;
    [SerializeField] public GameObject player;

    [SerializeField] public GameObject gameOver;
    public PlayerMovement playerMovement;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
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

    public void AddMaxHealth(int value)
    {
        maxHealth = maxHealth + value;
    }
    
    

    public void Heal(int value)
    {
        currHealth = currHealth + value;
    }
    
    public int Firerate
    {
        get => firerate;
        set => firerate = value;
    }

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public int MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public int Luck
    {
        get => luck;
        set => luck = value;
    }
    
    
}
