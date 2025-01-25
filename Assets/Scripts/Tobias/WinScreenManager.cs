using UnityEngine;
using System;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void Start()
    {
        PlayerStats.Instance.OnAllGumsPlaced += ShowWinScreen;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.OnAllGumsPlaced -= ShowWinScreen;
    }

    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }
}
