using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float StartTime {  get; private set; } = 900f;
    [SerializeField] private float timeLeft;
    [SerializeField] private bool isRunning;

    private Coroutine TimerCoroutine;
    /// <summary>
    /// Timer mit einer Zeit (Default 900 -> 15 Minuten) initialisieren
    /// </summary>
    //private Timer()
    private void Awake()
    {
        timeLeft = StartTime; // Startzeit setzen
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
            timeLeft -= 1f;
        }

        if (timeLeft <= 0)
        {
            Debug.Log("Zeit ist abgelaufen!");
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
    public void AddTime(float seconds)
    {
        timeLeft += seconds;
        Debug.Log(seconds + " Sekunden hinzugefügt. Verbleibende Zeit: " + timeLeft);
    }

    /// <summary>
    /// Zieht dem Timer eine bestimmte Menge an Zeit ab
    /// </summary>
    /// <param name="seconds">Anzahl der Sekunden zum abziehen</param>
    public void SubtractTime(float seconds)
    {
        timeLeft = Mathf.Max(0, timeLeft - seconds); // Falls die Anzahl der Sekunden ins negative geht dann return 0;
        Debug.Log(seconds + " Sekunden abgezogen. Verbleibende Zeit: " + timeLeft);
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

