using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;
    private void OnEnable()
    {
        Timer.OnTimerRanOut += ShowLoseScreen;
    }

    private void OnDisable()
    {
        Timer.OnTimerRanOut -= ShowLoseScreen;
    }

    private void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}
