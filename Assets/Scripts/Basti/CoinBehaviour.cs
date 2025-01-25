using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour , ICollectable
{
    private int value = 1;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;

    private void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    public void Collect()
    {
        playerStats.IncreaseCoins(value);

        SpawnPoint.HasObject = false;
        Destroy(gameObject);
        Destroy(this);
    }
}
