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
        Rigidbody _rb = Content.GetComponent<Rigidbody>();
        _rb.detectCollisions = false;
        _rb.angularDrag = 0;
        _rb.mass = 0;
    }

    public void OnUnpacked()
    {
        Content.gameObject.AddComponent<Rigidbody>();
        Content.enabled = true;
        isUnpacked = true;
        ConfigurableJoint CJ = Content.GetComponent<ConfigurableJoint>();
        if(CJ != null)
        {
            CJ.breakForce = 2500;
        }
        Rigidbody _rb = Content.GetComponent<Rigidbody>();
        _rb.detectCollisions = true;
        _rb.angularDrag = 0.05f;
        _rb.mass = 1;
    }

    public void OnGrabbed()
    {
        RemovablePart.enabled = true;
    }

    public void OnRemovableGrabbed()
    {
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = 2500;
        }
    }

    public void OnReleased()
    {
        RemovablePart.enabled = isUnpacked;
    }

    public void OnRemovableReleased()
    {
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = float.PositiveInfinity;
        }
    }
}