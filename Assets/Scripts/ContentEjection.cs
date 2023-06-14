using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEjection : MonoBehaviour
{
    [SerializeField] Packaging MainPackage;
    void OnJointBreak(float breakForce)
    {
        MainPackage.OnEjected();
    }
}
