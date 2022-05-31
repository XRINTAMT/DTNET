using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.Models.Patient;

namespace DTNET.Models {
    public class AreaToPlaceMedicalMaterials : MonoBehaviour
    {

        public TaskSystem TaskSystem;
        private HashSet<MedicalMaterialType> m_currentMaterialsInArea = new HashSet<MedicalMaterialType>();
        private int materialCount = 1;

        public PatientObject patientObject;

        void Start() {
            materialCount += patientObject.GetNumberOfTubesToTake();
            Debug.Log("Number Of Materials To Collect:: "+materialCount);
            StartCoroutine(CollectMaterials());
        }

        IEnumerator CollectMaterials() 
        {
            while(true) {
                if(m_currentMaterialsInArea.Count == materialCount) 
                {
                    Debug.Log("All Materials in place? :)");
                    TaskSystem.hasCollectedAllMedicalMaterials();
                    yield break; // End Coroutine
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        

        void OnTriggerEnter(Collider other) 
        {
            //CauldronIngredient ingredient = other.attachedRigidbody.GetComponentInChildren<CauldronIngredient>();
            MedicalMaterialToCollect CollectedMedicalMaterial = other.attachedRigidbody.GetComponentInChildren<MedicalMaterialToCollect>();
            Debug.Log("m_currentMaterialsInArea.Count :: " + m_currentMaterialsInArea.Count);
            if (CollectedMedicalMaterial != null && CollectedMedicalMaterial.IsNotInArea())
            {
                Debug.Log("Added Medical Material!");
                CollectedMedicalMaterial.HasEnterTheArea();
                //m_currentMaterialsInArea.Add(medicalMaterial.materialName);
                AddMaterial(CollectedMedicalMaterial.materialType);
            } 
            else 
            {
                Debug.Log("Medical Material not valid, mby already collected?");
            }
        }

        void OnTriggerExit(Collider other) {
            Debug.Log("Material Left the Area!");
            MedicalMaterialToCollect medicalMaterial = other.attachedRigidbody.GetComponentInChildren<MedicalMaterialToCollect>();
            //bool didRemove = m_currentMaterialsInArea.Remove(medicalMaterial.materialName);
            RemoveMaterial(medicalMaterial.materialType);
            medicalMaterial.HasLeftTheArea();
        }


        private void AddMaterial(MedicalMaterialType materialType) {
            m_currentMaterialsInArea.Add(materialType);
        }

        private bool RemoveMaterial(MedicalMaterialType materialType) {
            return m_currentMaterialsInArea.Remove(materialType);
        }
    }
}
