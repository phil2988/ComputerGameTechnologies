using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        // Rotate the coin slowly for testing
        transform.Rotate(Vector3.up, Time.deltaTime * 60f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Money>().addCoin();
            Destroy(gameObject);
        }
    }
}
