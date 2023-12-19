using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] public Button healthButton;
    [SerializeField] public Button firerateButton;
    [SerializeField] public Button damageButton;
    [SerializeField] public Button movementspeedButton;
    [SerializeField] public Button luckButton;

    [SerializeField] public GameObject upgradeMenu;
    [SerializeField] public GameObject upgradePoint;
    [SerializeField] public GameObject player;

    [SerializeField] private int range;
    
    [SerializeField] private Money money;
    [SerializeField] private int startCost;
    [SerializeField] private int increase;

    private int healthCounter;
    private int damageCounter;
    private int movementSpeedCounter;
    private int firerateCounter;
    private int luckCounter;

    private PlayerStats stats;
    private PlayerMovement movement;
    
    // Start is called before the first frame update
    void Start()
    {
        healthButton.onClick.AddListener(OnClickHealthFunction);
        firerateButton.onClick.AddListener(OnClickFirerateFunction);
        damageButton.onClick.AddListener(OnClickDamageFunction);
        movementspeedButton.onClick.AddListener(OnClickMovementSpeedFunction);
        luckButton.onClick.AddListener(OnClickLuckFunction);
        
        stats = player.GetComponent<PlayerStats>();
        movement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(upgradePoint.transform.position, player.transform.position);
        if (distance <= range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                upgradeMenu.SetActive(!upgradeMenu.activeSelf);
            }

            if (upgradeMenu.activeSelf)
            {
                movement.menu = true;
                healthButton.GetComponentInChildren<TextMeshProUGUI>().text =
                    "Health " + "(" + (startCost + healthCounter * increase) + ")";
                firerateButton.GetComponentInChildren<TextMeshProUGUI>().text =
                    "(WIP) Firerate " + "(" + (startCost + firerateCounter * increase) + ")";
                damageButton.GetComponentInChildren<TextMeshProUGUI>().text =
                    "Damage " + "(" + (startCost + damageCounter * increase) + ")";
                movementspeedButton.GetComponentInChildren<TextMeshProUGUI>().text =
                    "Movement Speed " + "(" + (startCost + movementSpeedCounter * increase) + ")";
                luckButton.GetComponentInChildren<TextMeshProUGUI>().text =
                    "(WIP) Luck " + "(" + (startCost + luckCounter * increase) + ")";
            }
            else
            {
                movement.menu = false;
            }
        }
        
    }
    void OnClickHealthFunction()
    {
        //Check money
        //If enough
        //Upgrade stat
        //else nothing
        if (money.useMoney(startCost + (healthCounter * increase)))
        {
            healthCounter++;
            stats.AddMaxHealth(10);
            stats.Heal(10);
        }
    }
    void OnClickFirerateFunction()
    {
        if (money.useMoney(startCost + firerateCounter * increase))
        {
            firerateCounter++;
            stats.Firerate = stats.Firerate+5;
        }
    }
    void OnClickDamageFunction()
    {
        if (money.useMoney(startCost + damageCounter * increase))
        {
            damageCounter++;
            stats.Damage= stats.Damage+5;
        }
    }

    void OnClickMovementSpeedFunction()
    {
        if (money.useMoney(startCost + movementSpeedCounter * increase))
        {
            movementSpeedCounter++;
            stats.MovementSpeed= stats.MovementSpeed+2;
        }
    }

    void OnClickLuckFunction()
    {
        if (money.useMoney(startCost + luckCounter * increase))
        {
            luckCounter++;
            stats.Luck = stats.Luck+5;
        }
    }
}
