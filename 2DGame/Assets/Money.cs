using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField]
    private int amount;

    [SerializeField] private TextMeshProUGUI coinCounter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            addCoin();
        }
        
        coinCounter.text = ": " + amount;
    }

    public void addCoin(int amountToAdd = 0)
    {
        if(amountToAdd == 0)
        {
            amount++;
        }
        amount += amountToAdd;
    }

    public bool useMoney(int cost)
    {
        if (amount >= cost)
        {
            amount = amount - cost;
            return true;
        }
        else
        {
            return false;
        }
    }

}
