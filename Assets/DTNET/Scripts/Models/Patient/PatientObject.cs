using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DTNET.Models;
using DTNET.UI;

namespace DTNET.Models.Patient {
    public class PatientObject : MonoBehaviour
    {
        private bool textBoxIsActive;
        public PatientStorage patientStorage;
        public CameraUI cameraUI;
        public TaskSystem taskSystem; 
        private Patient patient; 
        private float textAliveTime = 0.0f;

        // Start is called before the first frame update
        void Start()
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

                cameraUI.DisplayMessage("Verify ID!");
            }
        }    
        

        private void initPatient() {
            textBoxIsActive = false;
            SetTextBoxActive(textBoxIsActive);
            fetchRandomPatientFromStorage();
            setPatientIdentityInformationsInTextBox();
        }

        
        public void askPatientAboutIdentity() {
            textBoxIsActive = !textBoxIsActive;
            SetTextBoxActive(textBoxIsActive);
            taskSystem.askedForIDDone();
            textAliveTime = 7.0f;
        }

        private void fetchRandomPatientFromStorage() {
            patient = patientStorage.GetRandomPatient();
        }

        private void setPatientIdentityInformationsInTextBox() {
            TextMeshProUGUI idInfoTMP = getTextPromtObject();
            idInfoTMP.text = getPatientIdentityInformation();
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

        private TextMeshProUGUI getTextPromtObject() {
            return this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        private GameObject getPatientTextBox() {
            return this.gameObject.transform.GetChild(0).gameObject;
        }

        private void hideTextBox() {
            GameObject textBox = getPatientTextBox();
            textBox.SetActive(false);
            textAliveTime = 0;
        }

        private void displayTextBox() {
            textAliveTime = 6.0f;
            GameObject textBox = getPatientTextBox();
            textBox.SetActive(true);
        }


        private void SetTextBoxActive(bool isActive) {
            GameObject textBox = getPatientTextBox();
            textBox.SetActive(isActive);
        }

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

        
        public string getTubesToTakeStr() {
            string tubes = "";
            List<SampleTube> tubesToTake = GetTubesToTakeList();
            foreach (SampleTube tube in tubesToTake) {
                tubes += (tube.analysis + "\n");
            }
            return tubes;
        }

        public List<SampleTube> GetTubesToTakeList() {
            return patient.referral.tubesToTake;
        }

        public int GetNumberOfTubesToTake() {
            return patient.referral.tubesToTake.Count;
        }

    }
}
