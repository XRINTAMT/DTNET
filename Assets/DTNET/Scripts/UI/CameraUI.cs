using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DTNET.CSystem;

public class CameraUI : MonoBehaviour
{
    private int currentMessageIndex = 0;

    private TextMeshProUGUI displayText;

    public float textAliveTime = 4.0f;

    private string selectedMode;

    void Start()
    {
        displayText = GetTextTMP();
        //DisplayNextMessage();
        selectedMode = GameMode.SelectedMode;
        if(isBeginnerMode()) 
        {
            DisplayMessage("Welcome!\nStart with hygin");
        } else 
        {
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        textAliveTime -= Time.deltaTime;
        if (textAliveTime <= 0.0f)
        {
            timerEnded();
        }
    }

    public void DisplayMessage(string msg) {
        if(isBeginnerMode()) {
            displayText.text = msg;
            this.gameObject.SetActive(true);
            resetTextAliveTime();
        }
    }

    private void resetTextAliveTime() {
        textAliveTime = 4.0f;
    }

    void timerEnded()
    {
        this.gameObject.SetActive(false);
    }

    private bool isBeginnerMode() {
        return (selectedMode !=" Experience");
    }

    private TextMeshProUGUI GetTextTMP() {
        return this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }
}
