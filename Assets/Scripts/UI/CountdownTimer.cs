using System;
using System.Threading;
using FMODUnity;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 120f;
    public TextMeshProUGUI timerText;
    public float intesityThreshold = 0.5f;
    [SerializeField] private StudioEventEmitter musicEmitter;
    private float timeRemaining;
    public float TimeRemaining => timeRemaining;
    public bool timerFinished = false;
    public bool runTimer = true;
    public Action finishedAction;
    private bool isHighIntensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = startTime;
        SetMusicIntensity(false);
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();

        bool shouldBeHighIntensity = timeRemaining < startTime * intesityThreshold;
        if (shouldBeHighIntensity != isHighIntensity)
        {
            SetMusicIntensity(shouldBeHighIntensity);
        }
    }

    private void SetMusicIntensity(bool highIntensity)
    {
        isHighIntensity = highIntensity;
        string intensityLabel = highIntensity ? "High" : "Low";

        // Intensity is local to the music event, not a global FMOD parameter.
        if (musicEmitter == null || !musicEmitter.EventInstance.isValid())
        {
            return;
        }

        FMOD.RESULT result = musicEmitter.EventInstance.setParameterByNameWithLabel(
            "Intensity",
            intensityLabel
        );

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError($"Could not set FMOD Intensity to {intensityLabel}: {result}");
        }
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
        SetMusicIntensity(false);
    }

    public void TimerFinished()
    {
        if (finishedAction != null)
        {
            finishedAction();
        }
    }
}
