using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChance : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    public float dropChance = 1f;
    public void DropCoin()
    {
        if (Random.value <= dropChance)
        {
            Debug.Log("Coin dropped");
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
