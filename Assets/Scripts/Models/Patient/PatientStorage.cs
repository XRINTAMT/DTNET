using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Models.Patient
{
    [CreateAssetMenu(fileName = "newPatientStorage", menuName = "DTNET/Patient Storage")]
    public class PatientStorage : ScriptableObject
    {
        public List<Patient> patients;

        public Patient GetPatientByIndex(int index)
        {
            return patients[index];
        }

        public Patient GetRandomPatient() {
            var random = new System.Random();
            int index = random.Next(patients.Count);
            return patients[index];
        }
    }
}

