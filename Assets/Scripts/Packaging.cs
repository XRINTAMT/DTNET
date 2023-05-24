using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

public class Packaging : MonoBehaviour
{
    [SerializeField] Grabbable Package;
    [SerializeField] Grabbable RemovablePart;
    [SerializeField] Grabbable Content;

    bool isUnpacked;

    public void Start()
    {
        
    }

    public void OnUnpacked()
    {
        Content.enabled = true;
        isUnpacked = true;
        ConfigurableJoint CJ = Content.GetComponent<ConfigurableJoint>();
        if(CJ != null)
        {
            CJ.breakForce = 1000;
        }
    }

    public void OnGrabbed()
    {
        RemovablePart.enabled = true;
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = 1000;
        }
    }

    public void OnReleased()
    {
        RemovablePart.enabled = isUnpacked;
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = float.PositiveInfinity;
        }
    }
}
