using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Text timerTextPassed;
    [SerializeField] Text timerTextPassedTest;
    float sec = 0;
    float min = 0;
    float hour = 0;
    float secPassed = 0;
    float minPassed = 0;
    float hourPassed = 0;

    bool flashSecond;
    bool pause;
    // Start is called before the first frame update
    void Start()
    {
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;

        StartCoroutine(TimerFlow());
        StartCoroutine(TimerFlowPassed());
    }
    public void TimePassed(Text textTime)
    {
        textTime.text= "Your time: " + minPassed.ToString("00") + " : " + secPassed.ToString("00");
    }
    public void Pause() 
    {
        pause = !pause;
    }
    public void WaitTime(float waitTime)
    {
        min = min + waitTime;
        minPassed = minPassed + waitTime;
    }
    IEnumerator TimerFlow()
    {
        while (!pause)
        {
            flashSecond = !flashSecond;

            if (sec == 59)
            {
                min++;
                sec = -1;
            }
            if (min >= 60)
            {
                float difference = min - 60;
                hour++;
                min = 0 + difference;
            }
            if (hour == 24)
            {
                hour = 0;
            }
            sec += 1;

            if (flashSecond) timerText.text = hour.ToString("00") + ":" + min.ToString("00")/* + ":" + sec.ToString("00")*/;
            else timerText.text = hour.ToString("00") + " " + min.ToString("00")/* + " " + sec.ToString("00")*/;
          
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator TimerFlowPassed()
    {
        while (!pause)
        {
            if (secPassed == 59)
            {
                minPassed++;
                secPassed = -1;
            }
            if (minPassed >= 60)
            {
                float difference = min - 60;
                hourPassed++;
                minPassed = 0 + difference;
            }
            if (hourPassed == 24)
            {
                hourPassed = 0;
            }
            secPassed += 1;

            if (timerTextPassedTest!=null)
            {
                timerTextPassedTest.text = hourPassed.ToString("00") + ":" + minPassed.ToString("00")+ ":" + secPassed.ToString("00");
            }
            yield return new WaitForSeconds(1);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
