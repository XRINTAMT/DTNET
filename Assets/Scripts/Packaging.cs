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
    bool isEjected;

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
        Rigidbody _rb = Content.GetComponent<Rigidbody>();
        _rb.detectCollisions = true;
        _rb.angularDrag = 0.05f;
        _rb.mass = 1;
    }

    public void OnEjected()
    {
        isEjected = true;
    }

    public void OnGrabbed()
    {
        if(RemovablePart != null)
        {
            RemovablePart.enabled = true;
        }
        
    }

    public void OnContentGrabbed()
    {
        ConfigurableJoint CJ = Content.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = 1000;
        }
    }

    public void OnContentReleased()
    {
        if (!isEjected)
        {
            ConfigurableJoint CJ = Content.GetComponent<ConfigurableJoint>();
            if (CJ != null)
            {
                CJ.breakForce = float.PositiveInfinity;
            }
        }
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
        if(RemovablePart != null)
        {
            RemovablePart.enabled = isUnpacked;
        }
    }

    public void OnRemovableReleased()
    {
        ConfigurableJoint CJ = RemovablePart.GetComponent<ConfigurableJoint>();
        if (CJ != null)
        {
            CJ.breakForce = float.PositiveInfinity;
        }
    }

    public void MainPackagingDestroyed()
    {
        if (!isUnpacked)
        {
            Destroy(gameObject);
        }
        if (!isEjected)
        {
            if(Content != null)
                Destroy(Content.gameObject);
        }
    }
}
