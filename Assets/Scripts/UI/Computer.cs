using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using DTNET.Models;

namespace DTNET.UI
{
    public class Computer : MonoBehaviour
    {
        public GameObject computerCanvas;
        public GameObject mainPanel;
        public GameObject referralPanel;
        public bool isDisplayScreen;

        public PatientStorage patientStorage;

        private Patient patient;

        // Start is called before the first frame update
        void Start()
        {
            HideScreen();
            DisplayMainPanel();
            patient = patientStorage.getPatientByIndex(1);
            Debug.Log("patient : " + patient.fullName);
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
        }


        public void ClickedOn()
        {
            Debug.Log("PressedComputer!!!");
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
