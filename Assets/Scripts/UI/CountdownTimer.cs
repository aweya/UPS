using System;
using System.Threading;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 120f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;
    public float TimeRemaining => timeRemaining;
    public bool timerFinished = false;
    public bool runTimer = true;
    public Action finishedAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (timeRemaining > 0 && runTimer)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (runTimer)
        {
            timeRemaining = 0;
            if (!timerFinished)
            {
                timerFinished = true;
                runTimer = false;
                TimerFinished();
            }

        }
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer(float newStartTime)
    {
        startTime = newStartTime;
        timeRemaining = startTime;
        timerFinished = false;
        runTimer = true;
    }

    public void TimerFinished()
    {
        if (finishedAction != null)
        {
            finishedAction();
        }
    }
}
