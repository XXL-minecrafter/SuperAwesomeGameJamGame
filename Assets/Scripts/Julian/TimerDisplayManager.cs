using TMPro;
using UnityEngine;

public class TimerDisplayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        Timer.OnTImerUpdated += UpdateTimerDisplay;
    }

    private void OnDisable()
    {
        Timer.OnTImerUpdated -= UpdateTimerDisplay;
    }

    private void UpdateTimerDisplay(int timeLeft)
    {
        int minutes = (int)(timeLeft / 60f);
        int seconds = timeLeft % 60;

        timerText.text = $"{minutes}:{(seconds > 10 ? $"{seconds}" : $"0{seconds}")}";
    }
}
