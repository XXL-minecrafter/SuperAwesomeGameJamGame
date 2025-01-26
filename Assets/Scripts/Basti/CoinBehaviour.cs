using System;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour , ICollectable
{
    private int value = 1;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;

    public static Action OnCoinCollect;
    private void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    public void Collect()
    {
        playerStats.IncreaseCoins(value);

        SpawnPoint.RemoveSpawnPointObject();
        OnCoinCollect?.Invoke();
        Destroy(gameObject);
    }
}
