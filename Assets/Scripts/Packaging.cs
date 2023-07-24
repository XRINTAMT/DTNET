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
        
    }

    public void OnUnpacked()
    {
        //Content.gameObject.AddComponent<Rigidbody>();
        Content.enabled = true;
        isUnpacked = true;
        Rigidbody _rb = Content.GetComponent<Rigidbody>();
        _rb.detectCollisions = true;
        _rb.angularDrag = 0.05f;
        _rb.mass = 1;
        RemovablePart.gameObject.AddComponent<Trash>();
    }

    public void OnEjected()
    {
        isEjected = true;
        Debug.Log("Ejected");
        Package.gameObject.AddComponent<Trash>();
    }

    public void OnGrabbed()
    {
        if(RemovablePart != null)
        {
            RemovablePart.enabled = true;
        }
        if (!isUnpacked)
        {
            Rigidbody _rb = Content.GetComponent<Rigidbody>();
            _rb.detectCollisions = false;
            _rb.angularDrag = 0;
            _rb.mass = 0;
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
        if (!isUnpacked)
        {
            Rigidbody _rb = Content.GetComponent<Rigidbody>();
            _rb.detectCollisions = true;
            _rb.angularDrag = 0.05f;
            _rb.mass = 1;
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
        Debug.Log("Unpacked: " + isUnpacked + "; Ejected: " + isEjected);
        if (!isUnpacked)
        {
            Destroy(gameObject);
        }
        else
        {
            if (!isEjected)
            {
                if (Content != null)
                {
                    Destroy(Content.gameObject);
                }

            }
        }
    }
}
