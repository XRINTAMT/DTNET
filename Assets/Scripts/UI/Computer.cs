using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

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

        //public PatientStorage patientStorage;

        public TextMeshProUGUI idRowTMP;
        public TextMeshProUGUI namerowTMP;
        public TextMeshProUGUI tubesRowTMP;


        // Start is called before the first frame update
        void Start()
        {
            HideScreen();
            DisplayMainPanel();
            //patient = patientStorage.GetRandomPatient();
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

            idRowTMP.text = "ID: " + patientObject.getPatientId();//patient.id;
            namerowTMP.text = "Patient : " + patientObject.getPatientFullnamne();//patient.fullName;
            tubesRowTMP.text = patientObject.getTubesToTake();
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
