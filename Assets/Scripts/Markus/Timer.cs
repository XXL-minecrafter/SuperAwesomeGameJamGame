using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public int StartTime {  get; private set; } = 900;
    [SerializeField] private int timeLeft;
    [SerializeField] private bool isRunning;

    private Coroutine TimerCoroutine;

    private static Action<int> onTimerUpdated;
    public static event Action<int> OnTImerUpdated { add => onTimerUpdated += value; remove => onTimerUpdated -= value; }

    private static Action onTimerRanOut;
    public static event Action OnTimerRanOut { add => onTimerRanOut += value; remove => onTimerRanOut -= value; }

    /// <summary>
    /// Timer mit einer Zeit (Default 900 -> 15 Minuten) initialisieren
    /// </summary>
    //private Timer()
    private void Awake()
    {
        timeLeft = StartTime; // Startzeit setzen

        onTimerUpdated?.Invoke(timeLeft);

        isRunning = false; // Darf nicht läufen
    }

    /// <summary>
    /// Startet den Timer
    /// </summary>
    public void StartTimer()
    {
        isRunning = true; // Und los gehts
        StartCoroutine(TimerCountdown()); //Hopp!
    }

    /// <summary>
    /// Stopt den Timer
    /// </summary>
    public void StopTimer()
    {
        isRunning = false; // Halte den Timer an
    }

    /// <summary>
    /// Coroutine, die den Timer herunterzählt
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimerCountdown()
    {
        while (timeLeft > 0f && isRunning)
        {
            Debug.Log("Verbleibende Zeit: " + timeLeft);
            yield return new WaitForSeconds(1f);
 
            SubtractTime(1);
        }

        if (timeLeft <= 0)
        {
            Debug.Log("Zeit ist abgelaufen!");
            onTimerRanOut?.Invoke();
        }
        else
        {
            Debug.Log("Timer gestoppt.");
        }
    }

    /// <summary>
    /// Füge dem Timer neue Zeit hinzu
    /// </summary>
    /// <param name="seconds">Anzahl der Sekunden zum hinzufügen</param>
    public void AddTime(int seconds)
    {
        timeLeft += seconds;
        Debug.Log(seconds + " Sekunden hinzugefügt. Verbleibende Zeit: " + timeLeft);
    }

    /// <summary>
    /// Zieht dem Timer eine bestimmte Menge an Zeit ab
    /// </summary>
    /// <param name="seconds">Anzahl der Sekunden zum abziehen</param>
    public void SubtractTime(int seconds)
    {
        timeLeft = Mathf.Max(0, timeLeft - seconds); // Falls die Anzahl der Sekunden ins negative geht dann return 0;
        Debug.Log(seconds + " Sekunden abgezogen. Verbleibende Zeit: " + timeLeft);

        onTimerUpdated?.Invoke(timeLeft);
    }

    /// <summary>
    /// Gibt die aktuelle Zeit zurück
    /// </summary>
    /// <returns></returns>
    public float GetTime()
    {
        return timeLeft;
    }
} // End public class Timer : MonoBehaviour

