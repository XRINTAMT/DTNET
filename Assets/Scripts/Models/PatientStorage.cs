using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Models
{
    [CreateAssetMenu(fileName = "newPatientStorage", menuName = "DTNET/Patient Storage")]
    public class PatientStorage : ScriptableObject
    {
        public List<Patient> patients;

        public Patient getPatientByIndex(int index)
        {
            return patients[index];
        }
    }
}

