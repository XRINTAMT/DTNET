using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : DataSaver
{
    Vector3 LocalPosition;
    Vector3 LocalEulers;
    Vector3 LocalScale;
    Transform Parent;
    bool Kinematic;
    Rigidbody rb;

    private void Start()
    {
        TryGetComponent<Rigidbody>(out rb);
    }

    override public void Save()
    {
        Debug.Log(name + " location saved");
        LocalPosition = transform.localPosition;
        LocalEulers = transform.localEulerAngles;
        LocalScale = transform.localScale;
        Parent = transform.parent;
        if (rb != null)
            Kinematic = rb.isKinematic;
    }

    override public void Load()
    {
        Debug.Log(name + " location loaded");
        transform.parent = Parent;
        transform.localPosition = LocalPosition;
        transform.localEulerAngles = LocalEulers;
        transform.localScale = LocalScale;
        if (rb != null)
            rb.isKinematic = Kinematic;
    }
}
