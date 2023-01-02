using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{   
    [SerializeField] Text timerText;
    [SerializeField] Text timerTextCurrent;
    [SerializeField] Text timerTextExamMode;
    [SerializeField] GameObject endPanel;
    float secTimer = 0;
    float minTimer = 0;
    float hourTimer = 0;

    float secCurrent = 0;
    float minCurrent = 0;
    float hourCurrent = 0;
   

    bool flashSecond;
    bool pause;
    public bool setTime;
    // Start is called before the first frame update
    void Start()
    {
        hourCurrent = System.DateTime.Now.Hour;
        minCurrent = System.DateTime.Now.Minute;
        secCurrent = System.DateTime.Now.Second;

        StartCoroutine(TimerFlowUp());
        StartCoroutine(TimerFlowCurrentTime());
    }
    public void TimePassed(Text textTime)
    {
        textTime.text= "Your time: " + minTimer.ToString("00") + " : " + secTimer.ToString("00");
    }
    public void Pause() 
    {
        pause = !pause;
    }
    public void WaitTime(float waitTime)
    {
        minTimer = minTimer + waitTime;
        minCurrent = minCurrent + waitTime;
    }

    IEnumerator TimerFlowUp()
    {
        while (!pause)
        {
            if (secTimer == 59)
            {
                minTimer++;
                secTimer = -1;
            }
            if (minTimer >= 60)
            {
                float difference = minTimer - 60;
                hourTimer++;
                minTimer = 0 + difference;
            }
            if (hourTimer == 24)
            {
                hourTimer = 0;
            }
            secTimer += 1;

            if (timerText != null)
            {
                timerText.text = hourTimer.ToString("00") + ":" + minTimer.ToString("00") + ":" + secTimer.ToString("00");
            }
            yield return new WaitForSeconds(1);

        }
    }
    IEnumerator TimerFlowCurrentTime()
    {
        while (!pause)
        {
            flashSecond = !flashSecond;

            if (secCurrent == 59)
            {
                minCurrent++;
                secCurrent = -1;
            }
            if (minCurrent >= 60)
            {
                float difference = minCurrent - 60;
                hourCurrent++;
                minCurrent = 0 + difference;
            }
            if (hourCurrent == 24)
            {
                hourCurrent = 0;
            }
            secCurrent += 1;

            if (flashSecond) timerTextCurrent.text = hourCurrent.ToString("00") + ":" + minCurrent.ToString("00")/* + ":" + secCurrent.ToString("00")*/;
            else timerTextCurrent.text = hourCurrent.ToString("00") + " " + minCurrent.ToString("00")/* + " " + sec.ToString("00")*/;
          
            yield return new WaitForSeconds(1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (endPanel.activeSelf && !setTime)
        {
            TimePassed(timerTextExamMode);
            setTime = true;
        }
     
    }
}
