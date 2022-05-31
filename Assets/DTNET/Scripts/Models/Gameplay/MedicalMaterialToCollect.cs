using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTNET.Models.Patient;

public class MedicalMaterialToCollect : MonoBehaviour
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

    public bool IsNotInArea() {
        return !isInArea;
    }

    public bool IsTheSameMedicalMaterialType(MedicalMaterialType collectedType) {
        return (collectedType == materialType);
    }
}
