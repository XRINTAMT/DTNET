using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.Handlers;
using DTNET.Models;

namespace DTNET.Tasks {
    public class CollectMaterialTask : MonoBehaviour
    {
        public TaskTrackingHandler taskTracking;
        public PatientTask patient;
        public CameraUIHandler cameraUI;

        private HashSet<MedicalMaterialType> currentMaterialsInArea;
        private int materialCount = 0;

        private bool taskIsPerformed = false;

        // Start is called before the first frame update
        void Start()
        {
            currentMaterialsInArea = new HashSet<MedicalMaterialType>();
            materialCount += patient.GetNumberOfTubesToTake();
        }

        // Update is called once per frame
        void Update()
        {
            if(TaskIsNotCompleted()) return;

            if(taskIsPerformed) return;

            PerformCollectMaterialsTask();
        }

        private void PerformCollectMaterialsTask() 
        {
            Debug.Log("All Materials is collected!");
            taskIsPerformed = true;
            taskTracking.CollectedAllMaterialsTaskCompleted();
            cameraUI.DisplayMessage("Next\nPut on a pair of gloves");
        }

        void OnTriggerEnter(Collider other) 
        {
            CollectablelMaterial CollectedMedicalMaterial = other.attachedRigidbody.GetComponentInChildren<CollectablelMaterial>();
            if(CollectedMedicalMaterial == null) return;

            if(CollectedMedicalMaterial.IsAlreadyInArea()) 
            {
                Debug.Log("Already Collected!");
                return;
            }

            if (CollectedMedicalMaterial.IsNotInArea())
            {
                Debug.Log("Added New Medical Material!");
                CollectedMedicalMaterial.HasEnterTheArea();
                AddMaterial(CollectedMedicalMaterial.materialType);
            } 
            else 
            {
                Debug.Log("Medical Material not valid");
            }
            Debug.Log("Current Materials In Area :: " + currentMaterialsInArea.Count);
        }

        void OnTriggerExit(Collider other) {
            CollectablelMaterial medicalMaterial = other.attachedRigidbody.GetComponentInChildren<CollectablelMaterial>();
            if(medicalMaterial == null) return;

            Debug.Log("Material Left the Area!");
            try {
                RemoveMaterial(medicalMaterial.materialType);
                medicalMaterial.HasLeftTheArea();
            } catch(System.Exception e) {
                Debug.Log("[Exception] OnTriggerExit:: "+e.Message);
            }

        }

        private bool TaskIsNotCompleted() {
            return (currentMaterialsInArea.Count < materialCount);
        }

        private void AddMaterial(MedicalMaterialType materialType) {
            currentMaterialsInArea.Add(materialType);
        }

        private bool RemoveMaterial(MedicalMaterialType materialType) {
            return currentMaterialsInArea.Remove(materialType);
        }

    }
}
