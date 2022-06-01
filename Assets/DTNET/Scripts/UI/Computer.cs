using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DTNET.Models;
using DTNET.Models.Patient;

namespace DTNET.UI
{
    public class Computer : MonoBehaviour
    {

        //public Patient patient;
        public PatientObject patientObject;
        public GameObject computerCanvas;
        public GameObject mainPanel;
        public GameObject referralPanel;
        public bool isDisplayScreen;
        public CameraUI cameraUI;

        public TaskSystem taskSystem;


        // Start is called before the first frame update
        void Start()
        {
            HideScreen();
            DisplayMainPanel();
        }

        private void DisplayScreen()
        {
            computerCanvas.SetActive(true);
        }

        private void HideScreen()
        {
            computerCanvas.SetActive(false);
        }


        public void DisplayMainPanel()
        {
            mainPanel.SetActive(true);
            referralPanel.SetActive(false);
        }

        public void DisplayReferralPanel()
        {
            mainPanel.SetActive(false);
            referralPanel.SetActive(true);

            setPatientIDRowText("ID: " + patientObject.getPatientId());
            setPatientNameRowText("Name : " + patientObject.getPatientFullnamne());
            setFastingRowText("Fasting: "+patientObject.PatientHasBeenFasting());
            setTubesToTakeRowText(patientObject.getTubesToTakeStr());
            // Check off that task is done!
            taskSystem.checkedReferralDone();
            cameraUI.DisplayMessage("Collect the missing tubes!");
        }

        private GameObject getReferralPanel() {
            return this.gameObject.transform.GetChild(0).GetChild(2).GetChild(1).gameObject;
        }

        private void SetTextOnRow(int rowIndex, string text) 
        {
            GameObject refPanel = getReferralPanel();
            TextMeshProUGUI idrow = refPanel.transform.GetChild(rowIndex).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            idrow.text = text;
        }

        private void setPatientIDRowText(string text) {
            //GameObject refPanel = getReferralPanel();
            //TextMeshProUGUI idrow = refPanel.transform.GetChild(rowIndex).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            //idrow.text = text;
            SetTextOnRow(0, text);
        }

        private void setPatientNameRowText(string text) {
            int rowIndex = 1;
            GameObject refPanel = getReferralPanel();
            TextMeshProUGUI idrow = refPanel.transform.GetChild(rowIndex).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            idrow.text = text;
        }

        private void setFastingRowText(string text) {
            int rowIndex = 2;
            GameObject refPanel = getReferralPanel();
            TextMeshProUGUI idrow = refPanel.transform.GetChild(rowIndex).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            idrow.text = text;
        }
        private void setTubesToTakeRowText(string text) {
            int rowIndex = 3;
            GameObject refPanel = getReferralPanel();
            TextMeshProUGUI idrow = refPanel.transform.GetChild(rowIndex).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            idrow.text = text;
        }


        public void ClickedOn()
        {
            isDisplayScreen = !isDisplayScreen;
            if(isDisplayScreen)
            {
                DisplayScreen();
            }
            else
            {
                HideScreen();
            }
        }
    }
}
