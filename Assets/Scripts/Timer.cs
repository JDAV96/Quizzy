using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public bool timerFinished;

    private float currentTimerValue;
    private float totalTimerValue;
    Image timerImage;

    private void Awake() {
        timerImage = GetComponent<Image>();
    }
    
    void Update()
    {
        UpdateTimer();
    }

    public void StartTimer(float time)
    {
        timerFinished = false;
        totalTimerValue = time;
        currentTimerValue = totalTimerValue;
        timerImage.fillAmount = 1;
    }

    public void StopTimer()
    {
        currentTimerValue = 0;
        timerFinished = true;
    }

    private void UpdateTimer()
    {
        currentTimerValue -= Time.deltaTime;

        if(currentTimerValue > 0)
        {
            timerImage.fillAmount = currentTimerValue / totalTimerValue;
        }
        else
        {
            timerFinished = true;
        }
    }
}
