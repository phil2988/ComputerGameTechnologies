using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        healthButton.onClick.AddListener(OnClickHealthFunction);
        firerateButton.onClick.AddListener(OnClickFirerateFunction);
        damageButton.onClick.AddListener(OnClickDamageFunction);
        movementspeedButton.onClick.AddListener(OnClickMovementSpeedFunction);
        luckButton.onClick.AddListener(OnClickLuckFunction);
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
        }
        
    }
    void OnClickHealthFunction()
    {
        //Check money
        //If enough
        //Upgrade stat
        //else nothing
    }
    void OnClickFirerateFunction()
    {
        //Check money
        //If enough
        //Upgrade stat
        //else nothing
    }
    void OnClickDamageFunction()
    {
        //Check money
        //If enough
        //Upgrade stat
        //else nothing
    }

    void OnClickMovementSpeedFunction()
    {
        //Check money
        //If enough
        //Upgrade stat
        //else nothing
    }

    void OnClickLuckFunction()
    {
        //Check money
        //If enough
        //Upgrade stat
        //else nothing
    }
}
