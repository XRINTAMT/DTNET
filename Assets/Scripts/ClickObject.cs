using RuntimeHandle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField] private RuntimeTransformHandle runtimeTransformHandle;

    private void OnMouseDown()
    {
        runtimeTransformHandle = FindObjectOfType<RuntimeTransformHandle>();

        if (runtimeTransformHandle!=null) runtimeTransformHandle.target = gameObject.transform;
    }
}
