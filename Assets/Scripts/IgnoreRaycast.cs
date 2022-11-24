using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreRaycast : MonoBehaviour
{
    void Awake()
    {
        //gameObject.layer uses only integers, but we can turn a layer name into a layer integer using LayerMask.NameToLayer()
        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.layer = LayerIgnoreRaycast;
        Debug.Log("Current layer: " + gameObject.layer);
    }
}
