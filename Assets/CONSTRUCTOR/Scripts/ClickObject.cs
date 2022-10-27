using RuntimeHandle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField] private RuntimeTransformHandle runtimeTransformHandle;
    [SerializeField] GameObject PanelInformation;

    private void OnMouseDown()
    {

        runtimeTransformHandle = FindObjectOfType<RuntimeTransformHandle>();

        if (runtimeTransformHandle!=null) runtimeTransformHandle.target = gameObject.transform;

        if (PanelInformation!=null) PanelInformation.SetActive(true);
       
    }
}
