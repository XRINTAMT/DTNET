using RuntimeHandle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    [SerializeField] private RuntimeTransformHandle runtimeTransformHandle;

    public void ButtonPosition() 
    {
        runtimeTransformHandle.type = (HandleType)0;
        runtimeTransformHandle.axes = HandleAxes.XYZ;
    }
    public void ButtonRotation()
    {
        runtimeTransformHandle.type = (HandleType)1;
        runtimeTransformHandle.axes = HandleAxes.Y;
    }

    public void ButtonScale()
    {
        runtimeTransformHandle.type = (HandleType)2;
    }

}
