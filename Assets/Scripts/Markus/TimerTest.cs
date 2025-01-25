using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{

    private Timer myTimer;

    private void Awake()
    {
        myTimer = FindObjectOfType<Timer>();
        myTimer.StartTimer();
    }


}
