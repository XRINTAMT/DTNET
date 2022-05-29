using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Models {
    public class AreaToPlaceMedicalMaterials : MonoBehaviour
    {

        public TaskSystem TaskSystem;
        private HashSet<string> m_currentMaterialsInArea = new HashSet<string>();
        private int materialCount = 4;

        void Start() {
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
            MedicalMaterialToCollect medicalMaterial = other.attachedRigidbody.GetComponentInChildren<MedicalMaterialToCollect>();
            Debug.Log("m_currentMaterialsInArea.Count :: " + m_currentMaterialsInArea.Count);
            if (medicalMaterial != null && medicalMaterial.IsNotInArea())
            {
                Debug.Log("Added Medical Material!");
                medicalMaterial.HasEnterTheArea();
                //m_currentMaterialsInArea.Add(medicalMaterial.materialName);
                AddMaterial(medicalMaterial.materialName);

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
            RemoveMaterial(medicalMaterial.materialName);
            medicalMaterial.HasLeftTheArea();
        }


        private void AddMaterial(string materialName) {
            m_currentMaterialsInArea.Add(materialName);
        }

        private bool RemoveMaterial(string materialName) {
            return m_currentMaterialsInArea.Remove(materialName);
        }
    }
}
