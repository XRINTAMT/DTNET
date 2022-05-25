using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DTNET.Models.Patient {
    public class PatientObject : MonoBehaviour
    {
        private bool textBoxIsActive;
        public PatientStorage patientStorage;
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

        private void fetchRandomPatientFromStorage() {
            patient = patientStorage.GetRandomPatient();
        }

        private void setPatientIdentityInformationsInTextBox() {
            TextMeshProUGUI idInfoTMP = getTextPromtObject();
            idInfoTMP.text = getPatientIdentityInformation();
        }
        
        private string getPatientIdentityInformation() {
            string patientInfo = "Hi name name is: " + this.getPatientFullnamne() + "\n";
            patientInfo += "and my identifier is: " + this.getPatientId();
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

        public void askPatientAboutIdentity() {
            textBoxIsActive = !textBoxIsActive;
            SetTextBoxActive(textBoxIsActive);
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

        
        public string getTubesToTake() {
            string tubes = "";
            foreach (int tube in patient.referral.tubesToTake) {
                tubes += (tube + "\n");
            }
            return tubes;
        }

    }
}
