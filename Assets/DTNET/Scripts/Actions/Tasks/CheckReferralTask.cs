using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DTNET.Models;
using DTNET.Handlers;

namespace DTNET.Tasks {
    public class CheckReferralTask : MonoBehaviour
    {
        public PatientTask patientInfo;
        public GameObject computerCanvas;
        public CameraUIHandler cameraUI;

        public TaskTrackingHandler taskTracking;

        private bool screenIsActive = false;
        private bool taskIsPerformed = false;


        void Start()
        {
            HideScreen();
        }

        /* 
        * The Task!
        */
        public void PerformCheckReferralTask() 
        {
            Debug.Log("PerformCheckReferralTask!");

            screenIsActive = !screenIsActive;
            computerCanvas.SetActive(screenIsActive);

            SetPatientInfoOnScreen();

            if(taskIsPerformed) return;
            // Task Perfomed
            taskTracking.CheckReferralTaskCompleted();
            taskIsPerformed = true;
            cameraUI.DisplayMessage("Next\nCollect the sampling tubes");
        }

        private void SetPatientInfoOnScreen() 
        {
            setPatientIDRowText("ID: " + patientInfo.getPatientId());
            setPatientNameRowText("Name : " + patientInfo.getPatientFullnamne());
            setFastingRowText("Fasting: "+patientInfo.PatientHasBeenFasting());
            setTubesToTakeRowText(patientInfo.getTubesToTakeStr());
        }

        private void DisplayScreen()
        {
            computerCanvas.SetActive(true);
        }

        private void HideScreen()
        {
            computerCanvas.SetActive(false);
        }

        private GameObject getReferralPanel() 
        {
            return computerCanvas.transform.GetChild(1).gameObject;
        }

        private void setPatientIDRowText(string text) 
        {
            SetTextOnRow(0, text);
        }

        private void setPatientNameRowText(string text) 
        {
            SetTextOnRow(1, text);
        }

        private void setFastingRowText(string text) 
        {
            SetTextOnRow(2, text);
        }

        private void setTubesToTakeRowText(string text) 
        {
            SetTextOnRow(3, text);
        }

        private void SetTextOnRow(int rowIndex, string text) 
        {
            GameObject refPanel = getReferralPanel();
            TextMeshProUGUI idrow = refPanel.transform.GetChild(rowIndex).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            idrow.text = text;
        }

    }
}
