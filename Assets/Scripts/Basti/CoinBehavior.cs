using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private int value = 1;

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Collected();
        }
    }

    private void Collected()
    {
        playerStats.IncreaseCoins(value);

        Destroy(gameObject);
        Destroy(this);
    }
}
