using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DTNET.Models;

namespace DTNET.Models.Patient {
    public class PatientObject : MonoBehaviour
    {
        private bool textBoxIsActive;
        public PatientStorage patientStorage;

        public TaskSystem taskSystem; 
        private Patient patient; 


        // Start is called before the first frame update
        void Start()
        {
            initPatient();
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
        }

        private void displayTextBox() {
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
