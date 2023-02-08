using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Date : MonoBehaviour
{
    public void GenerateDatestamp()
    {
        GetComponent<Text>().text = System.DateTime.Now.Date.Day.ToString("00") + ".\n" + System.DateTime.Now.Date.Month.ToString("00");
    }

    public void GenerateTimeStamp()
    {
        GetComponent<Text>().text = System.DateTime.Now.Hour.ToString("00") + ":\n" + System.DateTime.Now.Minute.ToString("00");
    }

}
