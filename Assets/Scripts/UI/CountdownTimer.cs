using System.Threading;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    private bool timerFinished = false;
    public bool timerRun = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0 && timerRun)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timerRun)
        {
            timeRemaining = 0;
            if (!timerFinished)
            {
                timerFinished = true;
                TimerFinished();

            }
            
        }

        int minutes = Mathf.FloorToInt(timeRemaining/60);
        int seconds = Mathf.FloorToInt(timeRemaining%60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    void TimerFinished()
    {
        Debug.Log("timerFinished");
    }

}
