using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaught : MonoBehaviour
{
    private Vector3 spawnPoint;
    public static Action OnPlayerCaught;
    // Start is called before the first frame update

    private void Awake()
    {
        spawnPoint = transform.position;
    }
    private void OnEnable()
    {
        CatchTransitionscript.OnFullBlackScreen += TeleportToSpawn;

    }
    private void OnDisable()
    {
        CatchTransitionscript.OnFullBlackScreen -= TeleportToSpawn;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy" && PlayerStats.Instance.IsChewing)
        {
            OnPlayerCaught?.Invoke();
        }
    }
    private void TeleportToSpawn()
    {
        transform.position = spawnPoint;
    }
}
