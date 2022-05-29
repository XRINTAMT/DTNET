using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalMaterialToCollect : MonoBehaviour
{
    public string materialName;
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
}
