using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DTNET.Models {
    public class CollectablelMaterial : MonoBehaviour
    {
        public string materialName;
        public MedicalMaterialType materialType;
        private bool isInArea = false;

        public void HasEnterTheArea() {
            isInArea = true;
        }

        public void HasLeftTheArea() {
            isInArea = false;
        }

        public bool IsAlreadyInArea() {
            return isInArea;
        }

        public bool IsNotInArea() {
            return !isInArea;
        }
    }
}
