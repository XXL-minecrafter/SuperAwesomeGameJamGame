using UnityEngine;

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
