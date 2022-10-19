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
    }
    public void ButtonRotation()
    {
        runtimeTransformHandle.type = (HandleType)1;
    }

    public void ButtonScale()
    {
        runtimeTransformHandle.type = (HandleType)2;
    }

}
