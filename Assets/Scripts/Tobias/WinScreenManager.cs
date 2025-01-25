using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private void OnEnable()
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
