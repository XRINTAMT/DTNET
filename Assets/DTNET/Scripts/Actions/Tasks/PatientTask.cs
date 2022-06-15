using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DTNET.Models;
using DTNET.Handlers;

namespace DTNET.Tasks {
    public class PatientTask : MonoBehaviour
    {
        public PatientStorage patientStorage;
        public TaskTrackingHandler taskTracking;
        public GameObject PatientTextBoxCanvas;
        public CameraUIHandler cameraUI;

        private Patient patient;

        private bool textBoxIsActive;
        private float textAliveTime = 0.0f;
        
        void Awake()
        {
            initPatient();
        }


        void Update()
        {
            if(!textBoxIsActive) return;
        
            Debug.Log("textAliveTime : "+textAliveTime);
            textAliveTime -= Time.deltaTime;
            if (textAliveTime <= 0.0f)
            {
                textBoxIsActive = !textBoxIsActive;
                SetTextBoxActive(textBoxIsActive);
                //cameraUI.DisplayMessage("Verify ID in Referral");
            }
        }

        /* 
        * The Task!
        */
        public void PerformAskPatientIDTask() {
            textBoxIsActive = !textBoxIsActive;
            SetTextBoxActive(textBoxIsActive);
            taskTracking.AskForPatientIDTaskCompleted();
            cameraUI.DisplayMessage("Next\nVerify ID in Referral");
            textAliveTime = 7.0f;
        }

        private void initPatient() {
            fetchRandomPatientFromStorage();
            textBoxIsActive = false;
            setPatientInformationInTextBox();
            SetTextBoxActive(textBoxIsActive);
        }

        private void fetchRandomPatientFromStorage() {
            patient = patientStorage.GetRandomPatient();
        }

        private void SetTextBoxActive(bool isActive) {
            PatientTextBoxCanvas.SetActive(isActive);
        }

        private void setPatientInformationInTextBox() {
            TextMeshProUGUI idInfoTMP = getTextPromtObject();
            idInfoTMP.text = getPatientIdentityInformation();
        }

        private TextMeshProUGUI getTextPromtObject() {
            return PatientTextBoxCanvas.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        private string getPatientIdentityInformation() {
            string patientInfo = "Hi name name is: " + this.getPatientFullnamne() + "\n";
            patientInfo += "My identifier is: " + this.getPatientId() + "\n";
            if(this.PatientHasBeenFasting()) 
            {
                patientInfo += "I have been fasting.";
            } 
            else {
                patientInfo += "I ate just before I got here.";
            }
            return patientInfo;
        }

    // ========================== Patient Helper Functions ========================================
        public Patient getPatient() {
            return patient;
        }

        public int getPatientId() {
            return patient.id;
        }

        public string getPatientFullnamne() {
            return patient.fullName;
        }

        public bool PatientHasBeenFasting() 
        {
            return patient.referral.shouldBeFasting;
        }

        public List<SampleTube> GetTubesToTakeList() {
            return patient.referral.tubesToTake;
        }

        public int GetNumberOfTubesToTake() {
            return patient.referral.tubesToTake.Count;
        }

        public string getTubesToTakeStr() {
            string tubes = "";
            List<SampleTube> tubesToTake = GetTubesToTakeList();
            foreach (SampleTube tube in tubesToTake) {
                tubes += (tube.analysis + "\n");
            }
            return tubes;
        }
    }
}
